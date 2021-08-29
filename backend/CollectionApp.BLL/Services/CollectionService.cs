using CollectionApp.BLL.Interfaces;
using CollectionApp.DAL.Attributes;
using CollectionApp.DAL.Entities;
using CollectionApp.DAL.Interfaces;
using System.Linq;
using System.Collections.Generic;
using CollectionApp.BLL.DTO;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.Extensions.Configuration;
using CollectionApp.DAL.DTO;
using CollectionApp.BLL.Exceptions;
using CollectionApp.BLL.Utils;
using CollectionApp.DAL.Utils;
using System.Linq.Expressions;
using System;
using CollectionApp.DAL.Extensions;

namespace CollectionApp.BLL.Services
{
    public class CollectionService : ICollectionService
    {
        IUnitOfWork UnitOfWork { get; }
        public IConfiguration Configuration { get; }

        private IAccountService _accountService;

        public CollectionService(
            IUnitOfWork unitOfWork,
            IConfiguration configuration,
            IAccountService accountService)
        {
            UnitOfWork = unitOfWork;
            Configuration = configuration;
            _accountService = accountService;
        }

        public IEnumerable<string> GetTopics()
        {
            var topics = new List<string>();
            var props = typeof(Collection).GetProperties();
            foreach (var prop in props)
            {
                var attrs = (TopicAttribute[])prop.GetCustomAttributes(typeof(TopicAttribute), false);
                foreach (var attr in attrs)
                {
                    if (attr != null)
                    {
                        topics.AddRange(attr.Topics.ToList<string>());
                    }
                }
            }
            return topics;
        }
        
        async private Task DeleteImages(Cloudinary cloudinary, Collection collection)
        {
            var images = UnitOfWork.Images.Find(image => image.CollectionId == collection.Id).ToList();
            foreach (var image in images)
            {
                var deletionParams = new DeletionParams(image.PublicId);
                cloudinary.Destroy(deletionParams);
                await UnitOfWork.Images.Delete(image.Id);
            }
            await UnitOfWork.SaveAsync();
        }

        async private Task UploadImages(Collection collection, List<IFormFile> files)
        {
            var account = new Account(
                Configuration["Cloudinary:Name"],
                Configuration["Cloudinary:Key"],
                Configuration["Cloudinary:Secret"]);
            var cloudinary = new Cloudinary(account);
            await DeleteImages(cloudinary, collection);
            foreach (var file in files)
            {
                var uploadResult = cloudinary.Upload(new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, file.OpenReadStream()),
                });
                UnitOfWork.Images.Add(new Image
                {
                    ImagePath = uploadResult.SecureUrl.AbsoluteUri,
                    PublicId = uploadResult.PublicId,
                    Collection = collection
                });
                await UnitOfWork.SaveAsync();
            }
        }

        public async Task<Collection> CheckRights(
            ClaimsPrincipal claimsPrincipal,
            int collectionId,
            string userId = "")
        {
            var collection = await UnitOfWork.Collections
                .Get(collectionId, collection => collection.User);
            var user = await _accountService.GetCurrentUser(claimsPrincipal, userId);
            if (user.Id != collection.User.Id && !RoleUtil.IsAdmin(claimsPrincipal))
            {
                throw new UserNoRightsException();
            }
            return collection;
        }

        public async Task CreateCollection(
            ClaimsPrincipal userPrincipal,
            CollectionDTO collectionDto,
            string userId = "")
        {
            using (var transaction = UnitOfWork.Context.Database.BeginTransaction())
            {
                collectionDto.User = await _accountService.GetCurrentUser(userPrincipal, userId);
                var collection = UnitOfWork.Collections
                    .Add(MapperUtil.Map<CollectionDTO, Collection>(collectionDto));
                UnitOfWork.Collections.Add(collection);
                await UnitOfWork.SaveAsync();
                await UploadImages(collection, collectionDto.Files);
                await transaction.CommitAsync();
            }
        }

        public async Task<EntityPageDTO<Collection>> GetUserCollections(
            ClaimsPrincipal claimsPrincipal,
            int page = 1,
            string userId = "")
        {
            var user = await _accountService.GetCurrentUser(claimsPrincipal, userId);
            return UnitOfWork.Collections
                .Paginate(
                page: page,
                predicate: collection => collection.User == user,
                includes: new Expression<Func<Collection, object>>[] {
                    collection => collection.Images,
                    collection => collection.User,
                });
        }

        public async Task<CollectionDTO> GetCollection(int collectionId)
        {
            var collection = await UnitOfWork.Collections.Get(collectionId);
            if (collection == null)
            {
                throw new CollectionNotFound();
            }
            var collectionDto = MapperUtil.Map<Collection, CollectionDTO>(collection);
            collectionDto.Topics = GetTopics();
            return collectionDto;
        }

        public IEnumerable<string> GetImages(int collectionId)
        {
            return UnitOfWork.Images
                .Find(image => image.CollectionId == collectionId)
                .Select(image => image.ImagePath)
                .ToList();
        }

        public async Task EditCollection(ClaimsPrincipal claimsPrincipal, CollectionDTO collectionDto)
        {       
            var collection = await CheckRights(claimsPrincipal,(int)collectionDto.Id);
            using (var transaction = UnitOfWork.Context.Database.BeginTransaction())
            {
                collectionDto.User = collection.User;
                MapperUtil.Map<CollectionDTO, Collection>(collectionDto, collection);
                UnitOfWork.Collections.Update(collection);
                await UploadImages(collection, collectionDto.Files);
                await UnitOfWork.SaveAsync();
                await transaction.CommitAsync();
            }
        }

        public async Task DeleteCollection(ClaimsPrincipal claimsPrincipal, int collectionId)
        {
            await CheckRights(claimsPrincipal, collectionId);
            using (var transaction = UnitOfWork.Context.Database.BeginTransaction())
            {
                var collection = await CheckRights(claimsPrincipal, collectionId);
                await UploadImages(collection, new List<IFormFile>());
                await UnitOfWork.Collections.Delete(collectionId);
                await UnitOfWork.SaveAsync();
                await transaction.CommitAsync();
            }
        }

        public IEnumerable<Collection> GetLagestNumberItems()
        {
            return UnitOfWork.Context.Collections
                .IncludeMultiple(
                collection => collection.Images,
                collection => collection.User)
                .OrderByDescending(collection => collection.Items.Count())
                .Take(6);
        }
    }
}
