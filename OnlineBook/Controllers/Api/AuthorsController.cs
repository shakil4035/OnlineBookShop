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
    public class AuthorsController : ApiController
    {
        AuthorManager _manager = new AuthorManager();
        // GET: api/Authors
        public IHttpActionResult Get()
        {
            try
            {
                var enities = _manager.GetAll();
                return Ok(enities);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        // GET: api/Authors/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                var entity = _manager.Get(id);
                if (entity == null)
                    return NotFound();
                return Ok(entity);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        // POST: api/Authors
        public IHttpActionResult Post([FromBody] AuthorViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(_manager.Add(vm));
                }
                else
                {
                    return BadRequest("Input Data is valid");
                }
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        // PUT: api/Authors/5
        public IHttpActionResult Put(int id, [FromBody]AuthorViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(_manager.Update(id, vm));
                }
                else
                {
                    return BadRequest("Input Data is valid");
                }
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        // DELETE: api/Authors/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                return Ok(_manager.Remove(id));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
