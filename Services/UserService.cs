using System.Net.Http.Headers;

namespace SmartList.Services
{
    public class UserService : IUserService
    {
        private readonly SmartListContext _context;
        public UserService(SmartListContext context)
        {
            _context = context;
        }

        public async Task<UserDto?> Login(string email)
        {
            var user = await _context.Users.FirstAsync(u=>u.Email==email);
            if (user != null)
            {
                return new UserDto()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                };
            }
            return null;
        }
        public async Task<UserDto> CreateUser(UserDto userDto)
        {
            var result = _context.Users.Add(new User()
            {
                UserName = userDto.UserName,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email
            });
            var user = result.Entity;
            await _context.SaveChangesAsync();
            return new UserDto()
            {
                Id = user.Id,
                UserName=user.UserName,
                FirstName=user.FirstName,
                LastName=user.LastName,
                Email=user.Email
            };
        }
    }
}
