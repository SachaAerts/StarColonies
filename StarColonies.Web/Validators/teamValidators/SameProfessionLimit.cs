using System.ComponentModel.DataAnnotations;
using StarColonies.Domains.Models;
using StarColonies.Domains.Models.Colony;

namespace StarColonies.Web.validators.teamValidators;

public class SameProfessionLimit: ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is IList<ColonistModel> list)
        {
            var jobCounts = new Dictionary<JobModel, int>();

            foreach (var colonist in list)
            {
                jobCounts.TryAdd(colonist.Job, 0);

                jobCounts[colonist.Job]++;

                if (jobCounts[colonist.Job] > 2)
                    return false;
            }

            return true;
        }

        return false;
    }
}