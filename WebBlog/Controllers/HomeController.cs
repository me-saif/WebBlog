using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBlog.Data;
using WebBlog.Data.Repository;
using WebBlog.Models;

namespace WebBlog.Controllers
{
    public class HomeController : Controller
    {
        private IRepository _repo;

        public HomeController(IRepository repo)
        {
            _repo = repo;
        }
     
        public IActionResult Index() {
            var posts = _repo.GellAllPosts();
            return View(posts);
        }

        public IActionResult Post(int id){
            var post = _repo.GetPost(id);
            return View(post);
        }

    }
}
