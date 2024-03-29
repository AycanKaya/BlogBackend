﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class Post : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public bool isActive { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }     
        public string AuthorID { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsApprove { get; set; }



    }
}
