
using API.DTO;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface IBookRepostiory
    {

        Task<IEnumerable<Book>> GetBooksAsync();
        Task<Book> GetBooksByIdAsync(int id);
        Task<Book> GetBooksByTitleAsync(string title);
        Task<Book> GetBooksByAuthorAsync(string author);
        void Update(Book book);
        Task<bool> SaveAllAsync();
        Task<PagedList<BookDto>> GetBooksPagedAsync(BookParams bookParams);

        Task<BookDto> GetBookDtoByTitleAsync(string title);
        Task<BookDto> GetBooksDtoByAuthorAsync(string author);

        void DeleteBook(Book book);
    }
}