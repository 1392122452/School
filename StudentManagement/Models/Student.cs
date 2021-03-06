﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
    /// <summary>
    /// 学生模型
    /// </summary>
    public class Student
    {

        public int Id { get; set; }

        [Display(Name = "姓名")]

        [Required(ErrorMessage = "请输入名字"), MaxLength(50, ErrorMessage = "名字长度不能超过50个字符")]
        public string Name { get; set; }

        [Display(Name = "年级")]
        [Required(ErrorMessage = "请输入年级")]
        public ClassNameEnum? ClassName { get; set; }

        [Display(Name = "邮箱")]
        [Required(ErrorMessage = "请输入邮箱")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
            ErrorMessage ="邮箱的格式不正确")]   //邮箱验证的正则表达式
        public string Email { get; set; }

        public string PhotoPath { get; set; }

    }
}
    