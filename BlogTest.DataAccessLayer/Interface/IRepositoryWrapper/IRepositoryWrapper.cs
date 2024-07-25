using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTest.DataAccessLayer.Interface.IRepositoryWrapper
{
    public interface IRepositoryWrapper
    {
        //Define Interface Repository Name
        IBlogService Blogs { get; }
        //Define Custom Method 
        Task SaveAsync();
    }
}
