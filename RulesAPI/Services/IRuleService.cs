using System.Threading.Tasks;

namespace RulesAPI.Services
{

    public interface IRuleService
    {
        Task ExecuteRulesByEntityType(string entityType, object before, object after);

    }
}
