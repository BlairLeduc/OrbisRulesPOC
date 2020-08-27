using System.Threading.Tasks;
using Action = RulesAPI.Database.Models.Action;

namespace RulesAPI.Actions
{
    public class InitiateDocusignSigningCeremonyAction : IActionProcessor
    {
        public async Task<bool> ExecuteAction(Action action, object message)
        {
            // Here write the Code to initiate the Docusign Signing ceremony
            return await Task.FromResult((true));
            //throw new NotImplementedException();
        }
    }
}
