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
    public class KullaniciYorumuController : ControllerBase
    {
        private readonly IKullaniciYorumuDal _kullaniciYorumuDal; 
        public KullaniciYorumuController(IKullaniciYorumuDal kullaniciYorumuDal)
        {
            _kullaniciYorumuDal = kullaniciYorumuDal;
        }

        // GET: api/KullaniciYorumu
        [HttpGet]
        public ActionResult<IEnumerable<KullaniciYorumu>> GetKullaniciYorumlari()
        {
            return _kullaniciYorumuDal.GetAll();
        }

        // GET: api/KullaniciYorumu/5
        [HttpGet("{id}")]
        public ActionResult<KullaniciYorumu> GetKullaniciYorumu(int id)
        {
            var kullaniciYorumu =_kullaniciYorumuDal.GetById(id);

            if (kullaniciYorumu == null)
            {
                return NotFound();
            }

            return kullaniciYorumu;
        }

        // PUT: api/KullaniciYorumu/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKullaniciYorumu(int id, KullaniciYorumu kullaniciYorumu)
        {
            if (id != kullaniciYorumu.KullaniciYorumuId)
            {
                return BadRequest();
            }

            try
            {
                _kullaniciYorumuDal.Update(kullaniciYorumu);
                var update = _kullaniciYorumuDal.GetById(kullaniciYorumu.CalisanId);
                return Ok(update);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KullaniciYorumuExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/KullaniciYorumu
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public ActionResult<KullaniciYorumu> PostKullaniciYorumu(KullaniciYorumu kullaniciYorumu)
        {
            _kullaniciYorumuDal.Add(kullaniciYorumu);
            return Ok(kullaniciYorumu);
        }

        // DELETE: api/KullaniciYorumu/5
        [HttpDelete("{id}")]
        public ActionResult<KullaniciYorumu> DeleteKullaniciYorumu(int id)
        {
            var kullaniciYorumu = _kullaniciYorumuDal.GetById(id);
            if (kullaniciYorumu == null)
            {
                return NotFound();
            }

            _kullaniciYorumuDal.Delete(kullaniciYorumu);
            return kullaniciYorumu;
        }

        private bool KullaniciYorumuExists(int id)
        {
            return _kullaniciYorumuDal.Any(a=> a.KullaniciYorumuId==id);
        }
    }
}
