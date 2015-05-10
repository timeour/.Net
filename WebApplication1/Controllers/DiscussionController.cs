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
using System.Data.Entity.Validation;
//viev blogpost and can add comments to it
namespace WebApplication1.Controllers
{
    public class DiscussionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> manager;
        private static Post post;
        public DiscussionController()
        {
            manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }
        // GET: /Discussion/
        public ActionResult Index(int id)
        {
            ApplicationUser currentuser = manager.FindByName(User.Identity.GetUserName());
            int postid = id;
            DiscussionController.post = db.post.ToList().First(post => post.id == id);
            ViewData.Add("PostName", db.post.ToList().First(post => post.id == id).name);
            ViewData.Add("PostText", db.post.ToList().First(post => post.id == id).text);

            return View(db.comments.ToList().Where(comment => comment.post.id == postid));


        }


        // GET: /Discussion/Details/5


        // GET: /Discussion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Discussion/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,text")] Coment coment)
        {
            ApplicationUser currentuser = db.Users.Find(User.Identity.GetUserId());
            Post p = db.post.Find(post.id);
                //await manager.FindByIdAsync(User.Identity.GetUserId());
            if (ModelState.IsValid)
            {
                coment.author = currentuser;
                coment.post = p;
                db.comments.Add(coment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index/" + post.id);
            }

            return View(coment);
        }

        // GET: /Discussion/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coment coment = await db.comments.FindAsync(id);
            if (coment == null)
            {
                return HttpNotFound();
            }
            return View(coment);
        }

        // POST: /Discussion/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,text")] Coment coment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coment).State = EntityState.Modified;                             
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Discussion");
            }
            return View(coment);
        }

        // GET: /Discussion/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coment coment = await db.comments.FindAsync(id);
            if (coment == null)
            {
                return HttpNotFound();
            }
            return View(coment);
        }

        // POST: /Discussion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Coment coment = await db.comments.FindAsync(id);
            int post_id = coment.post.id;
            db.comments.Remove(coment);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Discussion", new { id = post_id});
        }

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
