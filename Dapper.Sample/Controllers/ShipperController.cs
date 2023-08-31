using Dapper.Sample.Entities;
using Dapper.Sample.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dapper.Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipperController : ControllerBase
    {
        private IShipperDAL service;
        public ShipperController(IShipperDAL service)
        {
            this.service = service;
        }

        [HttpGet("")]
        public ActionResult Get()
        {
            var shipper = service.GetAll();
            return Ok(shipper);
        }

        [HttpGet("~/getByIdShipper/ID")]
        public ActionResult GetById(int id)
        {
            var shipper = service.GetById(id);
            if (shipper is null)
            {
                return NotFound();
            }
            return Ok(shipper);           
        }

        [HttpDelete("~/deleteShipper/ID")]
        public ActionResult Delete(int id)
        {
            service.DeleteById(id);
            return Ok(id + "has been deleted");
        }

        [HttpPost("~/addShipper")]
        public ActionResult Post(Shipper s)
        {
            var newShipper = service.Add(s);
            return Ok("The new shipper is added");
        }

        [HttpPut("~/updateShipper")]
        public ActionResult Put( int id, Shipper shipper)
        {
            service.Update(id, shipper);
            return Ok(id + "has been updated");
        }
    }
}
