using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vidly.Database;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CustomersController(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public ActionResult<IEnumerable<CustomerDto>> GetCustomers()
        {
            var customers = _dbContext.Customers
                .Include(c => c.MembershipType)
                .ToList()
                .Select(_mapper.Map<CustomerDto>);

            return Ok(customers);
        }

        [HttpGet("{id:int}")]
        public ActionResult<CustomerDto> GetCustomer(int id)
        {
            var customer = _dbContext.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == default)
                return NotFound();

            return Ok(_mapper.Map<CustomerDto>(customer));
        }

        [HttpPost]
        public ActionResult<CustomerDto> CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customer = _mapper.Map<Customer>(customerDto);

            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();

            customerDto.Id = customer.Id;

            return CreatedAtAction(
                actionName: nameof(GetCustomer),
                routeValues: new { id = customer.Id },
                value: customerDto);
        }

        [HttpPut("{id:int}")]
        public ActionResult<CustomerDto> UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customerInDb = _dbContext.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == default)
                return NotFound();

            _mapper.Map(customerDto, customerInDb);

            _dbContext.Customers.Update(customerInDb);
            _dbContext.SaveChanges();

            customerDto.Id = id;

            return Ok(customerDto);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteCustomer(int id)
        {
            var customer = _dbContext.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == default)
                return NotFound();

            _dbContext.Customers.Remove(customer);
            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}