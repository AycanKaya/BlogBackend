using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Entities
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; }
        public string AuthorName { get; set; }
        public int PostID { get; set; }

        public DateTime Created { get; set; }
       

    }
}
