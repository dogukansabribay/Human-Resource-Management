using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
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
    public class CalisanEgitimController : ControllerBase
    {
        private readonly IEgitimBilgiDal _egitimBilgiDal;
        public CalisanEgitimController(IEgitimBilgiDal egitimBilgiDal)
        {
            _egitimBilgiDal = egitimBilgiDal;
        }
        [HttpGet]
        public ActionResult<IEnumerable<EgitimBilgi>> GetEgitimler()
        {
            return _egitimBilgiDal.GetAll();
        }

        // GET: api/Bildirim/5
        [HttpGet("{id}")]
        public ActionResult<EgitimBilgi> GetEgitim(int id)
        {
            var egitim = _egitimBilgiDal.GetById(id);

            if (egitim == null)
            {
                return NotFound();
            }

            return egitim;
        }

        // PUT: api/Bildirim/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public IActionResult PutEgitimBilgi(int id, EgitimBilgi egitim)
        {
            if (id != egitim.EgitimBilgiId)
            {
                return BadRequest();
            }
            try
            {
                _egitimBilgiDal.Update(egitim);
                var updatedBildirim = _egitimBilgiDal.GetById(egitim.EgitimBilgiId);
                return Ok(updatedBildirim);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EgitimBilgiExists(id))
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
        public ActionResult<EgitimBilgi> PostEgitim(EgitimBilgi egitim)
        {
            _egitimBilgiDal.Add(egitim);
            return Ok(egitim);
        }

        // DELETE: api/Bildirim/5
        [HttpDelete("{id}")]
        public ActionResult<EgitimBilgi> DeleteEgitim (int id)
        {
            var egitim = _egitimBilgiDal.GetById(id);
            if (egitim == null)
            {
                return NotFound();
            }

            _egitimBilgiDal.Delete(egitim);
            return egitim;
        }

        private bool EgitimBilgiExists(int id)
        {
            return _egitimBilgiDal.Any(e => e.EgitimBilgiId == id);
        }
    }
}
