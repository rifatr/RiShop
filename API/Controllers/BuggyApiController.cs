using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Infra.Data;
using API.Errors;

namespace API.Controllers
{
    public class BuggyApiController(StoreContext context) : BaseApiController
    {
        private readonly StoreContext _context = context;

        [HttpGet("not-found")]
        public ActionResult GetNotFound()
        {
            return NotFound(new ApiResponse(404));
        }

        [HttpGet("server-error")] // null exception
        public ActionResult GetServerError()
        {
            var thing = _context.Products.Find(150);
            var thingToReturn = thing.ToString();

            return Ok();
        }

        [HttpGet("bad-request")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }
        
        [HttpGet("bad-request/{id}")] // validation error
        public ActionResult GetBadRequest(int id)
        {
            return Ok();
        }
    }
}