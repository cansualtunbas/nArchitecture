using Application.Features.Models.Dtos;
using Application.Features.Models.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Models.Queries.GetListModel
{
    public class GetListModelQuery:IRequest<ModelListModel>
    {
        public PageRequest PageRequest { get; set; }

        //querynin kendisini veriyoruz ki hangi handler çalışılacağını bilsin
        public class GetListModelQueryHandler : IRequestHandler<GetListModelQuery, ModelListModel>
        {
            private readonly IMapper _mapper;
            private readonly IModelRepository _modelRepository;

            public GetListModelQueryHandler( IMapper mapper, IModelRepository modelRepository)
            {
                _mapper = mapper;
                _modelRepository = modelRepository;
            }

            public async Task<ModelListModel> Handle(GetListModelQuery request, CancellationToken cancellationToken)
            {
                //join işlemine ihtiyaç var. Brand içindeki brandname ulaşmak için
                //araba modelleri-carmodel
               IPaginate<Model> models=await _modelRepository.GetListAsync(include: m => m.Include(c => c.Brand),
                                              index:request.PageRequest.Page,
                                              size:request.PageRequest.PageSize);

                //birden fazla include işlemi yapılacaksa .Include ile devm edebilirsin.
                //data modeli-data model
                ModelListModel mappedModels = _mapper.Map<ModelListModel>(models);
                return mappedModels;
            }
        }
    }
}
