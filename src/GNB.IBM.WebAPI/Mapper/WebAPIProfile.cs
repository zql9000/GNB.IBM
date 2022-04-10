using AutoMapper;
using GNB.IBM.Application.Models;
using GNB.IBM.WebAPI.Dto;

namespace GNB.IBM.WebAPI.Mapper
{
    public class WebAPIProfile : Profile
    {
        public WebAPIProfile()
        {
            CreateMap<ConversionRateModel, ConversionRateDto>().ReverseMap();
            CreateMap<ProductTransactionModel, ProductTransactionDto>().ReverseMap();
        }
    }
}
