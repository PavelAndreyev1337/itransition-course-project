using AutoMapper;
using System;

namespace CollectionApp.BLL.Utils
{
    public static class MapperUtil
    {
        public static TDestination Map<TSource, TDestination>(
            TSource source,
            TDestination destination = null,
            MapperConfiguration conf = null)
            where TSource : class
            where TDestination : class
        {
            if (conf == null)
            {
                conf = new MapperConfiguration(
                cfg => cfg.CreateMap<TSource, TDestination>());
            }
            var mapper = new Mapper(conf);
            if (destination == null)
            {
                return mapper.Map<TSource, TDestination>(source);
            }
            else
            {
                mapper.Map<TSource, TDestination>(source, destination);
            }
            return destination;
        }
    }
}
