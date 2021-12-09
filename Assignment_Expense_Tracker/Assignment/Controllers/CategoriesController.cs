using Assignment.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ExpenseDbContext db;
        public CategoriesController(ExpenseDbContext db) { this.db = db; }
        public IActionResult Index()
        {
            return View(db.ExpenseCategories.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(ExpenseCategory expense)
        {
            if (ModelState.IsValid)
            {
                if (db.ExpenseCategories.Any(x => x.CategoryName.ToLower() == expense.CategoryName.ToLower()))
                {
                    ModelState.AddModelError("", "Category name already exits");
                    return View(expense);
                }
                db.ExpenseCategories.Add(expense);
                db.SaveChanges();
                return PartialView("_ViewPartial", true);
            }
            return PartialView("_ViewPartial", false);
        }
        public ActionResult Edit(int id)
        {
            return View(db.ExpenseCategories.FirstOrDefault(x => x.ExpenseCategoryId == id));
        }
        [HttpPost]
        public ActionResult Edit(ExpenseCategory expense)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expense).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return PartialView("_ViewPartial", true);
            }
            return PartialView("_ViewPartial", false);
        }
        public ActionResult Delete(int id)
        {
            return View(db.ExpenseCategories.FirstOrDefault(x => x.ExpenseCategoryId == id));
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DoDelete(int id)
        {
            var expense = new ExpenseCategory { ExpenseCategoryId = id };
            db.Entry(expense).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
