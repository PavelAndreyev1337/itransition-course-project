using AutoMapper;

namespace CollectionApp.BLL.Utils
{
    public static class MapperUtil
    {
        public static TDestination Map<TSource, TDestination>(TSource source, TDestination destination = null)
            where TSource : class
            where TDestination : class
        {
            var mapper = new Mapper(new MapperConfiguration(
                cfg => cfg.CreateMap<TSource, TDestination>()));
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
