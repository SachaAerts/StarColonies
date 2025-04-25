using System.ComponentModel.DataAnnotations;
using StarColonies.Domains.Models;
using StarColonies.Domains.Models.Colony;

namespace StarColonies.Web.Validators.TeamValidators
{
    public class JobRequired : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is not IList<ColonistModel> list)
                return false;

            bool hasEngineer = list.Any(c => c.Job == JobModel.Engineer);
            bool hasDoctor = list.Any(c => c.Job == JobModel.Doctor);
            bool hasScientist = list.Any(c => c.Job == JobModel.Scientist);

            if (hasEngineer && hasDoctor && hasScientist)
                return true;

            var missingJobs = new List<string>();
            if (!hasEngineer) missingJobs.Add("Engineer");
            if (!hasDoctor) missingJobs.Add("Doctor");
            if (!hasScientist) missingJobs.Add("Scientist");

            ErrorMessage = $"Your team must include at least one: {string.Join(", ", missingJobs)}.";
            return false;
        }
    }
}