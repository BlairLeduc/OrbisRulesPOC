using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RulesAPI.Database.Models;
using Action = RulesAPI.Database.Models.Action;

namespace RulesAPI.Database
{
    public class RuleContext : DbContext
    {

        public RuleContext(DbContextOptions<RuleContext> options) : base(options)
        {
        }
        public DbSet<Condition> Conditions { get; set; }
        public DbSet<Action> Actions { get; set; }
        public DbSet<Rule> Rules { get; set; }

        public DbSet<RuleAction> RuleActions { get; set; }
        public DbSet<RuleCondition> RuleConditions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<RuleAction>()
                .HasKey(t => new { TriggerId = t.RuleId, t.ActionId });

            modelBuilder.Entity<RuleCondition>()
                .HasKey(t => new { TriggerId = t.RuleId, t.ConditionId });


            modelBuilder.Entity<Condition>()
                .HasIndex(c => new { c.Name, c.Version })
                .IsUnique();

            modelBuilder.Entity<Action>()
                .HasIndex(c => new { c.Name, c.Version })
                .IsUnique();

        }

        
    }
}
