using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LU2WebApi.Controllers
{
    public class Environment2D : Controller
    {
        // GET: Environment2D
        public ActionResult Index()
        {
            return View();
        }

        // GET: Environment2D/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Environment2D/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Environment2D/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Environment2D/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Environment2D/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Environment2D/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Environment2D/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
