using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Web.Mvc;

namespace ProjectThesisStack.Models
{
    public class User
    {
        public string email { get; set; }
        public string password { get; set; }
        public LoginUserType UserType { get; set; }
    }
    public enum LoginUserType
    {
        Supervisor, Student, Admin
    }
}