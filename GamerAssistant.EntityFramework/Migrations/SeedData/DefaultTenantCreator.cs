using System.Linq;
using GamerAssistant.EntityFramework;
using GamerAssistant.MultiTenancy;

namespace GamerAssistant.Migrations.SeedData
{
    public class DefaultTenantCreator
    {
        private readonly GamerAssistantDbContext _context;

        public DefaultTenantCreator(GamerAssistantDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            //Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                _context.Tenants.Add(new Tenant {TenancyName = Tenant.DefaultTenantName, Name = Tenant.DefaultTenantName});
                _context.SaveChanges();
            }
        }
    }
}
