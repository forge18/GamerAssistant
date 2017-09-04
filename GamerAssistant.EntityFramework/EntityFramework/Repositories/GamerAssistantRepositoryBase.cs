using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace GamerAssistant.EntityFramework.Repositories
{
    public abstract class GamerAssistantRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<GamerAssistantDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected GamerAssistantRepositoryBase(IDbContextProvider<GamerAssistantDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class GamerAssistantRepositoryBase<TEntity> : GamerAssistantRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected GamerAssistantRepositoryBase(IDbContextProvider<GamerAssistantDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
