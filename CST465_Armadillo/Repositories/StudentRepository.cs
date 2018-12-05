using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CST465_Armadillo.Models;
using Microsoft.Extensions.Caching.Memory;

namespace CST465_Armadillo.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly string _CacheKey = "StudentRepository_List";
        private IMemoryCache _Cache;
        //Since I don't want to create an actual data layer, storing all data in the cache
        public StudentRepository (IMemoryCache cache)
        {
            _Cache = cache;
        }
        public List<Student> GetList()
        {
            List<Student> students = _Cache.Get<List<Student>>(_CacheKey);
            if(students == null)
            {
                students = new List<Student>();
            }
            return students;
        }

        public void Save(Student student)
        {
            List<Student> students = GetList();
            Student existingStudent = students.FirstOrDefault(s => s.Name == student.Name);
            if(existingStudent != null)
            {
                existingStudent.Courses = student.Courses;
            }
            else
            {
                students.Add(student);
            }
            _Cache.Set(_CacheKey, students);
        }
    }
}
