using API.DTO;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserRepository(DataContext context, IMapper mapper)
        {
             _context = context;
             _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetUserDtoAsync()
        {
           return await _context.Users 
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            var user =await _context.Users.Include(b =>b.Books).SingleOrDefaultAsync(x => x.Id == id);

            return user;
            //return await _context.Users.Include(b =>b.Books).SingleOrDefaultAsync(x => x.Id == id);
            /*
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == id);
            await _context.Entry(user).Reference(b => b.Books).LoadAsync();
            await _context.Entry(user).Collection(b => b.Books).LoadAsync();
            return user;
            */

            //return await _context.Users.Include("Books").SingleOrDefaultAsync(x => x.Id == id);
            

        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.Include(b =>b.Books).SingleOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _context.Users.Include(b =>b.Books).ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}