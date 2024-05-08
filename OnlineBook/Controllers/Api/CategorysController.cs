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
    public class CategorysController : ApiController
    {
      CategoryManager _manager = new CategoryManager();
        // GET: api/Categorys
        public IHttpActionResult Get()
        {
            try
            {
                var entities = _manager.GetAll();
                return Ok(entities);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        // GET: api/Categorys/5
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

        // POST: api/Categorys
        public IHttpActionResult Post([FromBody] CategotyViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(_manager.Add(vm));
                }
                else
                {
                    return BadRequest("Input data is not valid");
                }
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        //PUT: api/Categorys/5
        public IHttpActionResult Put(int id, [FromBody]CategotyViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(_manager.Update(id,vm));
                }
                else
                {
                    return BadRequest("Input data is not valid");
                }
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        // DELETE: api/Categorys/5
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
