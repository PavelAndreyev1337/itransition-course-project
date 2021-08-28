using CollectionApp.DAL.EF;
using CollectionApp.DAL.Entities;
using CollectionApp.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace CollectionApp.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationContext _context;
        private IRepository<Collection, int> _collectionRepository;
        private IRepository<Comment, int> _commentRepository;
        private IRepository<Image, int> _imageRepository;
        private IRepository<Item, int> _itemRepository;
        private IRepository<Tag, int> _tagRepository;
        private IRepository<User, string> _userRepository;
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private SignInManager<User> _signInManager;
        private bool disposed = false;

        public ApplicationContext Context
        { 
            get
            {
                return _context;
            }
        }

        public IRepository<Collection, int> Collections
        {
            get
            {
                _collectionRepository = _collectionRepository ?? new CollectionRepository(_context);
                return _collectionRepository;
            }

        }

        public IRepository<Comment, int> Comments
        {
            get
            {
                _commentRepository = _commentRepository ?? new CommentRepository(_context);
                return _commentRepository;
            }
        }

        public IRepository<Image, int> Images
        {
            get
            {
                _imageRepository = _imageRepository ?? new ImageRepository(_context);
                return _imageRepository;
            }
        }

        public IRepository<Item, int> Items
        {
            get
            {
                _itemRepository = _itemRepository ?? new ItemRepository(_context);
                return _itemRepository;
            }
        }

        public IRepository<Tag, int> Tags
        {
            get
            {
                _tagRepository = _tagRepository ?? new TagRepository(_context);
                return _tagRepository;
            }
        }

        public IRepository<User, string> Users
        {
            get
            {
                _userRepository = _userRepository ?? new UserRepository(_context);
                return _userRepository;
            }
        }

        public UserManager<User> UserManager
        {
            get
            {
                return _userManager;
            }
        }

        public RoleManager<IdentityRole> RoleManager
        {
            get
            {
                return _roleManager;
            }
        }

        public SignInManager<User> SignInManager
        {
            get
            {
                return _signInManager;
            }
        }

        public UnitOfWork(
            ApplicationContext context,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
