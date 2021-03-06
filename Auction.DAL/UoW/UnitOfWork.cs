using Auction.DAL.Models;
using Auction.DAL.Repositories;
using Auction.DAL.Repositories.Abstract;
using System;
using System.Threading.Tasks;

namespace Auction.DAL.UoW
{
    public class UnitOfWork:IUnitOfWork,IDisposable
    {
        private readonly AuctionDbContext _dbContext;

        public UnitOfWork()
        {
            _dbContext = new AuctionDbContext();
        }


        private IUserRepository _userRepository;
        private ILotRepository _lotRepository;
        private ILoginRepository _loginRepository;
        private IAccountTypeRepository _accountTypeRepository;
        private ICategoryRepository _categoryRepository;
        private IPictureRepository _pictureRepository;
        private IStakeRepository _stakeRepository;
        private INewsRepository _newsRepository;
        private ICartRepository _cartRepository;
        private IStatusRepository _statusRepository;

        public IStatusRepository StatusRepository
        {
            get
            {
                if( _statusRepository == null )
                    _statusRepository=new StatusRepository(_dbContext);
                return _statusRepository;
            }
        }

        public ICartRepository CartRepository
        {
            get
            {
                if( _cartRepository == null )
                    _cartRepository = new CartRepository(_dbContext);
                return _cartRepository;
            }
        }

        public INewsRepository NewsRepository
        {
            get
            {
                if (_newsRepository == null)
                    _newsRepository = new NewsRepository(_dbContext);
                return _newsRepository;
            }
        }

        public IStakeRepository StakeRepository
        {
            get
            {
                if( _stakeRepository == null )
                    _stakeRepository=new StakeRepository(_dbContext);
                return _stakeRepository;
            }
        }

        public IPictureRepository PictureRepository
        {
            get
            {
                if (_pictureRepository == null)
                    _pictureRepository = new PictureRepository(_dbContext);
                return _pictureRepository;
            }
        }
        public ICategoryRepository CategoryRepository
        {
            get
            {
                if (_categoryRepository == null)
                    _categoryRepository = new CategoryRepository(_dbContext);
                return _categoryRepository;
            }
        }
        public ILoginRepository LoginRepository
        {
            get
            {
                if (_loginRepository == null)
                    _loginRepository = new LoginRepository(_dbContext);
                return _loginRepository;
            }
        }
        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_dbContext);
                return _userRepository;
            }
        }
        public ILotRepository LotRepository
        {
            get
            {
                if(_lotRepository == null)
                    _lotRepository = new LotRepository(_dbContext);
                return _lotRepository;
            }
        }

        public IAccountTypeRepository AccountTypeRepository
        {
            get
            {
                if(_accountTypeRepository == null)
                    _accountTypeRepository= new AccountTypeRepository(_dbContext);
                return _accountTypeRepository;
            }
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        
    }
}
