using Abp.Zero.EntityFramework;
using GamerAssistant.Admin;
using GamerAssistant.Authorization.Roles;
using GamerAssistant.Authorization.Users;
using GamerAssistant.Events;
using GamerAssistant.Games;
using GamerAssistant.MultiTenancy;
using GamerAssistant.Users;
using System.Data.Common;
using System.Data.Entity;

namespace GamerAssistant.EntityFramework
{
    public class GamerAssistantDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for your Entities...
        public virtual IDbSet<GameCategory> GameCategories { get; set; }
        public virtual IDbSet<GameMechanic> GameMechanics { get; set; }
        public virtual IDbSet<GameTheme> GameThemes { get; set; }

        public virtual IDbSet<Event> Events { get; set; }
        public virtual IDbSet<EventAttachment> EventAttachments { get; set; }
        public virtual IDbSet<EventDateOption> EventDateOptions { get; set; }
        public virtual IDbSet<EventGame> EventGames { get; set; }
        public virtual IDbSet<EventTask> EventTasks { get; set; }
        public virtual IDbSet<EventUser> EventUsers { get; set; }

        public virtual IDbSet<Game> Games { get; set; }
        public virtual IDbSet<GameExpansion> GameExpansions { get; set; }
        public virtual IDbSet<GameImage> GameImages { get; set; }
        public virtual IDbSet<GamePlayDate> GamePlayDates { get; set; }

        public virtual IDbSet<UserFavorite> UserFavorites { get; set; }
        public virtual IDbSet<UserVote> UserVotes { get; set; }

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public GamerAssistantDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in GamerAssistantDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of GamerAssistantDbContext since ABP automatically handles it.
         */
        public GamerAssistantDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public GamerAssistantDbContext(DbConnection existingConnection)
         : base(existingConnection, false)
        {

        }

        public GamerAssistantDbContext(DbConnection existingConnection, bool contextOwnsConnection)
         : base(existingConnection, contextOwnsConnection)
        {

        }
    }
}
