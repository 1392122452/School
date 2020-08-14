using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
    public interface IStudentRepository
    {
        /// <summary>
        /// 通过ID来获取学生信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Student GetStudent(int id);
      
        /// <summary>
        /// 获取所有的学生信息
        /// </summary>
        /// <returns></returns>
        IEnumerable<Student> GetAllStudent();

        /// <summary>
        /// 添加一名学生信息
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        Student Add(Student student);

        /// <summary>
        /// 更新学生信息
        /// </summary>
        /// <param name="UpdataStudent"></param>
        /// <returns></returns>
        Student Updata(Student UpdataStudent);

        /// <summary>
        /// 删除学生信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Student Delete(int id);
     
    }
}
