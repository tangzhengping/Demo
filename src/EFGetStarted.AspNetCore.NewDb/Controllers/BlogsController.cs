using EFGetStarted.AspNetCore.NewDb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFGetStarted.AspNetCore.NewDb.Controllers
{
    public class BlogsController : Controller
    {
        private BloggingContext _context;

        public int test = 1;
        public BlogsController(BloggingContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Blogs.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Blog blog)
        {
            try
            {
                List<Blog> list = new List<Blog>();
                for (int i = 0; i < 5000; i++)
                {
                    Blog lg = new Blog();
                    lg.Url = System.Guid.NewGuid().ToString();
                    list.Add(lg);
                }
               var  dt_st = DateTime.Now.Ticks;
                _context.AddRange(list);
           var result = _context.SaveChanges();
                var dt_end = DateTime.Now.Ticks;
                var diff = (dt_end - dt_st) * Math.Pow(10,-7);
                Console.WriteLine(diff);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                throw;
            }


            return View(blog);
        }

        public IActionResult Modify()
        {
            var blg = _context.Blogs.First();
            blg.Url = DateTime.Now.ToString("yyyy-MM-dd HH:mm:fff");
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Delete()
        {
            var blg = _context.Blogs.First();
            _context.Blogs.Remove(blg);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
