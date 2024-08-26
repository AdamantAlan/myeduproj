using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcLearn.Measure;
using Microsoft.AspNetCore.Authorization;

namespace GrpcLearn.Services;

[Authorize]
public class MeasureManagerService : MeasureManager.MeasureManagerBase
{
    public override Task<CreateMeasureReadDto> Invite(CreateMeasureWriteDto dto, ServerCallContext context)
    {
        var eventDateTime = DateTime.UtcNow.AddDays(1);
        var eventDuration = TimeSpan.FromHours(2);
        return Task.FromResult(new CreateMeasureReadDto
        {
            Invitation = $"{dto.Name}, приглашаем вас посетить мероприятие",
            Start = Timestamp.FromDateTime(eventDateTime),
            Duration = Duration.FromTimeSpan(eventDuration)
        });
    }
}