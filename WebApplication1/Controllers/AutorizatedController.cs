using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Routing;

namespace WebApplication1.Controllers
{
    public class AutorizatedController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> manager;

        public AutorizatedController()
        {
            manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }
        // GET: /Autorizated/
        [Authorize]
        public ActionResult Index()
        {
            ApplicationUser currentuser = manager.FindByName(User.Identity.GetUserName());
            return View(db.post.ToList());
        }

        // GET: /Autorizated/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await db.post.FindAsync(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            TempData["postid"] = post.id;
            
            return Redirect("/Discussion/Index/"+post.id);
        }

        [Authorize]
        public ActionResult Redact()
        {

            Response.Redirect("/Page");
            return null;
        }
        // GET: /Autorizated/Create


        // POST: /Autorizated/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.


        // GET: /Autorizated/Edit/5


        // POST: /Autorizated/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.


        // GET: /Autorizated/Delete/5

        // POST: /Autorizated/Delete/5

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
