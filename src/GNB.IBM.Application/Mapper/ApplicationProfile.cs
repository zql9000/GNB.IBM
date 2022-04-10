using AutoMapper;
using GNB.IBM.Application.Models;
using GNB.IBM.Core.Entities;

namespace GNB.IBM.Application.Mapper
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<ConversionRate, ConversionRateModel>().ReverseMap();
            CreateMap<ProductTransaction, ProductTransactionModel>().ReverseMap();
        }
    }
}
