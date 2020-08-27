using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using RulesAPI.Database.Models;
using RulesAPI.Services;

namespace RulesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RulesController : ControllerBase
    {
        private readonly IRuleService _ruleService;
        public RulesController(IRuleService ruleService)
        {
            _ruleService = ruleService;
        }

        


        [HttpGet("{entityType}")]
        public async Task<IActionResult> Execute([FromRoute] string entityType)
        {
            var before = new SubsidizedPlacement
            {
                State = "Approved"
            };
            var after = new SubsidizedPlacement
            {
                State = "Waiting"
            };
            await _ruleService.ExecuteRulesByEntityType(entityType, before, after);
            return new OkResult();
        }
    }
}
