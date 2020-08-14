using ForumWeb.Models;
using System;
using System.Collections.Generic;

namespace ForumWeb.ViewModel
{
    public class GetEnumerator<T>
    {

        public int Id { get; set; }
        //回复内容
        public string Content { get; set; }
        //回复时间
        public DateTime Created { get; set; }
        //回复用户
        public virtual User User { get; set; }
        //回复的帖子
        public virtual Post Post { get; set; }


    }
}