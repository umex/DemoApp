using API.DTO;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository userRepository, IMapper mapper) 
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers(){
           
            var users = await _userRepository.GetUsersAsync();

            var usersToReturn = _mapper.Map<IEnumerable<UserDto>>(users);
            //ne dela brez ok
            return Ok(usersToReturn);
        }

        //[Authorize]
        [HttpGet("{id}")]
        public async Task< ActionResult<UserDto>> GetUserById(int id ){
            var user = await _userRepository.GetUserByIdAsync(id);



            return _mapper.Map<UserDto>(user);
        }

        [HttpGet("username/{username}")]
        public async Task< ActionResult<UserDto>> GetUserByUsername(string username){
            var user = await _userRepository.GetUserByUsernameAsync(username);
            return _mapper.Map<UserDto>(user);
        }

    }
}