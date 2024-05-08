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
    public class BooksController : ApiController
    {

        public BookManager _manager = new BookManager();
        // GET: api/Books
        public IHttpActionResult Get()
        {
            try
            {
                var enitiy = _manager.GetAll();
                return Ok(enitiy);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        // GET: api/Books/5
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

        // POST: api/Books
        public IHttpActionResult Post([FromBody] BookViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   return Ok(_manager.Add(vm));
                }
                else
                {
                    return BadRequest("Input is Not Valid");
                }
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        // PUT: api/Books/5
        public IHttpActionResult Put(int id, [FromBody] BookViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(_manager.Update(id,vm));
                }
                else
                {
                    return BadRequest("Upadate is Not Success");
                }
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        // DELETE: api/Books/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var enitiy = _manager.Remove(id);
                return Ok(enitiy);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
