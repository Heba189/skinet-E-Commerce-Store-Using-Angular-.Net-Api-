using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using core.Entities;
using core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;
      public BasketController(IBasketRepository basketRepository)
      {
       _basketRepository = basketRepository; 
      }
      [HttpGet]
      public async Task<ActionResult<CustomerBasket>> GetBasket(string id){
        var basket = await _basketRepository.GetBasketAsync(id);
        return Ok(basket ?? new CustomerBasket(id));
      }
      [HttpPost]
      public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket){
        var upoadedBasket = await _basketRepository.UpdateBasketAsync(basket);
        return Ok(upoadedBasket);
    }
    [HttpDelete]
    public async Task DeleteBasket(string id){
        await _basketRepository.DeleteBasketAsync(id);
    }
}}