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
