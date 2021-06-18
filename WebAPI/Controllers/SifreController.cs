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
    public class SifreController : ControllerBase
    {
        private readonly ISifreDal _sifreDal;
        public SifreController(ISifreDal sifreDal)
        {
            _sifreDal = sifreDal;
        }

        // GET: api/Sifre
        [HttpGet]
        public ActionResult<IEnumerable<Sifre>> GetSifreler()
        {
            return _sifreDal.GetAll();
        }

        // GET: api/Sifre/5
        [HttpGet("{id}")]
        public ActionResult<Sifre> GetSifre(int id)
        {
            var sifre = _sifreDal.GetById(id);

            if (sifre == null)
            {
                return NotFound();
            }

            return sifre;
        }

        // PUT: api/Sifre/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public IActionResult PutSifre(int id, Sifre sifre)
        {
            if (id != sifre.SifreId)
            {
                return BadRequest();
            }
            try
            {
                _sifreDal.Update(sifre);
                return Ok(sifre);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SifreExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/Sifre
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public  ActionResult<Sifre> PostSifre(Sifre sifre)
        {
            try
            {
                _sifreDal.Add(sifre);
                return Ok(sifre);
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }

        // DELETE: api/Sifre/5
        [HttpDelete("{id}")]
        public ActionResult<Sifre> DeleteSifre(int id)
        {
            var sifre = _sifreDal.GetById(id);
            if (sifre == null)
            {
                return NotFound();
            }

            _sifreDal.Delete(sifre);

            return sifre;
        }

        private bool SifreExists(int id)
        {
            return _sifreDal.Any(a=>a.SifreId==id);
        }
    }
}
