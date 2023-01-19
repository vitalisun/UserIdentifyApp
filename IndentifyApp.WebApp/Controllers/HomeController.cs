using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using IndentifyApp.WebApp.Models;
using IndentifyApp.BL.Services;

namespace IndentifyApp.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService) => _userService = userService;

        /// <summary>
        ///     Страница регистрации по дефолтному пути
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            return View();
        }

        /// <summary>
        ///     Страница заполнения данных пользователя для идентификации
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("home/profile/{id:int}")]
        public async Task<IActionResult> Profile(int id)
        {
            var model = await _userService.GetUserById(id);
            return View(new IndexViewModel { Login = model.Login });
        }


       
    }
}
