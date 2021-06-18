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
    public class CalisanHarcamalarController : ControllerBase
    {
        private readonly ICalisanHarcamaDal _calisanHarcamaDal;
        public CalisanHarcamalarController(ICalisanHarcamaDal calisanHarcamaDal)
        {
            _calisanHarcamaDal = calisanHarcamaDal;
        }

        // GET: api/CalisanHarcamalar
        [HttpGet]
        public ActionResult<IEnumerable<CalisanHarcama>> GetCalisanHarcamalari()
        {
            return _calisanHarcamaDal.GetAll();
        }

        // GET: api/CalisanHarcamalar/5
        [HttpGet("{id}")]
        public ActionResult<CalisanHarcama> GetCalisanHarcama(int id)
        {
            var calisanHarcama = _calisanHarcamaDal.GetById(id);

            if (calisanHarcama == null)
            {
                return NotFound();
            }

            return calisanHarcama;
        }

        // PUT: api/CalisanHarcamalar/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public IActionResult PutCalisanHarcama(int id, CalisanHarcama calisanHarcama)
        {
            if (id != calisanHarcama.CalisanHarcamaID)
            {
                return BadRequest();
            }

            

            try
            {
                _calisanHarcamaDal.Update(calisanHarcama);
                return Ok(calisanHarcama);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalisanHarcamaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/CalisanHarcamalar
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public ActionResult<CalisanHarcama> PostCalisanHarcama(CalisanHarcama calisanHarcama)
        {
            try
            {
                _calisanHarcamaDal.Add(calisanHarcama);
                return Ok(calisanHarcama);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE: api/CalisanHarcamalar/5
        [HttpDelete("{id}")]
        public ActionResult<CalisanHarcama> DeleteCalisanHarcama(int id)
        {
            var calisanHarcama = _calisanHarcamaDal.GetById(id);
            if (calisanHarcama == null)
            {
                return NotFound();
            }
            _calisanHarcamaDal.Delete(calisanHarcama);          
            return calisanHarcama;
        }

        private bool CalisanHarcamaExists(int id)
        {
            return _calisanHarcamaDal.Any(a=>a.CalisanHarcamaID==id);
        }
    }
}
