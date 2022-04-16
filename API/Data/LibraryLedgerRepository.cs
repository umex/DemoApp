using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class LibraryLedgerRepository : ILibraryLedgerRepository
    {
        private readonly DataContext _context;
        public LibraryLedgerRepository(DataContext context)
        {
             _context = context;
        }

        public async Task<LibraryLedger> GeLibraryLedgerByIdAsync(int id)
        {
            return await _context.LibraryLedgers.FindAsync(id);
        }

        public async Task<IEnumerable<LibraryLedger>> GeLibraryLedgersByBookIdAsync(int BookId)
        {
            return await _context.LibraryLedgers.Where(x => x.Book.Id == BookId).ToListAsync();
        }

        public async Task<IEnumerable<LibraryLedger>> GeLibraryLedgersByUserIdAsync(int UserId)
        {
            return await _context.LibraryLedgers.Where(x => x.User.Id == UserId).ToListAsync();
        }

        public async Task<IEnumerable<LibraryLedger>> GeteLibraryLedgerAsync()
        {
            return await _context.LibraryLedgers.ToListAsync();
        }

        public async Task<IEnumerable<LibraryLedger>> GetLibraryLedgersAsync()
        {
            return await _context.LibraryLedgers.ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(LibraryLedger libraryLedger)
        {
            _context.Entry(libraryLedger).State = EntityState.Modified;
        }
    }
}