using System.Security.Claims;
using Domain.Entities;
using Shared.DTOs;

namespace Application.Mappers
{
    public static class UserMapper
    {
        public static UserDto ToDto(this User user)
        {
            if (user == null)
                return null;

            return new UserDto
            {
                UserId = user.Id.ToString(),
                Username = user.Username,
                Email = user.Email,
                Roles = user.UserRoles?.Select(r => r.Role.Name).ToList() ?? new List<string>()
            };
        }

        public static User FromDto(this UserDto dto)
        {
            if (dto == null)
                return null;

            return new User
            {
                Id = Guid.Parse(dto.UserId),
                Username = dto.Username,
                Email = dto.Email,
                // Roles are usually mapped in service layer since EF manages relationships
            };
        }
    }
}