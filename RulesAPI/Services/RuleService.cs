using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RulesAPI.Database;
using RulesAPI.Database.Models;

namespace RulesAPI.Services
{
    public class RuleService : IRuleService
    {
        private readonly IConditionService _conditionService;
        private readonly IActionService _actionService;
        private readonly RuleContext _ruleContext;
        public RuleService(IConditionService conditionService, IActionService actionService, RuleContext ruleContext)
        {
            _conditionService = conditionService;
            _actionService = actionService;
            _ruleContext = ruleContext;
        }


        // Change before and after to Generic ie to IContentItem 
        public async Task ExecuteRulesByEntityType(string entityType, object before, object after)
        {
            // Get all the triggers to execute from the database, could also query on DeliveryPartner etc.
            var rulesToExecute = _ruleContext.Rules.Where(t => t.EntityType == entityType)
                .Include(t => t.RuleActions).ThenInclude(a => a.Action)
                .Include(t => t.RuleConditions).ThenInclude(c => c.Condition)
                .ToArray();
         
            foreach (var ruleToExecute in rulesToExecute)
            {
                var allConditionsPassed = true;
                var atLeastOneConditionPassed = false;
                foreach (var ruleCondition in ruleToExecute.RuleConditions)
                {
                    if (ruleCondition.Condition.IsPublished)
                    {
                        if (!await _conditionService.ExecuteCondition(
                            ruleCondition.Condition,
                            before, after))

                        {
                            allConditionsPassed = false;
                        }
                        else
                        {
                            atLeastOneConditionPassed = true;
                        }
                    }
                }
                if ((atLeastOneConditionPassed && ruleToExecute.ConditionBooleanOperatorType is ConditionBooleanOperatorType.Or) ||
                    (allConditionsPassed && ruleToExecute.ConditionBooleanOperatorType is ConditionBooleanOperatorType.And))
                {
                    // Could also execute the Actions Async , ie drop message to a queue and let Azure Function Execute the Action 
                    foreach (var ruleAction in ruleToExecute.RuleActions)
                    {
                        if (ruleAction.Action.IsPublished)
                        {
                            await _actionService.ExecuteAction(ruleAction.Action, after);
                        }
                    }
                }
            }
        }
    }
}
