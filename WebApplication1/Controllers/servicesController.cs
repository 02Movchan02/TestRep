using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class servicesController : ApiController
    {
        private clinicEntities2 db = new clinicEntities2();

        // GET: api/services
        public IQueryable<services> Getservices()
        {
            return db.services;
        }

        // GET: api/services/5
        [ResponseType(typeof(services))]
        public IHttpActionResult Getservices(int id)
        {
            services services = db.services.Find(id);
            if (services == null)
            {
                return NotFound();
            }

            return Ok(services);
        }

        // PUT: api/services/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putservices(int id, services services)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != services.id)
            {
                return BadRequest();
            }

            db.Entry(services).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!servicesExists(id))
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

        // POST: api/services
        [ResponseType(typeof(services))]
        public IHttpActionResult Postservices(services services)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.services.Add(services);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (servicesExists(services.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = services.id }, services);
        }

        // DELETE: api/services/5
        [ResponseType(typeof(services))]
        public IHttpActionResult Deleteservices(int id)
        {
            services services = db.services.Find(id);
            if (services == null)
            {
                return NotFound();
            }

            db.services.Remove(services);
            db.SaveChanges();

            return Ok(services);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool servicesExists(int id)
        {
            return db.services.Count(e => e.id == id) > 0;
        }
    }
}