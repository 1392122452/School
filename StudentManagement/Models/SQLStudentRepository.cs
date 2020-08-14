using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
    public class SQLStudentRepository : IStudentRepository
    {
        //防止误操作修改context的值
        private readonly AppDbContext context;
        public SQLStudentRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Student Add(Student student)
        {
            context.Students.Add(student);
            context.SaveChanges();
            return student;
        }

        public Student Delete(int id)
        {
            Student student = context.Students.Find(id);
            if (student!=null)
            {
                context.Students.Remove(student);
                context.SaveChanges();
            }
            return student;
        }

        public IEnumerable<Student> GetAllStudent()
        {
            return context.Students;
        }

        public Student GetStudent(int id)
        {
            return context.Students.Find(id);
        }

        public Student Updata(Student UpdataStudent)
        {
            var student = context.Students.Attach(UpdataStudent);
            student.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return UpdataStudent;
        }
    }
}
