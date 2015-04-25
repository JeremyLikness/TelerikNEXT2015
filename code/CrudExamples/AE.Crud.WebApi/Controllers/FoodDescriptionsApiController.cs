using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using AA.Crud.Domain;
using AB.Crud.DataAccess;

namespace AE.Crud.WebApi.Controllers
{
    public class FoodDescriptionsApiController : ApiController
    {
        private FoodContext db = new FoodContext();

        // GET: api/FoodDescriptionsApi
        public IQueryable<FoodDescription> GetFoodDescriptions()
        {
            return db.FoodDescriptions;
        }

        // GET: api/FoodDescriptionsApi/5
        [ResponseType(typeof(FoodDescription))]
        public async Task<IHttpActionResult> GetFoodDescription(string id)
        {
            FoodDescription foodDescription = await db.FoodDescriptions.FindAsync(id);
            if (foodDescription == null)
            {
                return NotFound();
            }

            return Ok(foodDescription);
        }

        // PUT: api/FoodDescriptionsApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFoodDescription(string id, FoodDescription foodDescription)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != foodDescription.Number)
            {
                return BadRequest();
            }

            db.Entry(foodDescription).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodDescriptionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/FoodDescriptionsApi
        [ResponseType(typeof(FoodDescription))]
        public async Task<IHttpActionResult> PostFoodDescription(FoodDescription foodDescription)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FoodDescriptions.Add(foodDescription);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FoodDescriptionExists(foodDescription.Number))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = foodDescription.Number }, foodDescription);
        }

        // DELETE: api/FoodDescriptionsApi/5
        [ResponseType(typeof(FoodDescription))]
        public async Task<IHttpActionResult> DeleteFoodDescription(string id)
        {
            FoodDescription foodDescription = await db.FoodDescriptions.FindAsync(id);
            if (foodDescription == null)
            {
                return NotFound();
            }

            db.FoodDescriptions.Remove(foodDescription);
            await db.SaveChangesAsync();

            return Ok(foodDescription);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FoodDescriptionExists(string id)
        {
            return db.FoodDescriptions.Count(e => e.Number == id) > 0;
        }
    }
}