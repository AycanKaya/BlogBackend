using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.DTO.EditorUserDTOs
{
    public class PostResponse
    {
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public Post Post { get; set; }

    }
}
