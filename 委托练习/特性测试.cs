using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 委托练习
{
    public interface IFeatureCollection
    {
        TFeature Get<TFeature>();
        void Set<TFeature>(TFeature instance);
    }
    public class FeatureCollection : IFeatureCollection
    {
        private ConcurrentDictionary<Type, object> features = new ConcurrentDictionary<Type, object>();
        public TFeature Get<TFeature>()
        {
            object feature;
            return features.TryGetValue(typeof(TFeature), out feature) ? (TFeature)feature : default(TFeature);
        }

        public void Set<TFeature>(TFeature instance)
        {
            features[typeof(TFeature)] = instance;
            //return this;
        }
    }
    class 特性测试
    {
    }
}
