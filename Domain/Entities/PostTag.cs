﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PostTag
    {
        public int Id { get; set; }
        public int TagID { get; set; }  

        public int PostID { get; set; }
    

    }
}
