using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Abp.Zero.EntityFramework;
using GamerAssistant.EntityFramework;

namespace GamerAssistant
{
    [DependsOn(typeof(AbpZeroEntityFrameworkModule), typeof(GamerAssistantCoreModule))]
    public class GamerAssistantDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<GamerAssistantDbContext>());

            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
