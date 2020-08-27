using System.Threading.Tasks;
using Action = RulesAPI.Database.Models.Action;

namespace RulesAPI.Actions
{
    public interface IActionProcessor
    {
        Task<bool> ExecuteAction(Action action, object message);

    }
}
