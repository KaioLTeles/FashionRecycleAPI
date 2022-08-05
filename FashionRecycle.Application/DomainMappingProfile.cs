using AutoMapper;
using FashionRecycle.API.Core.Entity;
using FashionRecycle.API.Core.InputModel;
using FashionRecycle.API.Core.ViewModel;

namespace FashionRecycle.Application
{
    public class DomainMappingProfile : Profile
    {
        public DomainMappingProfile()
        {
            CreateMap<CreateUserInputModel, UserEntity>()
                .ForMember(x => x.UserName, x => x.MapFrom(d => d.UserName))
                .ForMember(x => x.Name, x => x.MapFrom(d => d.Name))
                .ForMember(x => x.Email, x => x.MapFrom(d => d.Email))
                .ForMember(x => x.Password, x => x.MapFrom(d => d.Password))
                .ReverseMap();

            CreateMap<UserViewModel, UserEntity>()
                .ForMember(x => x.Id, x => x.MapFrom(d => d.Id))
                .ForMember(x => x.Name, x => x.MapFrom(d => d.Name))              
                .ReverseMap();

            CreateMap<ListAllClientsViewModel, ClientEntity>()
                .ForMember(x => x.Id, x => x.MapFrom(d => d.Id))
                .ForMember(x => x.Name, x => x.MapFrom(d => d.Name))
                .ForMember(x => x.PhoneNumber, x => x.MapFrom(d => d.PhoneNumber))
                .ForMember(x => x.Email, x => x.MapFrom(d => d.Email))
                .ForMember(x => x.CPF, x => x.MapFrom(d => d.CPF))
                .ForMember(x => x.Active, x => x.MapFrom(d => d.Active))
                .ReverseMap();

            CreateMap<ClientViewModel, ClientEntity>()
                .ForMember(x => x.Id, x => x.MapFrom(d => d.Id))
                .ForMember(x => x.Name, x => x.MapFrom(d => d.Name))
                .ForMember(x => x.PhoneNumber, x => x.MapFrom(d => d.PhoneNumber))
                .ForMember(x => x.Email, x => x.MapFrom(d => d.Email))
                .ForMember(x => x.CPF, x => x.MapFrom(d => d.CPF))
                .ForMember(x => x.Address, x => x.MapFrom(d => d.Address))
                .ForMember(x => x.StreetNumber, x => x.MapFrom(d => d.StreetNumber))
                .ForMember(x => x.CEP, x => x.MapFrom(d => d.CEP))
                .ForMember(x => x.Active, x => x.MapFrom(d => d.Active))
                .ReverseMap();

            CreateMap<CreateClientInputModel, ClientEntity>()
                .ForMember(x => x.Id, x => x.MapFrom(d => d.id))
                .ForMember(x => x.Name, x => x.MapFrom(d => d.name))
                .ForMember(x => x.PhoneNumber, x => x.MapFrom(d => d.phoneNumber))
                .ForMember(x => x.Email, x => x.MapFrom(d => d.email))
                .ForMember(x => x.CPF, x => x.MapFrom(d => d.cpf))
                .ForMember(x => x.Address, x => x.MapFrom(d => d.address))
                .ForMember(x => x.StreetNumber, x => x.MapFrom(d => d.streetNumber))
                .ForMember(x => x.CEP, x => x.MapFrom(d => d.cep))
                .ForMember(x => x.Active, x => x.MapFrom(d => d.active))
                .ReverseMap();            

            CreateMap<ListAllPartnersViewModel, PartnerEntity>()
                .ForMember(x => x.Id, x => x.MapFrom(d => d.Id))
                .ForMember(x => x.Name, x => x.MapFrom(d => d.Name))
                .ForMember(x => x.PhoneNumber, x => x.MapFrom(d => d.PhoneNumber))
                .ForMember(x => x.Email, x => x.MapFrom(d => d.Email))
                .ForMember(x => x.CPF, x => x.MapFrom(d => d.CPF))
                .ForMember(x => x.CNPJ, x => x.MapFrom(d => d.CNPJ))
                .ForMember(x => x.Active, x => x.MapFrom(d => d.Active))
                .ReverseMap();

            CreateMap<PartnerViewModel, PartnerEntity>()
                .ForMember(x => x.Id, x => x.MapFrom(d => d.Id))
                .ForMember(x => x.Name, x => x.MapFrom(d => d.Name))
                .ForMember(x => x.PhoneNumber, x => x.MapFrom(d => d.PhoneNumber))
                .ForMember(x => x.Email, x => x.MapFrom(d => d.Email))
                .ForMember(x => x.CPF, x => x.MapFrom(d => d.CPF))
                .ForMember(x => x.CNPJ, x => x.MapFrom(d => d.CNPJ))
                .ForMember(x => x.Address, x => x.MapFrom(d => d.Address))
                .ForMember(x => x.StreetNumber, x => x.MapFrom(d => d.StreetNumber))
                .ForMember(x => x.CEP, x => x.MapFrom(d => d.CEP))
                .ForMember(x => x.Active, x => x.MapFrom(d => d.Active))
                .ReverseMap();

            CreateMap<CreatePartnerInputModel, PartnerEntity>()
               .ForMember(x => x.Id, x => x.MapFrom(d => d.id))
               .ForMember(x => x.Name, x => x.MapFrom(d => d.name))
               .ForMember(x => x.PhoneNumber, x => x.MapFrom(d => d.phoneNumber))
               .ForMember(x => x.Email, x => x.MapFrom(d => d.email))
               .ForMember(x => x.CPF, x => x.MapFrom(d => d.cpf))
               .ForMember(x => x.CNPJ, x => x.MapFrom(d => d.cnpj))
               .ForMember(x => x.Address, x => x.MapFrom(d => d.address))
               .ForMember(x => x.StreetNumber, x => x.MapFrom(d => d.streetNumber))
               .ForMember(x => x.CEP, x => x.MapFrom(d => d.cep))
               .ForMember(x => x.Active, x => x.MapFrom(d => d.active))
               .ReverseMap();

            CreateMap<PartnerResumeListViewModel, PartnerEntity>()
               .ForMember(x => x.Id, x => x.MapFrom(d => d.Id))
               .ForMember(x => x.Name, x => x.MapFrom(d => d.Name))               
               .ReverseMap();

            CreateMap<ProviderViewModel, ProviderEntity>()
               .ForMember(x => x.Id, x => x.MapFrom(d => d.Id))
               .ForMember(x => x.CompanyName, x => x.MapFrom(d => d.CompanyName))
               .ForMember(x => x.LegalCompanyName, x => x.MapFrom(d => d.LegalCompanyName))
               .ForMember(x => x.PhoneNumber, x => x.MapFrom(d => d.PhoneNumber))
               .ForMember(x => x.Email, x => x.MapFrom(d => d.Email))
               .ForMember(x => x.CNPJ, x => x.MapFrom(d => d.CNPJ))               
               .ForMember(x => x.Address, x => x.MapFrom(d => d.Address))
               .ForMember(x => x.StreetNumber, x => x.MapFrom(d => d.StreetNumber))
               .ForMember(x => x.CEP, x => x.MapFrom(d => d.CEP))
               .ForMember(x => x.Active, x => x.MapFrom(d => d.Active))
               .ReverseMap();

            CreateMap<ListAllProvidersViewModel, ProviderEntity>()
               .ForMember(x => x.Id, x => x.MapFrom(d => d.Id))               
               .ForMember(x => x.LegalCompanyName, x => x.MapFrom(d => d.LegalCompanyName))
               .ForMember(x => x.PhoneNumber, x => x.MapFrom(d => d.PhoneNumber))
               .ForMember(x => x.Email, x => x.MapFrom(d => d.Email))
               .ForMember(x => x.CNPJ, x => x.MapFrom(d => d.CNPJ))                                             
               .ForMember(x => x.Active, x => x.MapFrom(d => d.Active))
               .ReverseMap();

            CreateMap<CreateProviderInputModel, ProviderEntity>()
               .ForMember(x => x.Id, x => x.MapFrom(d => d.id))
               .ForMember(x => x.CompanyName, x => x.MapFrom(d => d.companyName))
               .ForMember(x => x.LegalCompanyName, x => x.MapFrom(d => d.legalCompanyName))
               .ForMember(x => x.PhoneNumber, x => x.MapFrom(d => d.phoneNumber))
               .ForMember(x => x.Email, x => x.MapFrom(d => d.email))
               .ForMember(x => x.CNPJ, x => x.MapFrom(d => d.cnpj))
               .ForMember(x => x.Address, x => x.MapFrom(d => d.address))
               .ForMember(x => x.StreetNumber, x => x.MapFrom(d => d.streetNumber))
               .ForMember(x => x.CEP, x => x.MapFrom(d => d.cep))
               .ForMember(x => x.Active, x => x.MapFrom(d => d.active))
               .ReverseMap();

            CreateMap<ProductViewModel, ProductEntity>()
               .ForMember(x => x.Id, x => x.MapFrom(d => d.Id))
               .ForMember(x => x.Name, x => x.MapFrom(d => d.Name))
               .ForMember(x => x.Brand, x => x.MapFrom(d => d.Brand))
               .ForMember(x => x.AmountInventory, x => x.MapFrom(d => d.AmountInventory))
               .ForMember(x => x.PricePartner, x => x.MapFrom(d => d.PricePartner))
               .ForMember(x => x.PriceSale, x => x.MapFrom(d => d.PriceSale))
               .ForMember(x => x.Partner, x => x.MapFrom(d => new PartnerEntity { Id = d.PartnerId }))
               .ForMember(x => x.Active, x => x.MapFrom(d => d.Active))
               .ForMember(x => x.ProductStatus, x => x.MapFrom(d => d.ProductStatus))
               .ForMember(x => x.SerialNumber, x => x.MapFrom(d => d.SerialNumber))
               .ForMember(x => x.Model, x => x.MapFrom(d => d.Model))
               .ForMember(x => x.Colour, x => x.MapFrom(d => d.Colour))
               .ForMember(x => x.Observation, x => x.MapFrom(d => d.Observation))
               .ForMember(x => x.AlternativeId, x => x.MapFrom(d => d.AlternativeId))
               .ForMember(x => x.BrandId, x => x.MapFrom(d => d.BrandId))
               .ReverseMap();

            CreateMap<ListAllProductsViewModel, ProductEntity>()
               .ForMember(x => x.Id, x => x.MapFrom(d => d.Id))
               .ForMember(x => x.Name, x => x.MapFrom(d => d.Name))
               .ForMember(x => x.Brand, x => x.MapFrom(d => d.Brand))
               .ForMember(x => x.AmountInventory, x => x.MapFrom(d => d.AmountInventory))
               .ForMember(x => x.PricePartner, x => x.MapFrom(d => d.PricePartner))
               .ForMember(x => x.PriceSale, x => x.MapFrom(d => d.PriceSale))
               .ForMember(x => x.Partner, x => x.MapFrom(d => new PartnerEntity { Name = d.PartnerName }))
               .ForMember(x => x.Active, x => x.MapFrom(d => d.Active))
               .ForMember(x => x.ProductStatus, x => x.MapFrom(d => d.ProductStatus))
               .ForMember(x => x.AlternativeId, x => x.MapFrom(d => d.AlternativeId))
               .ForMember(x => x.SerialNumber, x => x.MapFrom(d => d.SerialNumber))
               .ForMember(x => x.Model, x => x.MapFrom(d => d.Model))
                .ForMember(x => x.BrandId, x => x.MapFrom(d => d.BrandId))
               .ReverseMap();

            CreateMap<CreateProductInputModel, ProductEntity>()
              .ForMember(x => x.Id, x => x.MapFrom(d => d.id))
              .ForMember(x => x.Name, x => x.MapFrom(d => d.name))              
              .ForMember(x => x.AmountInventory, x => x.MapFrom(d => d.amountInventory))
              .ForMember(x => x.PricePartner, x => x.MapFrom(d => d.pricePartner))
              .ForMember(x => x.PriceSale, x => x.MapFrom(d => d.priceSale))
              .ForMember(x => x.Partner, x => x.MapFrom(d => new PartnerEntity { Id = d.partnerId }))
              .ForMember(x => x.Active, x => x.MapFrom(d => d.active))
              .ForMember(x => x.ProductStatus, x => x.MapFrom(d => d.productStatus))

              .ReverseMap();

            CreateMap<ListProductsForSaleViewModel, ProductEntity>()
              .ForMember(x => x.Id, x => x.MapFrom(d => d.Id))
              .ForMember(x => x.Name, x => x.MapFrom(d => d.Name))
              .ForMember(x => x.Brand, x => x.MapFrom(d => d.Brand))                            
              .ForMember(x => x.PriceSale, x => x.MapFrom(d => d.PriceSale))
              .ForMember(x => x.AmountInventory, x => x.MapFrom(d => d.AmountInventory))
              .ForMember(x => x.Partner, x => x.MapFrom(d => d.Partner ))              
              .ReverseMap();

            CreateMap<ClientResumeListViewModel, ClientEntity>()
                .ForMember(x => x.Id, x => x.MapFrom(d => d.Id))
                .ForMember(x => x.Name, x => x.MapFrom(d => d.Name))               
                .ReverseMap();

            CreateMap<SaleInputModel, SalesEntity>()
                .ForMember(x => x.IdClient, x => x.MapFrom(d => d.IdClient))
                .ForMember(x => x.AmountSale, x => x.MapFrom(d => d.AmountSale))
                .ForMember(x => x.IdPaymentMethod, x => x.MapFrom(d => d.IdPaymentMethod))
                .ForMember(x => x.Observation, x => x.MapFrom(d => d.Observation))
                .ReverseMap();

            CreateMap<SaleItemsInputModel, SalesItemsEntity>()                
                .ForMember(x => x.IdProduct, x => x.MapFrom(d => d.IdProduct))
                .ForMember(x => x.IdPartner, x => x.MapFrom(d => d.IdPartner))
                .ForMember(x => x.PriceSale, x => x.MapFrom(d => d.PriceSale))
                .ReverseMap();

            CreateMap<PaymentViewModel, PaymentsEntity>()
                .ForMember(x => x.Id, x => x.MapFrom(d => d.Id))
                .ForMember(x => x.PaymenyType, x => x.MapFrom(d => d.PaymenyType))
                .ForMember(x => x.Amount, x => x.MapFrom(d => d.Amount))
                .ForMember(x => x.PaymentDate, x => x.MapFrom(d => d.PaymentDate))
                .ForMember(x => x.Name, x => x.MapFrom(d => d.Name))
                .ForMember(x => x.PaymentMade, x => x.MapFrom(d => d.paymentMade))
                .ReverseMap();

            CreateMap<CreatePaymentInputModel, PaymentsEntity>()
                .ForMember(x => x.Id, x => x.MapFrom(d => d.id))
                .ForMember(x => x.Name, x => x.MapFrom(d => d.name))
                .ForMember(x => x.PaymenyType, x => x.MapFrom(d => new PaymenyTypeEntity { Id = d.idPaymentType }))
                .ForMember(x => x.Amount, x => x.MapFrom(d => d.amount))
                .ForMember(x => x.PaymentMade, x => x.MapFrom(d => d.paymentMade))
                .ForMember(x => x.PaymentDate, x => x.MapFrom(d => d.paymentDate))
                .ReverseMap();

            CreateMap<ProviderResumeListViewModel, ProviderEntity>()
              .ForMember(x => x.Id, x => x.MapFrom(d => d.Id))
              .ForMember(x => x.LegalCompanyName, x => x.MapFrom(d => d.LegalCompanyName))
              .ForMember(x => x.CompanyName, x => x.MapFrom(d => d.CompanyName))
              .ForMember(x => x.CNPJ, x => x.MapFrom(d => d.CNPJ))              
              .ReverseMap();

            CreateMap<BrandViewModel, BrandEntity>()
              .ForMember(x => x.Id, x => x.MapFrom(d => d.Id))
              .ForMember(x => x.Name, x => x.MapFrom(d => d.Name))
              .ReverseMap();

            CreateMap<CreateBrandInputModel, BrandEntity>()
                .ForMember(x => x.Id, x => x.MapFrom(d => d.id))
                .ForMember(x => x.Name, x => x.MapFrom(d => d.name))
              .ReverseMap();
        }
    }
}
