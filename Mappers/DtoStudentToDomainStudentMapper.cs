using MyWebApplication2.Models;
using MyWebApplication2.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApplication2.Mappers
{
    public class DtoStudentToDomainStudentMapper
    {
        public Student DtoToDomainMap(StudentModel dtoStudent)
        {
            var domainStudent = new Student();

            domainStudent.RollNo = dtoStudent.RollNo;
            domainStudent.Name = dtoStudent.Name;
            domainStudent.Age = dtoStudent.Age;

            return domainStudent;
        }
    }
}
