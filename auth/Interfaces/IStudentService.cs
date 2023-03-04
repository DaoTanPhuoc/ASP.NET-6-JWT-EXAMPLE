using auth.Model;
using System.Collections.Generic;

namespace auth.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentViewModel>> GetStudents();
        Student GetStudentById(int id);
        
        void AddStudent(Student student);
        void Delete(int id);
        void UpdateStudent(int id, Student student);

        
    }
}
