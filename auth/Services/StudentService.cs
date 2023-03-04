using auth.Authorization;
using auth.Data;
using auth.Interfaces;
using auth.Model;

using Microsoft.EntityFrameworkCore;

namespace auth.Services
{
    public class StudentService: IStudentService
    {
        private readonly ApplicationDBContext _context;
       
        public StudentService(ApplicationDBContext context)
        {
            _context = context;
            
        }

        public void AddStudent(Student model)
        {
           if(_context.students.Any(x => x.Name == model.Name))
                throw new Exception("Name '" + model.Name+ "'Name is already");
            var student = new Student()
            {
                Name = model.Name,
                Address = model.Address,
                DepartmentId= model.DepartmentId,
                MajorId= model.MajorId,
            };

            _context.students.Add(student);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var student = getStu(id);
            _context.students.Remove(student);
            _context.SaveChanges();
        }


        public async Task<IEnumerable<StudentViewModel>> GetStudents()
        {
            var students = await _context.students.Include(s => s.Major).Include(s => s.Department).ToListAsync();
            
            List<StudentViewModel> models = new();
            foreach(var student in students)
            {
                StudentViewModel model = new()
                {
                    Id = student.Id,
                    Name = student.Name,
                    Address = student.Address,
                    DepartmentName = student.Department.Name,
                    MajorName = student.Major.Name,
                };
                models.Add(model);
            }
            return models;
        }

        public void UpdateStudent(int id,Student student)
        {
            var stu = getStu(id);
            if (student.Name != student.Name && _context.students.Any(x => x.Name == student.Name))
                throw new Exception("Name '" + student.Name + "' is already taken");

            //_mapper.Map(student, stu);
            _context.students.Update(student);
            _context.SaveChanges();
        }


        public Student GetStudentById(int id)
        {
            return getStu(id);
        }


        private Student getStu(int id)
        {
            var user = _context.students.Include(s => s.Major).Include(s => s.Department).FirstOrDefault(s => s.Id == id);
            if (user == null)
            {
                throw new KeyNotFoundException("Student not found");
            }
            return user;
        }

       
    }
}
