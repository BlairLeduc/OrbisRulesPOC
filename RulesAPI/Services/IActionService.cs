using System.Threading.Tasks;
using RulesAPI.Database.Models;

namespace RulesAPI.Services
{
    public interface IActionService
    {
        Task<bool> ExecuteAction(string actionName, object data, int? version = null);
        Task<bool> ExecuteAction(Action action, object data);
    }
}
