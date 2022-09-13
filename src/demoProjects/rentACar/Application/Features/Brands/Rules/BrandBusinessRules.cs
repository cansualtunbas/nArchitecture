using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Rules
{
    public class BrandBusinessRules
    {
        private readonly IBrandRepository _brandRepository;

        public BrandBusinessRules(IBrandRepository someFeatureEntityRepository)
        {
            _brandRepository = someFeatureEntityRepository;
        }

        public async Task BrandNameCanNotBeDuplicatedWhenInserted(string name)
        {
            IPaginate<Brand> result = await _brandRepository.GetListAsync(b => b.Name == name);
            //eğer data varsa businessexception fırlatacak.
            if (result.Items.Any()) throw new BusinessException("Brand name exists.");
        }
        public void BrandShouldExistWhenRequested(Brand brand)
        {
            //eğer data varsa businessexception fırlatacak.
            if (brand==null) throw new BusinessException("Requested brand does not exist");
        }
    }
}
