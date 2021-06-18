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
    public class BildirimController : ControllerBase
    {
        //private readonly KolayIkContext _context;

        //public BildirimController(KolayIkContext context)
        //{
        //    _context = context;
        //}

        private readonly IBildirimDal _bildirimDal;
        public BildirimController(IBildirimDal bildirimDal)
        {
            _bildirimDal = bildirimDal;
        }
        // GET: api/Bildirim
        [HttpGet]
        public ActionResult<IEnumerable<Bildirim>> GetBildirimler()
        {
            return _bildirimDal.GetAll();
        }

        // GET: api/Bildirim/5
        [HttpGet("{id}")]
        public ActionResult<Bildirim> GetBildirim(int id)
        {
            var bildirim = _bildirimDal.GetById(id);

            if (bildirim == null)
            {
                return NotFound();
            }

            return bildirim;
        }

        // PUT: api/Bildirim/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public IActionResult PutBildirim(int id, Bildirim bildirim)
        {
            if (id != bildirim.BildirimId)
            {
                return BadRequest();
            }
            try
            {
                _bildirimDal.Update(bildirim);
                var updatedBildirim = _bildirimDal.GetById(bildirim.BildirimId);
                return Ok(updatedBildirim);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BildirimExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

        }

        // POST: api/Bildirim
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public ActionResult<Bildirim> PostBildirim(Bildirim bildirim)
        {
            _bildirimDal.Add(bildirim);
            return Ok(bildirim);
        }

        // DELETE: api/Bildirim/5
        [HttpDelete("{id}")]
        public ActionResult<Bildirim> DeleteBildirim(int id)
        {
            var bildirim = _bildirimDal.GetById(id);
            if (bildirim == null)
            {
                return NotFound();
            }

            _bildirimDal.Delete(bildirim);
            return bildirim;
        }

        private bool BildirimExists(int id)
        {
            return _bildirimDal.Any(e => e.BildirimId == id);
        }
    }
}
