using System.Security.Claims;
using StarColonies.Domains.Models.Missions;
using StarColonies.Web.Pages;

namespace StarColonies.Web.Services;

public interface IMissionExecutionService
{
    Task<MissionResultModel> ResolveAndExecuteMissionAsync(
        ClaimsPrincipal user,
        MissionRequestModel request
    );
}