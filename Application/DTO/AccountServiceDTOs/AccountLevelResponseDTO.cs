using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.AccountServiceDTOs
{
    public class AccountLevelResponseDTO
    {
        public int Level { get; set; }
        public string LevelName { get; set; }
        public int SumOfPosts { get; set; }
    }
}
