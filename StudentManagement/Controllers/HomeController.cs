using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentManagement.Models;
using StudentManagement.ViewModel;
using System.IO;
using Microsoft.AspNetCore.Hosting;
//using Microsoft.Extensions.Hosting.Internal;

namespace StudentManagement.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IHostingEnvironment _hostingEnvironment;

        //使用构造函数注入的方式注入到IStudentRepository
        public HomeController(IStudentRepository studentRepository,IHostingEnvironment hostingEnvironment)
        {
            _studentRepository = studentRepository;
            _hostingEnvironment = hostingEnvironment;
        }
        
        //主页面
        public IActionResult Index()
        {
            //返回学生名字
            //return _studentRepository.GetStudent(1).Name;
            
            //查询所有学生的信息
            IEnumerable<Student> students = _studentRepository.GetAllStudent();
            //将学生的信息传递到视图中
            return View(students);

        }
        //学生详情页面
        public IActionResult Data(int id)
        {
            // throw new Exception("此异常发生在Data视图中");
            Student student = _studentRepository.GetStudent(id);

            //处理404报错信息
            if (student==null)
            {
                Response.StatusCode = 404;
                return View("StudentNotFound",id);
            }
            //实例化 HomeDataViewModel并存储Student详细信息和PageTitle
            HomeDataViewModel homeDataViewModel = new HomeDataViewModel()
            {
                Student = student,
                PageTitle="学生信息"
            };

            return View(homeDataViewModel);
        }
        //获取数据
        [HttpGet]
        public IActionResult AddS()
        {
            return View();
        }
        //发送数据
        [HttpPost]
        public IActionResult AddS(StudentsAddSViewModel model)
        {
            if (ModelState.IsValid) 
            {
                string uniqueFileName = null;

                if (model.Photos!=null&&model.Photos.Count>0)
                {

                    uniqueFileName = ProcessUploadedFile(model);

                }

                Student newStudent = new Student
                {
                    Name = model.Name,
                    Email = model.Email,
                    ClassName = model.ClassName,
                    PhotoPath = uniqueFileName,
                };
                
                _studentRepository.Add(newStudent);

                //Student newStudent = _studentRepository.Add(student);

                 return RedirectToAction("Data",new { id=newStudent.Id });
            }

            return View();
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            //查询是否在数据库中
            Student student = _studentRepository.GetStudent(id);

            
                StudentEditViewModel studentEditView = new StudentEditViewModel
                {
                    Id = student.Id,
                    Name = student.Name,
                    Email = student.Email,
                    ClassName = student.ClassName,
                    ExistingPhotoPath = student.PhotoPath
                };
                 return View(studentEditView); 
            
        }

        [HttpPost]
        public IActionResult Edit(StudentEditViewModel model)
        {
            //检测提供的数据是否有效，如果没有通过验证，需要重新编辑学生信息
            //这样用户就可以更正并且重新提交编辑表单
            if (ModelState.IsValid)
            {
                Student student = _studentRepository.GetStudent(model.Id);

                student.Email = model.Email;
                student.Name = model.Name;
                student.ClassName = model.ClassName;

                if (model.Photos.Count>0)
                {
                    if (model.ExistingPhotoPath!=null)
                    {
                        string filePahth = Path.Combine(_hostingEnvironment.WebRootPath,"img",model.ExistingPhotoPath);
                        //删除原本的图片
                        System.IO.File.Delete(filePahth);
                    }

                    student.PhotoPath = ProcessUploadedFile(model);

                }

                Student updateStudent= _studentRepository.Updata(student);

                return RedirectToAction("Index");     
            }
                return View(model);
        }

        /// <summary>
        /// 将图片保存到指定的路径中，并返回唯一的文件名
        /// </summary>
        /// <returns></returns>
        private string ProcessUploadedFile(StudentsAddSViewModel model)
        {

            string uniqueFileName = null;
            if (model.Photos.Count > 0)
            {
                     foreach (var photo in model.Photos)
                {
                    //将图片上传到WWWROOT的img文件夹中
                    //要获取到wwwroot文件夹的路径，我们需要注入ASP.NET Core提供的HostingEnvironment服务
                    //通过HostingEnvironment服务获取到wwwroot文件夹路径
                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "img");
                    //为了保证文件名是唯一，在文件夹名后加一个新的GUID值和一个下划线
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    //使用了非托管资源，所以需要手动进行释放
                    using (var fileStream=new FileStream(filePath,FileMode.Create))
                    {
                        //使用IFormFile接口提供的CopyTo()方法将文件复制到wwwroot/img文件夹中
                        photo.CopyTo(fileStream);
                    }
                        
                       // photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }
            }
               
            return uniqueFileName;
        }


        [HttpGet]

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            Student student = _studentRepository.GetStudent((int)id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);

        }

        [HttpPost]
        public IActionResult Delete(StudentDeleteViewModel model)
        {
            Student student = _studentRepository.GetStudent(model.Id);
            _studentRepository.Delete(model.Id);
            
            return RedirectToAction("Index");
        }

    }

}
