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
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using AA.Crud.Domain;
using AB.Crud.DataAccess;

namespace AE.Crud.WebApi.Controllers
{
    public class FoodDescriptionsController : ODataController
    {
        private FoodContext db = new FoodContext();

        // GET: odata/FoodDescriptions
        [EnableQuery]
        public IQueryable<FoodDescription> GetFoodDescriptions()
        {
            return db.FoodDescriptions;
        }

        // GET: odata/FoodDescriptions(5)
        [EnableQuery]
        public SingleResult<FoodDescription> GetFoodDescription([FromODataUri] string key)
        {
            return SingleResult.Create(db.FoodDescriptions.Where(foodDescription => foodDescription.Number == key));
        }

        // PUT: odata/FoodDescriptions(5)
        public async Task<IHttpActionResult> Put([FromODataUri] string key, Delta<FoodDescription> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            FoodDescription foodDescription = await db.FoodDescriptions.FindAsync(key);
            if (foodDescription == null)
            {
                return NotFound();
            }

            patch.Put(foodDescription);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodDescriptionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(foodDescription);
        }

        // POST: odata/FoodDescriptions
        public async Task<IHttpActionResult> Post(FoodDescription foodDescription)
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

            return Created(foodDescription);
        }

        // PATCH: odata/FoodDescriptions(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] string key, Delta<FoodDescription> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            FoodDescription foodDescription = await db.FoodDescriptions.FindAsync(key);
            if (foodDescription == null)
            {
                return NotFound();
            }

            patch.Patch(foodDescription);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodDescriptionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(foodDescription);
        }

        // DELETE: odata/FoodDescriptions(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] string key)
        {
            FoodDescription foodDescription = await db.FoodDescriptions.FindAsync(key);
            if (foodDescription == null)
            {
                return NotFound();
            }

            db.FoodDescriptions.Remove(foodDescription);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FoodDescriptionExists(string key)
        {
            return db.FoodDescriptions.Count(e => e.Number == key) > 0;
        }
    }
}
