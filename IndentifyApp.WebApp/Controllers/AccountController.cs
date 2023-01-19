using AutoMapper;
using IndentifyApp.BL.Models;
using IndentifyApp.BL.Services;
using IndentifyApp.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IndentifyApp.WebApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public AccountController(
        IUserService userService,
        IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    /// <summary>
    ///     Регистрация пользователя
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<RegisterResponse> Register([FromBody] RegisterViewModel model)
    {
        var createUserResponse = await _userService.CreateUser(_mapper.Map<RegisterDto>(model));

        if (createUserResponse.IsSuccess)
        {
            return new RegisterResponse { IsSuccess = true, Id = createUserResponse.Id };
        }

        return new RegisterResponse { IsSuccess = false, ErrorMessage = createUserResponse.ErrorMessage };
    }
}