using ForumWeb.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumWeb.ViewModel
{
    public class HomeDataViewModel
    {
        
        
            public Post Post { get; set; }

            public User User { get; set; }

            public PostReply PostReply { get; set; }

            public string Pagetitle { get; set; }

            public virtual IEnumerable<Post> Posts { get; set; }
       
          
    }
}
      

       
    

