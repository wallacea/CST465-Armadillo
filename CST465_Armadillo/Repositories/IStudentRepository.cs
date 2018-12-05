using CST465_Armadillo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CST465_Armadillo.Repositories
{
    public interface IStudentRepository
    {
        List<Student> GetList();
        void Save(Student student);
        
    }
}
