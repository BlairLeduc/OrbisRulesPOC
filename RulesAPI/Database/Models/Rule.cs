using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RulesAPI.Database.Models
{
    public class Rule
    {
        [Key]
        public int RuleId { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string CreatedBy { get; set; }
        [MaxLength(100)]
        public string EntityType { get; set; }
        [MaxLength(100)]
        public bool IsPublished { get; set; }
        public string DeliveryPartner { get; set; }
        public string ActionParameterValues { get; set; }
        public string ConditionParameterValues { get; set; }

        /// <summary>
        /// If there are multiple conditions, in this rule, all the Condition results (true or false)  will be applied to an AND Boolean operator / OR boolean operator.
        /// AND -> All conditions in the Rule must evaluate to true
        /// OR -> Only one of the conditions in the rule must evaluate to true
        /// </summary>
        public ConditionBooleanOperatorType ConditionBooleanOperatorType { get; set; }
        public List<RuleCondition> RuleConditions { get; set; }
        public List<RuleAction> RuleActions { get; set; }


    }

    public enum ConditionBooleanOperatorType 
    {
        And = 0,
        Or =1
    }

}
