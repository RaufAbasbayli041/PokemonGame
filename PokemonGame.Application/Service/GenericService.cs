using AutoMapper;
using FluentValidation;
using PokemonGame.Contracts.Contracts;
using PokemonGame_Domain.Entities;
using PokemonGame_Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Application.Service
{
    public class GenericService<TEntity, TDto> : IGenericService<TEntity, TDto>
        where TEntity : BaseEntity, new()
        where TDto : class
    {
 
        private readonly IGenericRepository<TEntity> _repository;

        protected readonly IMapper _mapper;
        private readonly IValidator<TDto> _validator;
        public GenericService(IGenericRepository<TEntity> repository, IMapper mapper, IValidator<TDto> validator)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
        }

        public virtual async Task<IEnumerable<TDto>> GetAllAsync()
        {            
            var datas = await _repository.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<TDto>>(datas);
            return dtos;
        }

        public virtual async Task<TDto> GetByIdAsync(int id)
        {
            var data = await _repository.GetByIdAsync(id);
            if (data == null)
            {
                return null;
            }
            var dto = _mapper.Map<TDto>(data);
            return dto;
        }

        public virtual async Task<TDto> AddAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            var addedData = await _repository.AddAsync(entity);
            var reponseDto = _mapper.Map<TDto>(addedData);
            return reponseDto;
        }

        public virtual async Task<TDto> UpdateAsync(TDto entity)
        {
            var data = _mapper.Map<TEntity>(entity);
            var updatedData = await _repository.UpdateAsync(data);
            var dto = _mapper.Map<TDto>(updatedData);
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
