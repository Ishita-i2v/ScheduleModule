using Domain.Exceptions;
using Scheduling.Contracts;
using Scheduling.Contracts.Schedule.Enums;

namespace Domain.Schedule;

public class Schedule : BaseEntity
{
    public string Name { get;  set; }
    public ScheduleType Type { get;  set; }
    public ScheduleSubType? SubType { get;  set; }
    public string Details { get;  set; }
    private DateTime _startDateTime;
    private DateTime? _endDateTime;
    public int? NoOfDays { get;  set; }
    public List<Days> StartDays { get;  set; }
        
    public ScheduleStatus Status { get;  set; } 
    public DateTime? RecurringTime { get;  set; }


    // Property to store StartDateTime in UTC and retrieve in local time
    public DateTime StartDateTime
    {
        get => _startDateTime.ToLocalTime();
        set => _startDateTime = value.Kind == DateTimeKind.Utc ? value : value.ToUniversalTime();
    }
    public DateTime? EndDateTime
    {
        get => _endDateTime?.ToLocalTime();
        set
        {
            if (value.HasValue)
            {
                _endDateTime = value.Value.Kind == DateTimeKind.Utc 
                    ? value.Value 
                    : value.Value.ToUniversalTime();
            }
            else
            {
                _endDateTime = null;
            }
        }
    }
        
    public string? StartCronExp { get; set; }
    public string? StopCronExp { get; set; }
    
    public void ConvertToUTC()
    {
        StartDateTime = StartDateTime.ToUniversalTime();
        EndDateTime = EndDateTime?.ToUniversalTime();
    }

    public Schedule()
    {
        StartDays = new List<Days>();

    }
   

    public Schedule(string name, ScheduleType type, ScheduleSubType? subType, string details, int? noOfdays,
        List<Days> startDays, ScheduleStatus enabled, DateTime startTime, DateTime endTime, DateTime? recurringTime)
    {
        ValidateSchedule(name, type, details, startTime, endTime);
        Name = name;
        Type = type;
        SubType = subType;
        StartDays = startDays;
        NoOfDays = noOfdays;
        Status = enabled;
        Details = details;
        StartDateTime = startTime;
        EndDateTime = endTime;
        RecurringTime = recurringTime;
    }
   
    private static void ValidateSchedule(string name, ScheduleType type, string details,
        DateTime startTime, DateTime endTime)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("name cannot be empty.");
        
        if (string.IsNullOrWhiteSpace(details))
            throw new DomainException("details cannot be empty.");
        
        if (type < 0)
            throw new DomainException("type cannot be negative.");
        
        if (startTime== default(DateTime))
            throw new DomainException("Start time cannot be empty.");
        
        if (endTime== default(DateTime))
            throw new DomainException("End time cannot be empty.");
        
    }
    public void UpdateDetails(string name, ScheduleType type, ScheduleSubType? subType, string details, int? noOfdays,
        List<Days> startDays, DateTime startTime, DateTime endTime,DateTime? recurringTime)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("name cannot be empty.");
        
        if (string.IsNullOrWhiteSpace(details))
            throw new DomainException("details cannot be empty.");
        
        if (type <= 0)
            throw new DomainException("Type must be positive.");
        
        Name = name;
        Details = details;
        Type = type;
        SubType = subType;
        NoOfDays = noOfdays;
        StartDays = startDays;
        StartDateTime = startTime;
        EndDateTime = endTime;
        RecurringTime = recurringTime;
    }
    public void UpdateStatus(ScheduleStatus state)
    {
        Status =state==ScheduleStatus.Enabled ? ScheduleStatus.Enabled : ScheduleStatus.Disabled;
    }
}