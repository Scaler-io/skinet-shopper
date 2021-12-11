using Microsoft.EntityFrameworkCore;
using Skinet.BusinessLogic.Contracts.Persistence.Specifications;
using Skinet.Entities.Common;
using System.Linq;

namespace Skinet.Persistence.Repositories
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,
                      ISpecification<TEntity> spec)
        {
            var query = inputQuery;

            if (spec.Criteria != null) query = query.Where(spec.Criteria);

            if (spec.Includes != null) query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }
    }
}
