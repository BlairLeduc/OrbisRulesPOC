using System.Threading.Tasks;
using RulesAPI.Database.Models;

namespace RulesAPI.Conditions
{
    public interface IConditionProcessor
    {
        Task<bool> ExecuteCondition(Condition condition, object beforeMessage, object afterMessage);

    }
}
