using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBlog.Models;

namespace WebBlog.Data.Repository
{
    public interface IRepository
    {
        Post GetPost(int id);
        List<Post> GellAllPosts();
        void AddPost(Post post);
        void UpdatePost(Post post);
        void RemovePost(int id);


        Task<bool> SaveChangesAsync();
    }
}
