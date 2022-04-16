using API.Data;
using API.DTO;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    //[Authorize]oo
    public class BookController : BaseApiController
    {
        private readonly IBookRepostiory _bookRepostiory;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public BookController(IBookRepostiory bookRepostiory, IMapper mapper, IUserRepository userRepository, DataContext context)
        {
            _bookRepostiory = bookRepostiory;
            _mapper = mapper;
            _userRepository = userRepository;
            _context = context;
        }
 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks([FromQuery]BookParams bookParams){
            var books = await _bookRepostiory.GetBooksPagedAsync(bookParams);
            //response je dostopen v vseh controllerjih
            Response.AddPaginationHeader(books.CurrentPage, books.PageSize, books.TotalCount, books.TotalPages);
            //var booksToReturn = _mapper.Map<IEnumerable<BookDto>>(books);
            //ne dela brez ok
            //return Ok(booksToReturn);
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task< ActionResult<BookDto>> GetBookById(int id ){
            var book = await _bookRepostiory.GetBooksByIdAsync(id);

            return _mapper.Map<BookDto>(book);
        }
        

        /*
        [HttpPut]
        public async Task<ActionResult> UpdateBook(BookDto bookDto)
        {   
            //takole bi loh dostopal do usernamea ki je shranjen v tokenu
            //var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var book = await _bookRepostiory.GetBooksByIdAsync(bookDto.Id);
            //razišči zakaj ne dela brez <BookDto, Book>
            _mapper.Map<BookDto, Book>(bookDto, book);
            _bookRepostiory.Update(book);

            if (await _bookRepostiory.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update user");
        }
        */

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> UpdateBook(BookDto bookDto)
        {   
            //takole bi loh dostopal do usernamea ki je shranjen v tokenu
            //var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var book = await _bookRepostiory.GetBooksByIdAsync(bookDto.Id);
            //razišči zakaj ne dela brez <BookDto, Book>
            _mapper.Map<BookDto, Book>(bookDto, book);
            _bookRepostiory.Update(book);

            if (await _bookRepostiory.SaveAllAsync()){ 
                return NoContent();
            }

            return BadRequest("Failed to update user");
        }

        [Authorize]
        [HttpPost("lend")]
        public async Task<ActionResult> LendBook(BookDto bookDto)
        {   
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());
            var book = await _bookRepostiory.GetBooksByIdAsync(bookDto.Id);
            //razišči zakaj ne dela brez <BookDto, Book>
            _mapper.Map<BookDto, Book>(bookDto, book);

            user.Books.Add(book);
            _bookRepostiory.Update(book);
            //_userRepository.Update(user);
        
            var userR = false;
            var bookR = false;


            if(await _userRepository.SaveAllAsync()){
                userR = true;
            }


            if (await _bookRepostiory.SaveAllAsync() ) {
                bookR = true;
            }

            if(userR && bookR){
                return NoContent();
            }

            return BadRequest("Failed to update book");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("delete/{bookId}")]
        public async Task<ActionResult> DeleteBook(int bookId)
        {   var username = User.GetUsername();
            //takole bi loh dostopal do usernamea ki je shranjen v tokenu
            //var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Book book = await _bookRepostiory.GetBooksByIdAsync(bookId);

            _bookRepostiory.DeleteBook(book);

            if (await _bookRepostiory.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update user");
        }
        
    }
}