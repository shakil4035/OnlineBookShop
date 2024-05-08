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
    public class SelersController : ApiController
    {
        public SellerManager _manager = new SellerManager();

        [Route("api/Selers/SearchAutoComplete")]
        [HttpGet]
        public IHttpActionResult SearchAutoComplete(string pNumber)
        {
            try
            {
                var info = _manager.GetAll().SingleOrDefault(c => c.IdNo == pNumber);

                return Ok(info);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("api/Selers/Search")]
        [HttpGet]
        public IHttpActionResult Search(string query = null)
        {
            if (!String.IsNullOrWhiteSpace(query))
            {
                var a = _manager.GetAll().Where(c => c.IdNo.ToLower().Contains(query) || c.Name.ToLower().Contains(query))
                    .ToList();
                return Ok(a);
            }
            return Ok(0);
        }

        [Route("api/Selers/CreateSellerId")]
        [HttpGet]
        public IHttpActionResult CreateSellerId()
        {

            try
            {
                var entities = _manager.CreateSellerId();
                return Ok(entities);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("api/Selers/GetInfoById")]
        [HttpGet]
        public IHttpActionResult GetInfoById(int id)
        {
            try
            {
                var entities = _manager.GetAll().Where(c => c.Id == id).ToList();
                return Ok(entities);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: api/Selers
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

        // GET: api/Selers/5
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

        // POST: api/Selers
        public IHttpActionResult Post([FromBody] SellerViewModel vm)
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

        // PUT: api/Selers/5
        public IHttpActionResult Put(int id, [FromBody] SellerViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(_manager.Update(id, vm));
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

        // DELETE: api/Selers/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                return Ok(_manager.remove(id));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
