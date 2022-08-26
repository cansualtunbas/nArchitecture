using Application.Features.Brands.Dtos;
using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Commands.CreateBrand
{
    public class CreateBrandCommand :IRequest<CreatedBrandEntityDto>
    {
        //dönüşte name veriyoruz.
        public string Name { get; set; }

        //böyle bir command sıraya koyulursa hangi handler çalışacak onu yazıyoruz.
        //irequesthandler içindeki ilk terim çağırılacak command, ikinci terim döndürülecek veri
        public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, CreatedBrandEntityDto>
        {
            private readonly IBrandRepository _brandRepository;
            private readonly IMapper _mapper;
            private readonly BrandBusinessRules _brandBusinessRules;
            //constructor
            public CreateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper, BrandBusinessRules brandBusinessRules)
            {
                _brandRepository = brandRepository;
                _mapper = mapper;
                _brandBusinessRules = brandBusinessRules;
            }
            //handle IRequestHandler implement'i ile geldi.
            public async Task<CreatedBrandEntityDto> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
            {
                //rules 
                await _brandBusinessRules.BrandNameCanNotBeDuplicatedWhenInserted(request.Name);

                //gelen request'i brand nesnesine çevirmek için mapper kullanıyoruz.
                Brand mappedBrand=_mapper.Map<Brand>(request);

                //repository kullanarak bu ekleme işlemini gerçekleştirmek gerekiyor.
                Brand createdBrand = await _brandRepository.AddAsync(mappedBrand);

                //veri tabanından geleni dto'ya çevirmemiz lazım.
                CreatedBrandEntityDto createdBrandEntityDto= _mapper.Map<CreatedBrandEntityDto>(createdBrand);

                //ve dto döndürüyoruz.
                return createdBrandEntityDto;
            }
        }

    }
    //mapper kısaca; brand'i dto'ya çeviriyor.
}
