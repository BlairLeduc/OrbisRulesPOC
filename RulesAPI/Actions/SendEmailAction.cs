using System;
using System.Threading.Tasks;
using Action = RulesAPI.Database.Models.Action;

namespace RulesAPI.Actions
{
    public class SendEmailAction : IActionProcessor
    {
        public async Task<bool> ExecuteAction(Action action, object message)
        {

            return await Task.FromResult(true);
            // Here write the Code to Send the email
            //throw new NotImplementedException();
        }
    }
}
