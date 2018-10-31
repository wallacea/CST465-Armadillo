using ArmadilloLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CST465_Armadillo.Repositories
{
    public interface IArmadilloRepository
    {
        Armadillo Get(int id);
        List<Armadillo> GetList();
        void Save(Armadillo armadillo);
        void Delete(int id);
    }
}
