using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RulesAPI.Database.Models
{
    public class Action
    {
       
            [Key]
            public int ActionId { get; set; }
            [MaxLength(100)]
            public string Name { get; set; }
            [MaxLength(1000)]
            public string Description { get; set; }
            public string Parameters { get; set; }
            [MaxLength(100)]
            public string ActionProcessorTypeName { get; set; }
            public int Version { get; set; }
            public bool IsPublished { get; set; }
            [MaxLength(200)]
            public string DisplayName { get; set; }
            public List<RuleAction> RuleActions { get; set; }

        


    }
}
