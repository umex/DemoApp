using API.Entities;

namespace API.Interfaces
{
    public interface ILibraryLedgerRepository
    {
        void Update(LibraryLedger libraryLedger);
        Task<bool> SaveAllAsync();
        
        Task<LibraryLedger> GeLibraryLedgerByIdAsync(int id);
        Task<IEnumerable<LibraryLedger>> GetLibraryLedgersAsync();
        Task<IEnumerable<LibraryLedger>> GeLibraryLedgersByUserIdAsync(int UserId);
        Task<IEnumerable<LibraryLedger>> GeLibraryLedgersByBookIdAsync(int BookId);
    }
}