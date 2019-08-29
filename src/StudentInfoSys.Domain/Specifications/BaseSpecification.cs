namespace StudentInfoSys.Domain.Specifications
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using StudentInfoSys.Domain.Interface.Specification;

    public abstract class BaseSpecification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>> Filter { get; private set; }

        public List<Expression<Func<T, object>>> Includes { get; private set; }

        public List<string> IncludesString { get; private set; }

        public BaseSpecification()
        {
            this.Filter = _ => true;
            this.Includes = new List<Expression<Func<T, object>>>();
            this.IncludesString = new List<string>();
        }

        public ISpecification<T> With(params ISpecification<T>[] otherSpecifications)
        {
            return new WithSpecification<T>(this, otherSpecifications);
        }

        protected virtual void ApplyFilter(Expression<Func<T, bool>> filter)
        {
            this.Filter = filter;
        }

        protected virtual void AddInclude(Expression<Func<T, object>> include)
        {
            this.Includes.Add(include);
        }

        protected virtual void AddInclude(string include)
        {
            this.IncludesString.Add(include);
        }
    }
}
