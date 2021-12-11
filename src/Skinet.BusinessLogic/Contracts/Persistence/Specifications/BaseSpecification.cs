using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Skinet.BusinessLogic.Contracts.Persistence.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification() { }
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria { get; }

        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        protected void AddIncludes(Expression<Func<T, object>> includes) 
        {
            Includes.Add(includes);
        }
    }
}
