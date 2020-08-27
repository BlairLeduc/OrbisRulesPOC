using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RulesAPI.Actions;
using RulesAPI.Database;
using Action = RulesAPI.Database.Models.Action;

namespace RulesAPI.Services
{
    public class ActionService : IActionService
    {
        private readonly IDictionary<string, IActionProcessor> _actionProcessors;
        private readonly RuleContext _ruleContext;

        public ActionService(IEnumerable<IActionProcessor> actionProcessors, RuleContext ruleContext)
        {
            _actionProcessors = actionProcessors.ToDictionary(k => k.GetType().Name, v => v, StringComparer.OrdinalIgnoreCase);
            _ruleContext = ruleContext;
        }

        public async Task<bool> ExecuteAction(string actionName, object data, int? version = null)
        {
            // Get the action with the latest version, unless version number was passed, then get action with that version
            var action = _ruleContext.Actions
                .Where(c => c.Name == actionName && 
                        c.IsPublished &&
                        (version == null || c.Version == version.Value))
                .OrderByDescending(c => c.Version)
                .FirstOrDefault();
            if (action == null)
            {
                throw new ArgumentException($"Cannot find action with Name: {actionName} and Version: {version}");
            }
            return await ExecuteAction(action, data);
        }
        public async Task<bool> ExecuteAction(Action action, object data)
        {
            if (_actionProcessors.TryGetValue(action.ActionProcessorTypeName, out IActionProcessor actionProcessor))
            {
                return await actionProcessor.ExecuteAction(action, data);
            }
            else
            {
                throw new ArgumentException("Action Processor not found in the database.");
            }
        }
    }
}
