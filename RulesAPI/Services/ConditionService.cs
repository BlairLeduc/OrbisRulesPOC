using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RulesAPI.Conditions;
using RulesAPI.Database;
using RulesAPI.Database.Models;

namespace RulesAPI.Services
{
    public class ConditionService :IConditionService
    {

        private readonly IDictionary<string, IConditionProcessor> _conditionProcessors;
        private readonly RuleContext _ruleContext;

        public ConditionService(IEnumerable<IConditionProcessor> conditionProcessors, RuleContext ruleContext)
        {
            _conditionProcessors = conditionProcessors.ToDictionary(k => k.GetType().Name, v => v, StringComparer.OrdinalIgnoreCase);
            _ruleContext = ruleContext;
        }
        public async Task<bool> ExecuteCondition(string conditionName,  object before, object after, int? version = null)
        {
            var condition = _ruleContext.Conditions
                .Where(c => c.Name == conditionName &&
                            c.IsPublished &&
                            (version == null || c.Version == version.Value))
                .OrderByDescending(c => c.Version)
                .FirstOrDefault();

            if (condition == null)
            {
                throw new ArgumentException($"Cannot find condition with Name: {conditionName} and Version: {version}");
            }
            return await ExecuteCondition(condition, before, after);
        }
        public async Task<bool> ExecuteCondition(Condition condition, object before, object after)
        {
            if (_conditionProcessors.TryGetValue(condition.ConditionProcessorTypeName, out IConditionProcessor conditionProcessor))
            {
                return await conditionProcessor.ExecuteCondition(condition, before, after);
            }
            else
            {
                throw new ArgumentException("Condition Processor not found in the database.");
            }
        }
    }
}
