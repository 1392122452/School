using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
    public class MokeStudentRepository : IStudentRepository
    {
        private readonly IStudentRepository _studentRepository;
        private List<Student> _studentsList;
        public MokeStudentRepository()
        {
            _studentsList = new List<Student>()
            {
                new Student() { Id = 1, Name = "张三", ClassName = ClassNameEnum.FirstGrade, Email = "139@qq.com" },
                new Student() { Id = 2, Name = "李四", ClassName = ClassNameEnum.SecondGrade, Email = "140@qq.com" },
                new Student() { Id = 3, Name = "王五", ClassName = ClassNameEnum.ThreeGrade, Email = "141@qq.com" },
            };
        }
        /// <summary>
        /// 添加学生信息接口
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public Student Add(Student student)
        {

            student.Id = _studentsList.Max(s => s.Id) + 1;
            //_studentRepository.GetAllStudent().Count().ToString();
            
            _studentsList.Add(student);
            return student;
            //throw new NotImplementedException();

        }
        /// <summary>
        /// 删除学生信息接口
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Student Delete(int id)
        {
            //通过ID查询学生
            Student student = _studentsList.FirstOrDefault(s => s.Id == id);
            //判断学生存在时就移除学生
            if (student!=null)
            {
                _studentsList.Remove(student);
            }

            return student;

        }
        /// <summary>
        /// 查询所有学生信息接口
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Student> GetAllStudent()
        {
            
            return _studentsList;
        }
        /// <summary>
        /// 通过ID获取学生信息接口
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Student GetStudent(int id)
        {
            
            return _studentsList.FirstOrDefault(a => a.Id == id);
        }


        /// <summary>
        /// 更新学生信息接口
        /// </summary>
        /// <param name="UpdataStudent"></param>
        /// <returns></returns>
        public Student Updata(Student UpdataStudent)
        {
            
            Student student = _studentsList.FirstOrDefault(s=>s.Id==UpdataStudent.Id);
            
            if (student!=null)
            {
                student.Id = UpdataStudent.Id;
                student.Name = UpdataStudent.Name;
                student.Email = UpdataStudent.Email;
                student.ClassName = UpdataStudent.ClassName;

            }
            return student;
        }
    }
}
