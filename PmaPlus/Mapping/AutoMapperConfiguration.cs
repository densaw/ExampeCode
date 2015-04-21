using AutoMapper;

namespace PmaPlus.Mapping
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<ModelToViewModelProfile>();
                x.AddProfile<ViewModelToModelProfile>();
            });
        }
    }
}
