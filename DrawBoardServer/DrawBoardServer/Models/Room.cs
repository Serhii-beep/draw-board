using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DrawBoardServer.Models
{
    public class Room
    {
        public Room()
        {
            Users = new List<User>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
