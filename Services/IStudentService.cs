using MyWebApplication2.Common;
using MyWebApplication2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApplication2.Services
{
    public interface IStudentService
    {
        bool ValidateStudentModel(StudentModel student);

        Task CreateStudent(StudentModel student);

        List<StudentModel> GetAllStudents();

        (StudentModel, Error) GetStudentByRollNo(string rollNo);

        Task<Error> UpdateStudentRecord(StudentModel student);

        Task<Error> DeleteStudentRecord(string rollNo);
    }
}
