using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SirketController : ControllerBase
    {
        private readonly ISirketDal _sirket;
        public SirketController(ISirketDal sirket)
        {
            _sirket = sirket;
        }

        [HttpPost]
        public ActionResult<Sirket> PostSirket(Sirket sirket)
        {
            try
            {
                _sirket.Add(sirket);
                return Ok(sirket);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet]
        public ActionResult<IEnumerable<Sirket>> GetSirketler()
        {
            return _sirket.GetAll();
        }

        // GET: api/Calisan/5
        [HttpGet("{id}")]
        public ActionResult<Sirket> GetSirket(int id)
        {
            var sirket = _sirket.GetById(id);

            if (sirket == null)
            {
                return NotFound();
            }

            return sirket;
        }

        // PUT: api/Calisan/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public IActionResult PutSirket(int id, Sirket sirket)
        {
            if (id != sirket.SirketId)
            {
                return BadRequest();
            }

            try
            {
                _sirket.Update(sirket);
                var updatedCalisan = _sirket.GetById(sirket.SirketId);
                return Ok(updatedCalisan);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SirketExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

        }
       

        // DELETE: api/Calisan/5
        [HttpDelete("{id}")]
        public ActionResult<Sirket> DeleteSirket(int id)
        {
            var sirket = _sirket.GetById(id);
            if (sirket == null)
            {
                return NotFound();
            }
            _sirket.Delete(sirket);
            _sirket.Update(sirket);
            var deletedCalisan = _sirket.GetById(sirket.SirketId);
            return deletedCalisan;
        }

        private bool SirketExists(int id)
        {
            return _sirket.Any(a => a.SirketId == id);
        }
    }
}
