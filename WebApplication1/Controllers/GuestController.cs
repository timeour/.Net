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
//first page. only post and descriptions. can view all post, can't add coments,can't view comments. /
namespace WebApplication1.Controllers
{
    public class GuestController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Guest/
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated) { return Redirect("/Autorizated"); }
            return View(db.post.ToList());

        }

        // GET: /Guest/Details/5
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
            return View(post);
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
