using Application.UserApp.UserDtos;

namespace API.Services
{
    public interface IJwtService
    {
        string GenerateJwt(UserDto user);
    }
}
