using CollectionApp.BLL.Interfaces;
using CollectionApp.DAL.DTO;
using CollectionApp.DAL.Entities;
using CollectionApp.DAL.Enums;
using CollectionApp.DAL.Interfaces;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CollectionApp.BLL.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public EntityPageDTO<User> GetUsers(ClaimsPrincipal claimsPrincipal, int page = 1)
        {
            return _unitOfWork.Users.Paginate(
                page: page,
                predicate: user =>
                {
                    var task = _unitOfWork.UserManager.GetRolesAsync(user);
                    task.Wait();
                    return task.Result.Count == 0;
                });
        }

        public async Task AddAdmin(string userId)
        {
            var user = await _unitOfWork.UserManager.FindByIdAsync(userId);
            await _unitOfWork.UserManager.AddToRoleAsync(user, Role.Admin.ToString().ToLower());
        }

        public async Task BlockUser(string userId)
        {
            var user = await _unitOfWork.UserManager.FindByIdAsync(userId);
            if (user.LockoutEnabled)
            {
                user.LockoutEnabled = false;
                user.LockoutEnd = DateTimeOffset.UtcNow;
            } 
            else
            {
                user.LockoutEnabled = true;
                user.LockoutEnd = DateTimeOffset.UtcNow.AddYears(3);
            }
            await _unitOfWork.UserManager.UpdateAsync(user);
            await _unitOfWork.UserManager.UpdateSecurityStampAsync(user);
        }

        public async Task DeleteUser(string userId)
        {
            var user = await _unitOfWork.Users.Get(
                userId,
                user => user.Collections,
                user => user.LikedItems,
                user => user.Comments);
            using (var transaction = _unitOfWork.Context.Database.BeginTransaction())
            {
                await _unitOfWork.UserManager.DeleteAsync(user);
                await transaction.CommitAsync();
            }
        }
    }
}
