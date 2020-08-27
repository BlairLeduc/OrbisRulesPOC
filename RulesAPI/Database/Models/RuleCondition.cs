namespace RulesAPI.Database.Models
{
    public class RuleCondition
    {
        public int RuleId { get; set; }
        public Rule Rule { get; set; }

        public int ConditionId { get; set; }
        public Condition Condition { get; set; }


    }
}
