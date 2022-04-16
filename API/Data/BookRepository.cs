using API.DTO;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class BookRepository : IBookRepostiory
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public BookRepository(DataContext context, IMapper mapper)
        {
             _context = context;
              _mapper = mapper;
        }



        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book> GetBooksByAuthorAsync(string author)
        {
            return await _context.Books.SingleOrDefaultAsync(x => x.Author == author);
        }

        public async Task<Book> GetBooksByIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<Book> GetBooksByTitleAsync(string title)
        {
             return await _context.Books.SingleOrDefaultAsync(x => x.Title == title);
        }



        public async Task<PagedList<BookDto>> GetBooksPagedAsync(BookParams bookParams)
        {
            //var query = _context.Books.ProjectTo<BookDto>(_mapper.ConfigurationProvider).AsNoTracking().AsQueryable();
            var query = _context.Books.AsQueryable();

            if((int)bookParams.bookSearchEnum ==0){
                var searchString = bookParams.Title == null ? "" : bookParams.Title;
                query = query.Where(u => u.Title.StartsWith(searchString));
             }else if((int)bookParams.bookSearchEnum == 1){ 
                 var searchString = bookParams.Author == null ? "" : bookParams.Author;
                 query = query.Where(u => u.Author.StartsWith(searchString));
            }

            return await PagedList<BookDto>.CreateAsync(query.ProjectTo<BookDto>(_mapper.ConfigurationProvider).AsNoTracking(), bookParams.PageNumber, bookParams.PageSize);
        }

        public async Task<BookDto> GetBookDtoByTitleAsync(string title)
        {
            return await _context.Books.Where(x => x.Title == title).ProjectTo<BookDto>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
        }

        public async Task<BookDto> GetBooksDtoByAuthorAsync(string author)
        {
            return await _context.Books.Where(x => x.Author == author).ProjectTo<BookDto>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
        }

        public void DeleteBook(Book book)
        {
            _context.Books.Remove(book);
        }
    }
}