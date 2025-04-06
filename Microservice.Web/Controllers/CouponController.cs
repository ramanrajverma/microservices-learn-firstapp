using Microservice.Web.Models;
using Microservice.Web.Service;
using Microservice.Web.Service.IService;
using Microservices.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Microservice.Web.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;
        public CouponController(ICouponService couponService)
        {
                _couponService = couponService;
        }

        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDto>? list = new();
            //var test = await _couponService.GetAllCouponAsync();
            ResponseDto? response = await _couponService.GetAllCouponAsync();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = (response?.Message != null) ? response.Message : "Error while loading coupons";
            }
            return View(list); // Pass the list to the view
        }

        public async Task<IActionResult> CouponCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CouponCreate(CouponDto model)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _couponService.CreateCouponAsync(model);
                if (response != null && response.IsSuccess)
                {
                   return RedirectToAction(nameof(CouponIndex));
                }
                else
                {
                    TempData["error"] = (response?.Message != null) ? response.Message : "Error while doing update";
                }
            }
            return View(model);
        }

        public async Task<IActionResult> CouponDelete(int couponId)
        {
            ResponseDto? response = await _couponService.DeleteCouponAsync(couponId);
            {
                if (response != null && response.IsSuccess)
                { 
                    return RedirectToAction(nameof(CouponIndex));
                }
                else
                {
                    TempData["error"] = (response?.Message != null) ? response.Message : "Error while doing delete";
                }
                return View();
            }
        }

    }
}
