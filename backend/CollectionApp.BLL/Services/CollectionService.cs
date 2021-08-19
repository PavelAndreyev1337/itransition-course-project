using CollectionApp.BLL.Interfaces;
using CollectionApp.DAL.Attributes;
using CollectionApp.DAL.Entities;
using CollectionApp.DAL.Interfaces;
using System.Linq;
using System.Collections.Generic;
using CollectionApp.BLL.DTO;
using CollectionApp.BLL.Enums;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.Extensions.Configuration;

namespace CollectionApp.BLL.Services
{
    public class CollectionService : ICollectionService
    {
        IUnitOfWork UnitOfWork { get; set; }
        public IConfiguration Configuration { get; set; }


        public CollectionService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            UnitOfWork = unitOfWork;
            Configuration = configuration;
        }

        public IEnumerable<string> GetTopics()
        {
            var topics = new List<string>();
            var props = typeof(Collection).GetProperties();
            foreach (var prop in props)
            {
                var attrs = (TopicAttribute[]) prop.GetCustomAttributes(typeof(TopicAttribute), false);
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

        private void SetExtraField(Collection collection, string propertyName)
        {
            collection.GetType().GetProperty(propertyName).SetValue(collection, true);
        }

        private void SetExtraFields(Collection collection, Dictionary<string, FieldType> fieldTypes)
        {
            foreach(var item in fieldTypes)
            {
                switch(item.Value)
                {
                    case FieldType.Integer:
                        SetExtraField(collection, item.Key + "IntegerFieldVisible");
                        break;
                    case FieldType.String:
                        SetExtraField(collection, item.Key + "StringFieldVisible");
                        break;
                    case FieldType.Markdown:
                        SetExtraField(collection, item.Key + "TextFieldVisible");
                        break;
                    case FieldType.Date:
                        SetExtraField(collection, item.Key + "DateFieldVisible");
                        break;
                    case FieldType.Boolean:
                        SetExtraField(collection, item.Key + "BoolVisible");
                        break;
                }
            }
        }

        async private Task UploadImages(Collection collection, List<IFormFile> files)
        {
            var account = new Account(
                Configuration["Cloudinary:Name"],
                Configuration["Cloudinary:Key"],
                Configuration["Cloudinary:Secret"]);
            var cloudinary = new Cloudinary(account);
            foreach(var file in files)
            {
                var uploadResult = cloudinary.Upload(new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, file.OpenReadStream())
                });
                UnitOfWork.Images.Add(new Image
                {
                    ImagePath = uploadResult.SecureUrl.AbsoluteUri,
                    Collection = collection
                });
                await UnitOfWork.SaveAsync();
            }
        }

        public async Task CreateCollection(ClaimsPrincipal userPrincipal, CollectionDTO collectionDto)
        {
            using (var transaction = UnitOfWork.Context.Database.BeginTransaction())
            {
                var user = await UnitOfWork.UserManager.GetUserAsync(userPrincipal);
                var collection = UnitOfWork.Collections.Add(new Collection
                {
                    Name = collectionDto.Name,
                    ShortDescription = collectionDto.Description,
                    Topic = collectionDto.Topic,
                    FirstFieldName = collectionDto.FirstFieldName,
                    SecondFieldName = collectionDto.SecondFieldName,
                    ThirdFieldName = collectionDto.ThirdFieldName,
                    User = user
                });
                var extraFields = new Dictionary<string, FieldType>
                {
                    { "First", collectionDto.FirstFieldType},
                    { "Second", collectionDto.SecondFieldType},
                    { "Third", collectionDto.ThirdFieldType}
                };
                SetExtraFields(collection, extraFields);
                UnitOfWork.Collections.Add(collection);
                await UnitOfWork.SaveAsync();
                await UploadImages(collection, collectionDto.Files);
                await transaction.CommitAsync();
            }
        }

        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}
