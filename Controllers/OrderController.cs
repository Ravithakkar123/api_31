using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Webapi.Models;

namespace Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly RestaurantDataContext _context;

        public OrderController(RestaurantDataContext context)
        {
            _context = context;
        }

        // GET: api/Order
        [HttpGet]
        public IActionResult GetOrders()
        {
                try
                {
                var orders = from o in _context.orders
                             select o;

                 var order = _context.orders.Include(o => o.OrderItem).Include(x=> x.customer).ToList();
                return Ok(order);
           
               }
                catch
                {
                return BadRequest();
                }
        }
   
        // GET: api/Order/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Order/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {

            try
            {

                var ord = _context.orders.Where(p => p.OrderId == order.OrderId).Include(p => p.OrderItem).SingleOrDefault();
                           
                if (ord != null)
                {
                      //update order
                     _context.Entry(ord).CurrentValues.SetValues(order);
                    // Delete children
                    foreach (var existingOrdItem in ord.OrderItem.ToList())
                    {
                        if (!order.OrderItem.Any(c => c.OrderItemId == existingOrdItem.OrderItemId))
                            _context.OrderItems.Remove(existingOrdItem);
                    }

                    //update and insert orderitem
                    foreach (var orderItemModel in order.OrderItem)
                {
                    var ordItem = ord.OrderItem.Where(c => c.OrderItemId == orderItemModel.OrderItemId).SingleOrDefault();

                    if (ordItem != null)
                    {
                        //update orderitem
                        _context.Entry(ordItem).CurrentValues.SetValues(orderItemModel);
                    }
                    else
                    {
                        //insert orderitem
                        var newitem = new OrderItem
                        {
                            OrderItemId = orderItemModel.OrderItemId,
                            Quantity = orderItemModel.Quantity,
                            Amount = orderItemModel.Amount,
                            FoodItemId = orderItemModel.FoodItemId,
                                                       
                        };
                        ord.OrderItem.Add(newitem);
                    }

                 }

                    await _context.SaveChangesAsync();
                }
                  
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }
       

        // POST: api/Order
         [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _context.orders.Add(order);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.OrderId }, order);
        }

        // DELETE: api/Order/5
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var result = _context.orders.Find(id);
            if (result == null)
            {
                return NotFound();
            }

            _context.orders.Remove(result);

            try
            {
                _context.SaveChanges();
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        private bool OrderExists(int id)
        {
            return _context.orders.Any(e => e.OrderId == id);
        }
    }
}
