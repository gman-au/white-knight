using System;
using System.Linq.Expressions;

namespace White.Knight.Interfaces.Command
{
    public interface IUpdateCommand<T> : ICommand<T>
    {
        public T Entity { get; set; }

        public Expression<Func<T, object>>[] Exclusions { get; set; }

        public Expression<Func<T, object>>[] Inclusions { get; set; }
    }
}