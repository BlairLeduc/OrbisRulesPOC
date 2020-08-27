namespace RulesAPI.Database.Models
{
    public class RuleAction
    {

        public int RuleId { get; set; }
        public Rule Rule { get; set; }

        public int ActionId { get; set; }
        public Action Action { get; set; }


    }
}
