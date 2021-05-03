using Business.Abstract_Referans_tutucular;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    //Bir buraya kendi controllerimizi yapıcaz. Bize gelen istekleri duzenliyecegiz.
    //Bu class bir controllerdır dedik.

    //Route..... demek adamlar bana nasıl ulassının karsılıgı. api/controller namesi
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IProductService _productService;
        //Hangi manager oldugubu bilmedigi icin hata verir. ctor ici classın tumunde kullanılamaz.
        //IoC Contanier -- Inversion of Control. Referansları koy isteyen istedigini alsın. Ama startuprdan yap
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            Thread.Sleep(500);
            var result=_productService.GetAll();
            //Succes se ok yani status 200 ve result dondur.
            if (result.Succes)
            {
                return Ok(result);
            }
            //Succes degilse BadRequest yani status 400 ve result
            return BadRequest(result.Message);
        }
        [HttpGet("getById")]
        public IActionResult GetById(int id)    
        {
            var result = _productService.GetById(id);
            if (result.Succes)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getByCategoryId")]
        public IActionResult GetByCategoryId(int categoryId)
        {
            var result = _productService.GetAllByCategoryId(categoryId);
            if (result.Succes)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("Add")]
        //Post sen bana data ver dedin.
        //Sen bana post ver dedin Burada product ver bana dedin. Ama bana product vermedin hayırdır sen?
        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);
            if (result.Succes)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }    
    }
}
