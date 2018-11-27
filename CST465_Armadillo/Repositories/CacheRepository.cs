using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CST465_Armadillo.Repositories
{
    public class CacheRepository : ICacheRepository
    {
        private readonly string _CachePrefix = "CacheRepo";
        private IMemoryCache _Cache;
        public CacheRepository(IMemoryCache cache)
        {
            _Cache = cache;
        }
        public string GetRandomNumber()
        {
            string cacheKey = $"{_CachePrefix}_RandomNumber";
            //var returnValue = (string)_Cache.Get(cacheKey);
            //if (returnValue != null)
            //{
            //    return returnValue;
            //}
            //else
            {
                string randString = (new Random()).Next(0, 100).ToString();
                //_Cache.Set("RandomNumber", randString);

                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();
                options.SlidingExpiration = TimeSpan.FromSeconds(3);
                options.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(10);
                
                _Cache.Set(cacheKey, randString, options);

                return randString;
            }
            
            
        }
    }
}
