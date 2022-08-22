using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.PostServiceDTOs;
using Domain.Entities;

namespace Application.DTO.TagDTOs
{
    public class PostAndTagsDTO
    {
        public PostResponseDTO post{ get; set; }
        
        public Comment[] comments { get; set; }
        public Tag[] Tags { get; set; }
    }
}
