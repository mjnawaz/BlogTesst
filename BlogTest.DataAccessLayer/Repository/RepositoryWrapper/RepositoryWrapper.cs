using BlogTest.DataAccessLayer.Interface;
using BlogTest.DataAccessLayer.Interface.IRepositoryWrapper;
using BlogTest.ModelMigrations.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTest.DataAccessLayer.Repository.RepositoryWrapper
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly ApplicationDbContext _context;
        public RepositoryWrapper(ApplicationDbContext context)
        {
            _context = context;
        }
        // Account Interface
        public IBlogService _blogs { get; set; }
        public IBlogService Blogs
        {
            get
            {
                if (_blogs == null)
                {
                    _blogs = new BlogRepository(_context);
                }
                return _blogs;
            }
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }


    }
}
