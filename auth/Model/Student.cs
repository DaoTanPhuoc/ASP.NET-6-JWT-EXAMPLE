using System.ComponentModel.DataAnnotations.Schema;

namespace auth.Model
{
    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        [ForeignKey("Major")]
        public int MajorId { get; set; }

        #region

        
        public Department Department { get; set; }
        
        public Major Major { get; set; }
        #endregion
    }

}
