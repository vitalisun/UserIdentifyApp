using IndentifyApp.DAL.Entities;
using IndentifyApp.DAL;
using Microsoft.AspNetCore.Identity;
using IndentifyApp.BL.Models;

namespace IndentifyApp.BL.Services;

public class UserService : IUserService
{
    private readonly IRepository<User> _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserService(
            IRepository<User> userRepository,
            IPasswordHasher<User> passwordHasher
        )

    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<RegisterResponse> CreateUser(RegisterDto model)
    {
        User? user;
        try
        {
            if (model.Password != model.PasswordConfirm)
                throw new Exception("Пароли не совпадают");

            var users = await _userRepository.GetAllAsync();

            user = users.FirstOrDefault(u => u.Login == model.Login);
            if (user != null)
                throw new Exception("Пользователь с таким логином уже существует");

            var hash = _passwordHasher.HashPassword(user, model.Password);
            user = new User { Login = model.Login, PasswordHash = hash };
            await _userRepository.AddAsync(user);
        }
        catch (Exception e)
        {
            return new RegisterResponse { IsSuccess = false, ErrorMessage = e.Message };
        }

        return new RegisterResponse { IsSuccess = true, Id = user.Id };
    }

    public async Task<User?> GetUserByLogin(string login)
    {
        IReadOnlyList<User?> users = await _userRepository.GetAllAsync();
        return users.FirstOrDefault(u => u.Login == login);
    }

    public async Task<User> GetUserById(int id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task<bool> ValidateCredentials(LoginDto model)
    {
        var users = await _userRepository.GetAllAsync();
        var user = users.FirstOrDefault(u => u.Login == model.Login);
        if (user == null)
            return false;

        if (!_passwordHasher
                .VerifyHashedPassword(user, user.PasswordHash, model.Password)
                .Equals(PasswordVerificationResult.Success))
            return false;

        return true;
    }
}