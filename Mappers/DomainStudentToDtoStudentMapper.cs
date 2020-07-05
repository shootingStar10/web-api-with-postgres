using MyWebApplication2.Models;
using MyWebApplication2.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApplication2.Mappers
{
    public class DomainStudentToDtoStudentMapper
    {
        public StudentModel DomainToDtoMap(Student domainStudent)
        {
            var dtoStudent = new StudentModel();

            dtoStudent.RollNo = domainStudent.RollNo.Trim();
            dtoStudent.Name = domainStudent.Name.Trim();
            dtoStudent.Age = domainStudent.Age;

            return dtoStudent;
        }
    }
}
