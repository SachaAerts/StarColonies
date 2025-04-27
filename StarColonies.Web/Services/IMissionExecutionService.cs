using Microsoft.AspNetCore.Mvc;
using StarColonies.Domains.Models.Missions;
using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Web.Pages;

namespace StarColonies.Web.Services;

public interface IMissionExecutionService
{
    Task<MissionExecutionResultModel> ResolveAndExecuteMissionAsync([FromBody] MissionRequestModel request, ColonistEntity user);
}