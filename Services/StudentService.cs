using MyWebApplication2.Common;
using MyWebApplication2.Mappers;
using MyWebApplication2.Models;
using MyWebApplication2.Repositories;
using MyWebApplication2.Repositories.Postgres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApplication2.Services
{
    public class StudentService : IStudentService
    {
        private readonly DtoStudentToDomainStudentMapper _dtoToDomainMapper;
        private readonly MyWebApiContext _context;
        private readonly DomainStudentToDtoStudentMapper _domainToDtoMapper;

        public StudentService(MyWebApiContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dtoToDomainMapper = new DtoStudentToDomainStudentMapper();
            _domainToDtoMapper = new DomainStudentToDtoStudentMapper();
        }
        public async Task CreateStudent(StudentModel student)
        {
            var domainStudent = _dtoToDomainMapper.DtoToDomainMap(student);
            _context.Students.Add(domainStudent);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public bool ValidateStudentModel(StudentModel student)
        {
            return student.Age > 0 && !string.IsNullOrEmpty(student.Name); 
        }

        public List<StudentModel> GetAllStudents()
        {
            var domainStudents = _context.Students.ToList();
            var dtoStudents = new List<StudentModel>();

            foreach (var item in domainStudents)
            {
                var dto = _domainToDtoMapper.DomainToDtoMap(item);
                dtoStudents.Add(dto);
            }

            return dtoStudents;
        }

        public (StudentModel, Error) GetStudentByRollNo(string rollNo)
        {
            var student = _context.Students.ToList().Where(x => x.RollNo.Trim() == rollNo).FirstOrDefault();

            if (student == null)
            {
                var error = new Error();
                error.Title = "Student not found with this roll number.";

                return (null, error);
            }

            return (_domainToDtoMapper.DomainToDtoMap(student), null);
        }

        public async Task<Error> UpdateStudentRecord(StudentModel student)
        {
            var domainStudent = _context.Students.ToList().Where(x => x.RollNo.Trim() == student.RollNo).FirstOrDefault();

            if (domainStudent == null)
            {
                var error = new Error();
                error.Title = "Student record not found.";

                return error;
            }

            var item = _dtoToDomainMapper.DtoToDomainMap(student);

            _context.Students.Update(item);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return null;
        }

        public async Task<Error> DeleteStudentRecord(string rollNo)
        {
            var domainStudent = _context.Students.ToList().Where(x => x.RollNo.Trim() == rollNo).FirstOrDefault();

            if (domainStudent == null)
            {
                var error = new Error();
                error.Title = "Student record not found.";

                return error;
            }

            var dto = _domainToDtoMapper.DomainToDtoMap(domainStudent);
            var item = _dtoToDomainMapper.DtoToDomainMap(dto);

            _context.Students.Remove(item);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return null;
        }
    }
}
