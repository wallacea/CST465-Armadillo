using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmadilloLib;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace CST465_Armadillo.Repositories
{
    public class ArmadilloCachingDBRepository : ArmadilloDBRepository
    {
        private readonly string _CachePrefix = "ArmadilloCacheRepo";
        private string _CacheListKey { get { return $"{_CachePrefix}_List"; } }
        private IMemoryCache _Cache;
        public ArmadilloCachingDBRepository(IOptions<ArmadilloSettings> armadilloConfig, IMemoryCache cache) : base(armadilloConfig)
        {
            _Cache = cache;
        }
        public override async Task<List<Armadillo>> GetList()
        {

            var armadilloList = (List<Armadillo>) _Cache.Get(_CacheListKey);
            if (armadilloList != null)
            {
                return armadilloList;
            }
            else
            {
                armadilloList = await base.GetList();
                _Cache.Set(_CacheListKey, armadilloList);
                return armadilloList;
            }

        }
        public override void Save(Armadillo armadillo)
        {
            base.Save(armadillo);
            _Cache.Remove(_CacheListKey);
        }
        public override void Delete(int id)
        {
            base.Delete(id);
            _Cache.Remove(_CacheListKey);
        }
    }
}
