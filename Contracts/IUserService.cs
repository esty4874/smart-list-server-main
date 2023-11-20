namespace SmartList.Contracts
{
    public interface IUserService
    {
        Task<UserDto> CreateUser(UserDto user);
        Task<UserDto?> Login(string email);


    }
}
