
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Infrastructure.Database.Cache.ApacheIgnite
{
    //public class CustomIgniteDbConfiguration : IgniteDbConfiguration
    //{
    //    static IIgnite ignite = Ignition.Start();
    //    static CacheConfiguration metaCache = new CacheConfiguration("metaCache")
    //    {
    //        CacheMode = CacheMode.Partitioned,
    //        Backups = 1,
    //        AtomicityMode = CacheAtomicityMode.Transactional,
    //        WriteSynchronizationMode = CacheWriteSynchronizationMode.PrimarySync
    //    };

    //    static CacheConfiguration dataCache = new CacheConfiguration("dataCache")
    //    {
    //        CacheMode = CacheMode.Partitioned,
    //        Backups = 1,
    //        AtomicityMode = CacheAtomicityMode.Transactional,
    //        WriteSynchronizationMode = CacheWriteSynchronizationMode.PrimarySync
    //    };

    //    static IDbCachingPolicy policy = new CustomIgniteCachePolicy();
    //    public CustomIgniteDbConfiguration()
    //      : base(ignite, metaCache, dataCache, policy)
    //    {
    //        // No-op.
    //    }
    //}
}
