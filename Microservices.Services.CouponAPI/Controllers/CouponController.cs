using AutoMapper;
using Azure;
using Microservices.Services.CouponAPI.Data;
using Microservices.Services.CouponAPI.Models;
using Microservices.Services.CouponAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Services.CouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMapper _mapper;

        public CouponController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _response = new ResponseDto();
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCoupons()
        {
            return Ok(_db.Coupons.ToList());
        }


        [HttpGet("all-coupons")]
        public object GetAllCoupons()   // with automapper and dto
        {
            try 
            {
                IEnumerable<Coupon> objList = _db.Coupons.ToList();
                _response.Result = _mapper .Map<IEnumerable<CouponDto>>(objList);
            }
            catch(Exception ex)
            {
               _response.IsSuccess = false;
               _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("{id:int}")]
        public object GetAllCoupons(int id) // with automapper and dto
        {
            try
            {
                Coupon couponObj = _db.Coupons.First(x => x.CouponId == id);
                _response.Result =_mapper.Map<CouponDto>(couponObj);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpGet]
        [Route("GetByCouponCode/{code}")]
        public object GetByCouponsCode(string code) // with automapper and dto
        {
            try
            {
                //Coupon couponObj = _db.Coupons.First(x => x.CouponCode == code.ToLower());  // Then on null control ll jump to exception state
                Coupon couponObj = _db.Coupons.FirstOrDefault(x => x.CouponCode == code.ToLower());
                //Coupon couponObj = _db.Coupons.FirstOrDefault(x => x.CouponCode.Equals(code, StringComparison.CurrentCultureIgnoreCase));
                if (couponObj == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Coupon not found";
                    return _response;
                }
                _response.Result = _mapper.Map<CouponDto>(couponObj);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpPost]
        [Route("CreateCoupon")]
        public ResponseDto Post([FromBody] CouponDto couponDto)
        {
            try 
            {
                Coupon couponObj = _mapper.Map<Coupon>(couponDto);
                _db.Coupons.Add(couponObj);
                _db.SaveChanges();

                _response.Result = _mapper.Map<CouponDto>(couponObj);

            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpPut]
        [Route("ModifyCoupon")]
        public ResponseDto Put([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon couponObj = _mapper.Map<Coupon>(couponDto);
                _db.Coupons.Update(couponObj);
                _db.SaveChanges();

                _response.Result = _mapper.Map<CouponDto>(couponObj);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete]
        //[Route("RemoveCoupon/{id}")]
        public ResponseDto Delete(int id)
        {
            try
            {
                Coupon couponObj = _db.Coupons.First(i => i.CouponId == id);

                _db.Coupons.Remove(couponObj);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
