using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Webapi.Models;

namespace Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly RestaurantDataContext _context;

        public CustomerController(RestaurantDataContext context)
        {
            _context = context;
        }
        // GET: api/Customer
        [HttpGet]
        public IActionResult GetCustomer()
        {
            try
            {
                var customers = from o in _context.Customers
                             select o;

                var customer = _context.Customers.Include(o => o.Addresses).ToList();
                return Ok(customer);
                      
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: api/Customer/5
        [HttpGet("{id}")]

        public async Task<ActionResult<Customer>> GetCustomerById(int id)
        {
          
             var customer = await _context.Customers.Include(a => a.Addresses).FirstOrDefaultAsync(i => i.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Customer/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
           
            try
            {

                var cust = _context.Customers.Where(p => p.CustomerId == customer.CustomerId).Include(p => p.Addresses).SingleOrDefault();

                if (cust != null)
                {
                    //update customer
                    _context.Entry(cust).CurrentValues.SetValues(customer);
                    // Delete address
                    foreach (var existingCustomer in cust.Addresses.ToList())
                    {
                        if (!customer.Addresses.Any(c => c.AddressId == existingCustomer.AddressId))
                            _context.addresses.Remove(existingCustomer);
                    }
                 


                    //update and insert address
                    foreach (var customerAddress in customer.Addresses)
                    {
                        var address_up = cust.Addresses.Where(c => c.AddressId == customerAddress.AddressId).SingleOrDefault();

                        if (address_up != null)
                        {
                            //update address
                            _context.Entry(address_up).CurrentValues.SetValues(customerAddress);
                        }
                        else
                        {
                            //insert address
                            var newitem = new Address
                            {
                                AddressId = customerAddress.AddressId,
                                AddressType = customerAddress.AddressType,
                                Street1 = customerAddress.Street1,
                                Street2 = customerAddress.Street2,
                                City = customerAddress.City,
                                District = customerAddress.District,
                                PinCode = customerAddress.PinCode
                            };
                             cust.Addresses.Add(newitem);
                        }
                       
                    }

                    await _context.SaveChangesAsync();
                }

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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
        // POST: api/Customer
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
        }

      
        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> DeleteCustomer(int id)
        {
            var customer = _context.Customers.Where(p => p.CustomerId == id).Include(p => p.Addresses).SingleOrDefault();

            if (customer == null)
            {
                return NotFound();
            }

            foreach (var child in customer.Addresses.ToList())
            {
                if (customer.Addresses.Any(c => c.AddressId == child.AddressId))
                    _context.addresses.Remove(child);
            }
            
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return customer;
        }
        
        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }
    }
}
