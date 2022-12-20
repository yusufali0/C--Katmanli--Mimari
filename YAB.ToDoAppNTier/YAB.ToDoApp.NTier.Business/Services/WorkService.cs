using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAB.ToDoApp.NTier.Business.Extensions;
using YAB.ToDoApp.NTier.Business.Interfaces;
using YAB.ToDoApp.NTier.Business.ValidationRules;
using YAB.ToDoAppNTier.Common.ResponseObjects;
using YAB.ToDoAppNTier.DataAccess.UnitOfWork;
using YAB.ToDoAppNTier.Dtos.Interfaces;
using YAB.ToDoAppNTier.Dtos.WorkDtos;
using YAB.ToDoAppNTier.Entities.Domains;

namespace YAB.ToDoApp.NTier.Business.Services
{
    public class WorkService : IWorkService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<WorkCreateDto> _createDtoValidator;
        private readonly IValidator<WorkUpdateDto> _updateDtoValidator;

        public WorkService(IUow uow, IMapper mapper, IValidator<WorkCreateDto> createDtoValidator, IValidator<WorkUpdateDto> updateDtoValidator)
        {
            _uow = uow;
            _mapper = mapper;
            _createDtoValidator = createDtoValidator;
            _updateDtoValidator = updateDtoValidator;
        }

        public async Task<IResponse<WorkCreateDto>> Create(WorkCreateDto dto)
        {
            var validationResult = _createDtoValidator.Validate(dto);

            if (validationResult.IsValid)
            {
                await _uow.GetRepository<Work>().Create(_mapper.Map<Work>(dto));
                await _uow.SaveChanges();
                return new Response<WorkCreateDto>(ResponseType.Success, dto);
            }
            else
            {

                return new Response<WorkCreateDto>(ResponseType.ValidationError, dto,validationResult.ConvertToCustomValidationError());
            }

        }

        public async Task<IResponse<List<WorkListDto>>> GetAll()
        {

            var data= _mapper.Map<List<WorkListDto>>(await _uow.GetRepository<Work>().GetAll());
            return new Response<List<WorkListDto>>(ResponseType.Success, data);
        }

        public async Task<IResponse<IDto>> GetById<IDto>(int id)
        {
            
            var data=_mapper.Map<IDto>(await _uow.GetRepository<Work>().GetByFilter(x=>x.Id==id));
            if (data == null)
            {
                return new Response<IDto>(ResponseType.NotFound, $"{id} ye ait data bulunamadı");
            }
            return new Response<IDto>(ResponseType.Success,data);
        }

        public async Task<IResponse> Remove(int id)
        {
            var removedEntity = await _uow.GetRepository<Work>().GetByFilter(x => x.Id == id);
            if (removedEntity != null)
            {
                _uow.GetRepository<Work>().Remove(removedEntity);
                await _uow.SaveChanges();
                return new Response(ResponseType.Success);
            }
            return new Response(ResponseType.NotFound,$"{id} ye ait data bulunamadı");
            

        }

        public async Task<IResponse<WorkUpdateDto>> Update(WorkUpdateDto dto)
        {
            var result = _updateDtoValidator.Validate(dto);
            if (result.IsValid)
            {
                var updateEntity = await _uow.GetRepository<Work>().Find(dto.Id);
                if (updateEntity != null)
                {
                    _uow.GetRepository<Work>().Update(_mapper.Map<Work>(dto),updateEntity);
                    await _uow.SaveChanges();
                    return new Response<WorkUpdateDto>(ResponseType.Success,dto);
                }
                return new Response<WorkUpdateDto>(ResponseType.NotFound,$"{dto.Id} ye ait data bulunamadı");
            }
            else
            {   

                return new Response<WorkUpdateDto>(ResponseType.ValidationError, dto,result.ConvertToCustomValidationError());
            }
        }
    }
}
