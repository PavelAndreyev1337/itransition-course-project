using CollectionApp.DAL.EF;
using CollectionApp.DAL.Entities;
using CollectionApp.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace CollectionApp.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationContext _context;
        private IRepository<Collection> _collectionRepository;
        private IRepository<Comment> _commentRepository;
        private IRepository<Image> _imageRepository;
        private IRepository<Item> _itemRepository;
        private IRepository<Tag> _tagRepository;
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private SignInManager<User> _signInManager;

        public IRepository<Collection> Collections
        {
            get
            {
                _collectionRepository = _collectionRepository ?? new CollectionRepository(_context);
                return _collectionRepository;
            }

        }

        public IRepository<Comment> Comments
        {
            get
            {
                _commentRepository = _commentRepository ?? new CommentRepository(_context);
                return _commentRepository;
            }
        }

        public IRepository<Image> Images
        {
            get
            {
                _imageRepository = _imageRepository ?? new ImageRepository(_context);
                return _imageRepository;
            }
        }

        public IRepository<Item> Items
        {
            get
            {
                _itemRepository = _itemRepository ?? new ItemRepository(_context);
                return _itemRepository;
            }
        }

        public IRepository<Tag> Tags
        {
            get
            {
                _tagRepository = _tagRepository ?? new TagRepository(_context);
                return _tagRepository;
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

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
