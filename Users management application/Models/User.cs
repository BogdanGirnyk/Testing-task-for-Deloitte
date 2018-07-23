using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Users_management_application.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string PicturePath { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string About { get; set; }
        public DateTime Registered { get; set; }
        public List<string> Tags { get; set; }
    }
}