using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
    //Derdimiz sadece microsoftun cache sistemini eklemek degil. Baske sistemler de kullanılabilir bundan 
    //dolayı bura var. yani dmeek istedigimi zaten biz istesek buraya direk manager e yazabilir ama biz 
    //bunu donusturemeyeiz solid olmaz.
    //.netcore dan gelen kodu kendimize uyguluyoruz.
    public class MemoryCacheManager : ICacheManager
    {

        IMemoryCache _memoryCache;
        public MemoryCacheManager()
        {
            //Bellege bakıp karsılıgını alıyor. CoreModule den.
            _memoryCache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();
        }
        public void Add(string key, object value, int duration)
        {
            //Cache set ettik.
            _memoryCache.Set(key, value, TimeSpan.FromMinutes(duration));

        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public object Get(string key)
        {
            return _memoryCache.Get(key);
        }
        //Bellekte var mı 
        public bool IsAdd(string key)
        {
            //Dat istemedigimiz icin out _
            return _memoryCache.TryGetValue(key, out _);
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }
        //Bellekten silmeye yarar.
        public void RemoveByPattern(string pattern)
        {
            //Kodu calısma anında olusturduk.
            //Bellekte cache ile ilgili olan yapıyı cek dedik.
            var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_memoryCache) as dynamic;
            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();
            //Cache elemanlarını gec.
            foreach (var cacheItem in cacheEntriesCollection)
            {
               
                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
                cacheCollectionValues.Add(cacheItemValue);
            }
            //Alttaki kurala uyanları cek.
            //Degerleri uygun olunları atık removluyoruz.
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList();

            foreach (var key in keysToRemove)
            {
                _memoryCache.Remove(key);
            }
        }
    }
}
