﻿using CollectionApp.BLL.DTO;
using CollectionApp.DAL.DTO;
using CollectionApp.DAL.Entities;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CollectionApp.BLL.Interfaces
{
    public interface ICollectionService
    {
        public IEnumerable<string> GetTopics();
        public Task CreateCollection(ClaimsPrincipal user, CollectionDTO collectionDto);
        public Task<Collection> CheckRights(ClaimsPrincipal claimsPrincipal, int collectionId);
        public Task<EntityPageDTO<Collection>> GetUserCollections(ClaimsPrincipal claimsPrincipal, int page=1);
        public Task<CollectionDTO> GetCollection(int collectionId);
        public IEnumerable<string> GetImages(int collectionId);
        public Task EditCollection(ClaimsPrincipal claimsPrincipal, CollectionDTO collectionDto);
        public Task DeleteCollection(ClaimsPrincipal claimsPrincipal, int collectionId);
        void Dispose();
    }
}
