using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumWeb.ViewModel
{
    public class ForumRegisterViewModel
    {
        //用户编号
        public int Id { get; set; }
        //用户名
        [Display(Name = "用户名")]

        [Required(ErrorMessage = "请输入用户名"), MaxLength(50, ErrorMessage = "名字长度不能超过50个字符")]
        public string Name { get; set; }
        //用户密码
        [Display(Name = "密码")]

        [Required(ErrorMessage = "请输入密码"), MaxLength(18, ErrorMessage = "名字长度不能超过18个字符")]
        public string Pwd { get; set; }
        //用户邮件
        [Display(Name = "邮箱")]
        [Required(ErrorMessage = "请输入邮箱")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
           ErrorMessage = "邮箱的格式不正确")]   //邮箱验证的正则表达式
        public string Email { get; set; }
        //用户电话
        [Display(Name = "手机号")]
        [Required(ErrorMessage = "请输入手机号")]
        [RegularExpression(@"^[1]+[3,5]+\d{9}",
          ErrorMessage = "手机格式不正确")]   //邮箱验证的正则表达式
        public string Phone { get; set; }

        [Display(Name = "头像")]
        public List<IFormFile> Photos { get; set; }
    }
}
