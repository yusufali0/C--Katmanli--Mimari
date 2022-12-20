using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAB.ToDoAppNTier.Common.ResponseObjects;
using YAB.ToDoAppNTier.Dtos.Interfaces;
using YAB.ToDoAppNTier.Dtos.WorkDtos;
using YAB.ToDoAppNTier.Entities.Domains;

namespace YAB.ToDoApp.NTier.Business.Interfaces
{
    public interface IWorkService
    {
        Task<IResponse<List<WorkListDto>>> GetAll();

        Task<IResponse<WorkCreateDto>> Create(WorkCreateDto dto);

        Task<IResponse<IDto>> GetById<IDto>(int id);

        Task<IResponse> Remove(int id);

        Task<IResponse<WorkUpdateDto>> Update(WorkUpdateDto dto);
    }
}
