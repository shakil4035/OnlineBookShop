using Pims.Core.Model;
using Pims.Core.ViewModel;
using Pims.Service.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnlineBook.Controllers.Api
{
    public class PurchesController : ApiController
    {
        public PurchesManager _manager = new PurchesManager();

        // GET: api/Purches
        public IHttpActionResult Get()
        {
            try
            {
                var entites = _manager.GetAll();
                return Ok(entites);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }


        // GET: api/Purches/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                var entity = _manager.GetAll();
                if (entity == null)
                    return NotFound();
                return Ok(entity);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        // POST: api/Purches
        public IHttpActionResult Post([FromBody] PurchesViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(_manager.Add(vm));
                }
                else
                {
                    return BadRequest("Input value is not valid");
                }
            }
            catch (Exception e)
            {

                return Ok(e.Message);
            }
        }

        // PUT: api/Purches/5
        public IHttpActionResult Put(int id, [FromBody] PurchesViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(_manager.Update(id,vm));
                }
                else
                {
                    return BadRequest("Input value is not valid");
                }
            }
            catch (Exception e)
            {

                return Ok(e.Message);
            }
        }

        // DELETE: api/Purches/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                return Ok(_manager.Remove(id));
            }
            catch (Exception e)
            {

                return Ok(e.Message);
            }
        }
        
    }
}
