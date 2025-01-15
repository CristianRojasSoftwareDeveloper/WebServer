using System.Linq.Expressions;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.GenericRepositories {

    public interface IQueryableRepository<EntityType> {

        EntityType? FirstOrDefault (Expression<Func<EntityType, bool>> predicate);
        Task<EntityType?> FirstOrDefaultAsync (Expression<Func<EntityType, bool>> predicate);

    }

}