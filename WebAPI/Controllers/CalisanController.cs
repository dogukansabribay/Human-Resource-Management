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
    public class CalisanController : ControllerBase
    {
        //private readonly KolayIkContext _context;

        //public CalisanController(KolayIkContext context)
        //{
        //    _context = context;
        //}

        private readonly ICalisanDal _calisanDAL;

        public CalisanController(ICalisanDal calisanDAL)
        {
            _calisanDAL = calisanDAL;
        }

        // GET: api/Calisan
        [HttpGet]
        public ActionResult<IEnumerable<Calisan>> GetCalisanlar()
        {
            return _calisanDAL.GetAll();
        }

        // GET: api/Calisan/5
        [HttpGet("{id}")]
        public ActionResult<Calisan> GetCalisan(int id)
        {
            var calisan = _calisanDAL.GetById(id);

            if (calisan == null)
            {
                return NotFound();
            }

            return calisan;
        }

        // PUT: api/Calisan/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public IActionResult PutCalisan(int id, Calisan calisan)
        {
            if (id != calisan.CalisanId)
            {
                return BadRequest();
            }

            try
            {
                _calisanDAL.Update(calisan);
                var updatedCalisan = _calisanDAL.GetById(calisan.CalisanId);
                return Ok(updatedCalisan);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalisanExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

        }

        // POST: api/Calisan
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public ActionResult<Calisan> PostCalisan(Calisan calisan)
        {
            try
            {
                _calisanDAL.Add(calisan);
                return Ok(calisan);
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }

        // DELETE: api/Calisan/5
        [HttpDelete("{id}")]
        public ActionResult<Calisan> DeleteCalisan(int id)
        {
            var calisan = _calisanDAL.GetById(id);
            if (calisan == null)
            {
                return NotFound();
            }
            calisan.AktifMi = false;
            _calisanDAL.Update(calisan);
            var deletedCalisan = _calisanDAL.GetById(calisan.CalisanId);
            return deletedCalisan;
        }

        private bool CalisanExists(int id)
        {
            return _calisanDAL.Any(a => a.CalisanId == id);
        }
    }
}
