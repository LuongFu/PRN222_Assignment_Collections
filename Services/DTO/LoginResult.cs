using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class LoginResult
    {
        public bool IsSuccess { get; set; }
        public int Role { get; set; }
        public short? UserId { get; set; }
        public string RedirectController { get; set; }
    }

}
