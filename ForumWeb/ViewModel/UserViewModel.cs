using ForumWeb.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ForumWeb.ViewModel
{
    public class UserViewModel
    {
        public User User { get; set; }

        public string PageTitle { get; set; }
   
    }
}
