using IndentifyApp.BL.Models;
using IndentifyApp.DAL.Entities;

namespace IndentifyApp.BL.Services;

public interface IUserService
{
    /// <summary>
    ///     Создание пользователя
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<RegisterResponse> CreateUser(RegisterDto model);
    
    /// <summary>
    ///    Полуение пользователя по логину
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    Task<User?> GetUserByLogin(string login);

    /// <summary>
    ///     Получение пользователя по id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<User> GetUserById(int id);

    /// <summary>
    ///     Проверка пароля
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<bool> ValidateCredentials(LoginDto model);
}