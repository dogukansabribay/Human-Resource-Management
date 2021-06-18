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
    public class CalisanAvansController : ControllerBase
    {
        //private readonly KolayIkContext _context;

        //public CalisanAvansController(KolayIkContext context)
        //{
        //    _context = context;
        //}
        private readonly ICalisanAvansDal _calisanAvansDal;
        public CalisanAvansController(ICalisanAvansDal calisanAvansDal)
        {
            _calisanAvansDal = calisanAvansDal;
        }

        // GET: api/CalisanAvans
        [HttpGet]
        public  ActionResult<IEnumerable<CalisanAvans>> GetCalisanAvanslar()
        {
            return _calisanAvansDal.GetAll();
        }

        // GET: api/CalisanAvans/5
        [HttpGet("{id}")]
        public ActionResult<CalisanAvans> GetCalisanAvans(int id)
        {
            var calisanAvans =_calisanAvansDal.GetById(id);

            if (calisanAvans == null)
            {
                return NotFound();
            }

            return calisanAvans;
        }

        // PUT: api/CalisanAvans/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public IActionResult PutCalisanAvans(int id, CalisanAvans calisanAvans)
        {
            if (id != calisanAvans.CalisanAvansID)
            {
                return BadRequest();
            }

            try
            {
                _calisanAvansDal.Update(calisanAvans);
                var updatedAvans = _calisanAvansDal.GetById(calisanAvans.CalisanAvansID);
                return Ok(updatedAvans);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalisanAvansExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

        }

        // POST: api/CalisanAvans
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public ActionResult<CalisanAvans> PostCalisanAvans(CalisanAvans calisanAvans)
        {
            _calisanAvansDal.AvansTalepTarihKontrol(calisanAvans.AvansTarihi);
            _calisanAvansDal.DefaultAyarlar(calisanAvans);
            _calisanAvansDal.Add(calisanAvans);
            return Ok(calisanAvans);
        }

        // DELETE: api/CalisanAvans/5
        [HttpDelete("{id}")]
        public ActionResult<CalisanAvans> DeleteCalisanAvans(int id)
        {
            var calisanAvans = _calisanAvansDal.GetById(id);
            if (calisanAvans == null)
            {
                return NotFound();
            }
            _calisanAvansDal.Delete(calisanAvans);
            return calisanAvans;
        }

        private bool CalisanAvansExists(int id)
        {
            return _calisanAvansDal.Any(a=>a.CalisanAvansID==id);
        }
    }
}
