using System.Threading.Tasks;
using RulesAPI.Database.Models;

namespace RulesAPI.Services
{
    public interface IConditionService
    {
        Task<bool> ExecuteCondition(string conditionName, object before, object after, int? version = null);

        Task<bool> ExecuteCondition(Condition condition, object before, object after);
    }
}
