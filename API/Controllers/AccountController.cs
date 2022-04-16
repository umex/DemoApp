using System.Security.Cryptography;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using API.DTOs;
using Microsoft.EntityFrameworkCore;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Identity;
using AutoMapper;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,ITokenService tokenService, IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        //ApiController nam pove parametre, ni vazno od kod pridejo url, body nima veze
        public async Task<ActionResult<UserRegisterDto>> Register(RegisterDto registerDto){

            if (await UserExists(registerDto.Username)) {
                return BadRequest("Username is taken");
            }
            
            /*
             var user = new AppUser
            {
                UserName = registerDto.Username
            };
            */
            AppUser user = new AppUser();

            user.UserName = registerDto.Username.ToLower();

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded){ 
                return BadRequest(result.Errors);
            }

            var roleResult = await _userManager.AddToRoleAsync(user, "Member");

            if (!roleResult.Succeeded) {
                return BadRequest(result.Errors);
            }

            if(!roleResult.Succeeded){
                return BadRequest();
            }

            string token = await  _tokenService.CreateTokenAsync(user);

            UserRegisterDto userDto = new UserRegisterDto{
                Username = user.UserName,
                Token = token
            };

            return userDto;
        }


        [HttpPost("login")]
        public async Task<ActionResult<UserRegisterDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username);

            if (user == null){
                return Unauthorized("Invalid username");
            } 

           

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) {
                return Unauthorized();
            }

            string token = await _tokenService.CreateTokenAsync(user);
            return new UserRegisterDto
            {
                Username = user.UserName,
                Token = token
            };
        }

        private async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName.Equals(username));
        }

        
    }
}