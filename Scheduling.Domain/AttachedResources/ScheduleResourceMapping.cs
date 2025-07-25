using Scheduling.Contracts;
using Scheduling.Contracts.AttachedResources.Enums;

namespace Domain.AttachedResources;


public class ScheduleResourceMapping :BaseEntity
{
    public Guid ScheduleId { get; set; }
    public Guid ResourceId { get; set; }
    
    public Resources ResourceType { get; set; }

    public ScheduleResourceMapping()
    {
        ScheduleId = new Guid();
    }

    public ScheduleResourceMapping(Guid resId, Guid schId, Resources type)
    {
        ScheduleId = schId;
        ResourceId = resId;
        ResourceType = type;
    }
}