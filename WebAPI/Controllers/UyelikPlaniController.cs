using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using DataAccess.Abstract;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UyelikPlaniController : ControllerBase
    {
        //private readonly KolayIkContext _context;

        //public UyelikPlaniController(KolayIkContext context)
        //{
        //    _context = context;
        //}
        private readonly IUyelikPlaniDal _uyelikPlaniDal;

        public UyelikPlaniController(IUyelikPlaniDal uyelikPlaniDal)
        {
            _uyelikPlaniDal = uyelikPlaniDal;
        }

        // GET: api/UyelikPlani
        [HttpGet]
        public ActionResult<IEnumerable<UyelikPlani>> GetUyelikPlanilari()
        {
            return _uyelikPlaniDal.GetAll();
        }

        // GET: api/UyelikPlani/5
        [HttpGet("{id}")]
        public ActionResult<UyelikPlani> GetUyelikPlani(int id)
        {
            var uyelikPlani = _uyelikPlaniDal.GetById(id);

            if (uyelikPlani == null)
            {
                return NotFound();
            }

            return uyelikPlani;
        }

        // PUT: api/UyelikPlani/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public IActionResult PutUyelikPlani(int id, UyelikPlani uyelikPlani)
        {
            if (id != uyelikPlani.UyelikPlaniID)
            {
                return BadRequest();
            }


            try
            {
                _uyelikPlaniDal.Update(uyelikPlani);
                var update = _uyelikPlaniDal.GetById(uyelikPlani.UyelikPlaniID);
                return Ok(update);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UyelikPlaniExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

        }

        // POST: api/UyelikPlani
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public ActionResult<UyelikPlani> PostUyelikPlani(UyelikPlani uyelikPlani)
        {

            _uyelikPlaniDal.Add(uyelikPlani);
            return Ok(uyelikPlani);
        }

        // DELETE: api/UyelikPlani/5
        [HttpDelete("{id}")]
        public ActionResult<UyelikPlani> DeleteUyelikPlani(int id)
        {
            var uyelikPlani = _uyelikPlaniDal.GetById(id);
            if (uyelikPlani == null)
            {
                return NotFound();
            }

            _uyelikPlaniDal.Delete(uyelikPlani);
            return uyelikPlani;
        }

        private bool UyelikPlaniExists(int id)
        {
            return _uyelikPlaniDal.Any(e => e.UyelikPlaniID == id);
        }
    }
}
