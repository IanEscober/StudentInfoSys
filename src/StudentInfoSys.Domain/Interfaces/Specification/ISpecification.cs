namespace StudentInfoSys.Domain.Interface.Specification
{
    using System;
    using System.Linq.Expressions;
    using System.Collections.Generic;

    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Filter { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        List<string> IncludesString { get; }
    }
}
