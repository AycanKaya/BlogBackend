using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities
{
    
    public class UserAccountLevel
    {
        [Required]
        public string UserID { get; set; }
        [Required]
        public int AccountLevelID { get; set; }
    }
}
