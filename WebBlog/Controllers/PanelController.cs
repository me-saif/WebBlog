using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebBlog.Data.Repository;
using WebBlog.Models;

namespace WebBlog.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PanelController: Controller
    {
        private IRepository _repo;

        public PanelController(IRepository repo)
        {
            _repo = repo;
        }
        public IActionResult Index()
        {
            var posts = _repo.GellAllPosts();
            return View(posts);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
                return View(new Post());
            else
            {
                var post = _repo.GetPost((int)id);
                return View(post);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Post post)
        {
            if (post.Id > 0)
                _repo.UpdatePost(post);
            else
                _repo.AddPost(post);

            if (await _repo.SaveChangesAsync())
                return RedirectToAction("Index");
            else
                return View(post);
        }

        public async Task<IActionResult> Remove(int id)
        {
            _repo.RemovePost(id);
            await _repo.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
