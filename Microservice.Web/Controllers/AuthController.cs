﻿using Microservice.Web.Models.Dto;
using Microservice.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
               _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDto loginRequestDto = new();
            return View(loginRequestDto);
        }

        [HttpPost]
        public IActionResult Register()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Logout()
        {
            return View();
        }
    }
}
