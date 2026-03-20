namespace AuditManagement.API.Services.Interfaces;

public interface IUserService
{
    Task CreateUserAsync(CreateUserDto dto);

    Task<UserResponseDto> LoginAsync(LoginDto dto);

    Task UpdateProfileAsync(int userId, UpdateUserDto dto);
}