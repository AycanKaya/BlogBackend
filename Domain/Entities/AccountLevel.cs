using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AccountLevel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; } //max post atabilme sayısı
    }
}
