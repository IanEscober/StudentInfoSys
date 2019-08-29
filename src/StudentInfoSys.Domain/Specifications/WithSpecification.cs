namespace StudentInfoSys.Domain.Specifications
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Collections.Generic;
    using StudentInfoSys.Domain.Interface.Specification;

    public class WithSpecification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>> Filter { get; private set; }

        public List<Expression<Func<T, object>>> Includes { get; private set; }

        public List<string> IncludesString { get; private set; }

        public WithSpecification(ISpecification<T> baseSpecification, params ISpecification<T>[] otherSpecifications)
        {
            var mergedIncludes = baseSpecification.Includes;
            var mergedIncludesString = baseSpecification.IncludesString;

            foreach (var otherSpecification in otherSpecifications)
            {
                mergedIncludes = mergedIncludes.Concat(otherSpecification.Includes).ToList();
                mergedIncludesString = mergedIncludesString.Concat(otherSpecification.IncludesString).ToList();
            }
            

            this.Filter = baseSpecification.Filter;
            this.Includes = mergedIncludes.ToList();
            this.IncludesString = mergedIncludesString.ToList();
        }
    }
}
