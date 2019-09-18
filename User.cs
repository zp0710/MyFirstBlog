using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFirstBlog.Models
{
    public class User//用户表
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage ="用户名是必须的")]
        [StringLength(100,ErrorMessage ="字符串超出限制",MinimumLength =2)]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "密码不能为空")]
        [StringLength(20, ErrorMessage = "密码必须大于6小于20", MinimumLength = 6)]
        public string PassWord { get; set; }

        
        public string HeadImg { get; set; }
    }
    public class RegisterUser//注册表
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "用户名是必须的")]
        [StringLength(100, ErrorMessage = "字符串超出限制", MinimumLength = 2)]
        [Index("dddd",IsUnique = true)]
        
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "密码不能为空")]
        [StringLength(20, ErrorMessage = "密码必须大于6小于20", MinimumLength = 6)]
        
        public string PassWord { get; set; }


        [DataType(DataType.Password)]
        [Required(ErrorMessage = "密码不能为空")]
        [Compare("PassWord",ErrorMessage ="两次输入的密码不一致")]
        public string ConfirmPassWord { get; set; }
    }

    public class Admin//管理员表
    {
        [Key]
        public int AdminId { get; set; }

        public string AdName { get; set; }

        public string AdPasw { get; set; }
    }
    public class Guest//留言表
    {
        [Key]
        public int GuestId { get; set; }

        public string Content { get; set; }

        public DateTime AddDateTime { get; set; }

        public virtual User User { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
    }
    public class Article//文章表
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="内容不能为空")]
        [StringLength(100,ErrorMessage ="文章不能超过100字符")]
        public string Content { get; set; }

        public DateTime AddDateTime { get; set; }

        [Required(ErrorMessage ="标题不能为空")]
        public string Title { get; set; }
        public int Hit { get; set; }

        public virtual User User { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }

    }
}