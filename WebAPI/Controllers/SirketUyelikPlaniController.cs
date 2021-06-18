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
    public class SirketUyelikPlaniController : ControllerBase
    {
        private readonly ISirketUyelikPlaniDal _sirketUyelikPlaniDal;
        public SirketUyelikPlaniController(ISirketUyelikPlaniDal sirketUyelikPlaniDal)
        {
            _sirketUyelikPlaniDal = sirketUyelikPlaniDal;
        }
        [HttpGet]
        public ActionResult<IEnumerable<SirketUyelikPlani>> GetPlanlar()
        {
            return _sirketUyelikPlaniDal.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<SirketUyelikPlani> GetPlan(int id)
        {
            var plan = _sirketUyelikPlaniDal.GetById(id);

            if (plan == null)
            {
                return NotFound();
            }

            return plan;
        }
      
        [HttpPut("{id}")]
        public IActionResult PutPlan(int id, SirketUyelikPlani plan)
        {
            if (id != plan.SirketUyelikPlaniID)
            {
                return BadRequest();
            }
            try
            {
                _sirketUyelikPlaniDal.Update(plan);
                var updatedPlan = _sirketUyelikPlaniDal.GetById((int)plan.SirketUyelikPlaniID);
                return Ok(updatedPlan);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlanExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

        }

     
        [HttpPost]
        public ActionResult<SirketUyelikPlani> PostPlan(SirketUyelikPlani plan)
        {
            _sirketUyelikPlaniDal.Add(plan);
            return Ok(plan);
        }

       
        [HttpDelete("{id}")]
        public ActionResult<SirketUyelikPlani> DeletePlan(int id)
        {
            var plan = _sirketUyelikPlaniDal.GetById(id);
            if (plan == null)
            {
                return NotFound();
            }

            _sirketUyelikPlaniDal.Delete(plan);
            return plan;
        }

        private bool PlanExists(int id)
        {
            return _sirketUyelikPlaniDal.Any(e => e.SirketUyelikPlaniID == id);
        }
    }
}
