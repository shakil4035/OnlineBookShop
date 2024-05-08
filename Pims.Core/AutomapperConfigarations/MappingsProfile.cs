using AutoMapper;
using Pims.Core.CommonModule;
using Pims.Core.Model;
using Pims.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pims.Core.AutomapperConfigarations
{
   public class MappingsProfile:Profile
    {
        public override string ProfileName => "MappingsProfile";
       public  MappingsProfile()
        {
            CreateMap<Author,AuthorViewModel>();
            CreateMap<AuthorViewModel, Author>();

            CreateMap<Category, CategotyViewModel>();
            CreateMap<CategotyViewModel, Category>();

            CreateMap<Seller, SellerViewModel>()
            .ForMember(vm => vm.JoinjngDate,
              opt => opt.MapFrom(m => DateTimeFormater.DateToString(m.JoinjngDate)))
           .ForMember(vm => vm.BirithDate,
              opt => opt.MapFrom(m => DateTimeFormater.DateToString(m.BirithDate)));

            CreateMap<SellerViewModel, Seller>()
              .ForMember(dto => dto.JoinjngDate,
                    opt => opt.MapFrom(m => DateTimeFormater.StringToDate(m.JoinjngDate)))
            .ForMember(dto => dto.BirithDate,
                    opt => opt.MapFrom(m => DateTimeFormater.StringToDate(m.BirithDate)));

            CreateMap<Books, BookViewModel>();
            CreateMap<BookViewModel,Books>();

            CreateMap<Purches, PurchesViewModel>()
             .ForMember(vm => vm.Date,
             opt => opt.MapFrom(m => DateTimeFormater.DateToString(m.Date)));
            CreateMap<PurchesViewModel, Purches>()
              .ForMember(dto => dto.Date,
               opt => opt.MapFrom(m => DateTimeFormater.StringToDate(m.Date)));

            CreateMap<PurchesDetails, PurchesDetailViewModel>();
            CreateMap<PurchesDetailViewModel, PurchesDetails>();
        }
    }
}
