using System;
using System.Threading.Tasks;
using RulesAPI.Database.Models;

namespace RulesAPI.Conditions
{
    public class CheckForTagsAddedCondition : IConditionProcessor
    {
        public async Task<bool> ExecuteCondition(Condition condition, object beforeMessage, object afterMessage)
        {
            // Write code specific to this condition
            return await Task.FromResult(true);
            //throw new NotImplementedException();
        }
    }
}
