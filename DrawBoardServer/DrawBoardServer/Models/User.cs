using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DrawBoardServer.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int? RoomId { get; set; }
        public virtual Room Room { get; set; }
    }
}
