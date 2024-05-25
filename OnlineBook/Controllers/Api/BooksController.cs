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

        [Route("api/Books/SearchAutoCompleteid")]
        [HttpGet]
        public IHttpActionResult SearchAutoCompleteid(int bid)
        {
            try
            {
                var info = _manager.GetAll().SingleOrDefault(c => c.Id == bid);

                return Ok(info);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("api/Books/Search")]
        [HttpGet]
        public IHttpActionResult Search(string query = null)
      {
            if (!String.IsNullOrWhiteSpace(query))
            {
                var a = _manager.GetAll().Where(c => c.BookTitel.ToLower().Contains(query))
                    .ToList();
                return Ok(a);
            }
            return Ok(0);
        }


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
