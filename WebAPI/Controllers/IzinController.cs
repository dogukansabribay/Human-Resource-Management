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
    public class IzinController : ControllerBase
    {
        private readonly IIzinDal _izinDal;
        public IzinController(IIzinDal izindal)
        {
            _izinDal = izindal;
        }
        // GET: api/Izin
        [HttpGet]
        public ActionResult<IEnumerable<Izin>> GetIzinler()
        {
            return _izinDal.GetAll();
        }

        // GET: api/Izin/5
        [HttpGet("{id}")]
        public ActionResult<Izin> GetIzin(int id)
        {
            var izin = _izinDal.GetById(id);

            if (izin == null)
            {
                return NotFound();
            }

            return izin;
        }

        // PUT: api/Izin/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public IActionResult PutIzin(int id, Izin izin)
        {
            if (id != izin.IzinID)
            {
                return BadRequest();
            }

            try
            {
                _izinDal.Update(izin);
                return Ok(izin);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IzinExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/Izin
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public ActionResult<Izin> PostIzin(Izin izin)
        {
            try
            {
                _izinDal.Add(izin);
                return Ok(izin);
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }

        }

        // DELETE: api/Izin/5
        [HttpDelete("{id}")]
        public ActionResult<Izin> DeleteIzin(int id)
        {
            var izin = _izinDal.GetById(id);
            if (izin == null)
            {
                return NotFound();
            }

            _izinDal.Delete(izin);
            return izin;
        }

        private bool IzinExists(int id)
        {
            return _izinDal.Any(a=>a.IzinID==id);
        }
    }
}
