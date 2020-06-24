using System;
using System.Collections.Generic;
using System.Linq;
using Kitchen.Api.Data;
using Kitchen.Api.Models;
using Kitchen.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Kitchen.Api.Controllers
{
    
    [Route("api/[controller]")]
    public class InventoryController : Controller    
    {

        private readonly ICupcakeRepository _cupcakeRepository;


        public InventoryController(ICupcakeRepository cupcakeRepository){
            _cupcakeRepository = cupcakeRepository ?? throw new ArgumentNullException(nameof(cupcakeRepository));

        }



        [HttpGet]
        public IActionResult GetItems(){
            return Ok(_cupcakeRepository.GetCupcakes());
            
        }

        [HttpGet("{id}" ,Name="GetCupcake")]
        public IActionResult GetItem(int id){

            var itemToReturn = _cupcakeRepository.GetCupcake(id);

            if(itemToReturn == null) {
                //_logger.LogInformation($"Cupcake with id {id} not found in the inventory");
                return NotFound();
                }

            return Ok(itemToReturn);

            

        }

        // [HttpPost]
        // public IActionResult AddCupCake([FromBody] CupcakesForCreation cupcake){
        //     if(cupcake == null) return BadRequest();
        //     var maxItemid = CupcakeDatastore.Current.Cupcakes.Max(
        //         c => c.Id
        //     );
        //     var newCupcake = new CupcakeDto(){
        //         Id = ++maxItemid,
        //         Name = cupcake.Name,
        //         Description = cupcake.Description,
        //         Image = cupcake.Image,
        //         Price = cupcake.Price
        //     };
        //     CupcakeDatastore.Current.Cupcakes.Add(newCupcake);

        //     return CreatedAtRoute("GetCupcake",new {id = newCupcake.Id}, newCupcake);
            
        // }
        
    }
}