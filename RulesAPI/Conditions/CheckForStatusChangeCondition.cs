using System;
using System.Threading.Tasks;
using RulesAPI.Database.Models;

namespace RulesAPI.Conditions
{
    public class CheckForStatusChangeCondition : IConditionProcessor
    {

        // Note: Instead of passing in objects , change beforeMessage and afterMessage to Generic   where T:  IContentItem
        public async Task<bool> ExecuteCondition(Condition condition , object beforeMessage, object afterMessage)
        {

            // Read parameters here ->  condition.Parameters

            var beforeSubsidizedPlacement = beforeMessage as SubsidizedPlacement;
            var afterSubsidizedPlacement = afterMessage as SubsidizedPlacement;
            if (beforeSubsidizedPlacement == null || afterSubsidizedPlacement == null)
            {
                throw new ArgumentException("Incorrect Types were passed");
            }
            bool result = beforeSubsidizedPlacement.State != afterSubsidizedPlacement.State;
            return await Task.FromResult(result);
        }
    }
}
