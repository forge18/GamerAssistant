using GamerAssistant.EntityFramework;
using EntityFramework.DynamicFilters;

namespace GamerAssistant.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly GamerAssistantDbContext _context;

        public InitialHostDbBuilder(GamerAssistantDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new DefaultEditionsCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
        }
    }
}
