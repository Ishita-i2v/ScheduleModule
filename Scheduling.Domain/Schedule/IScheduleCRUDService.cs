using Scheduling.Contracts.AttachedResources.DTOs;
using Scheduling.Contracts.Schedule.DTOs;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Domain.Schedule;

public interface IScheduleCRUDService
{ 
        // DTO methods for manager
        Task<ScheduleDto> GetByIdAsync(Guid id);
        Task<IEnumerable<ScheduleDto>> GetAllAsync();
        Task<ScheduleDto> AddAsync(ScheduleDto dto, string userName = "");
        Task UpdateAsync(ScheduleDto dto, string userName = "");
        Task DeleteAsync(Guid id, string userName = "");
        Task<bool> ExistAsync(Guid id, string userName = "");


}