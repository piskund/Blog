using System;
using System.Linq;
using Crossover.Common.Abstractions;

namespace Crossover.Core.Abstractions
{
    public interface IPostRepository
    {
        IPost Get(Guid id);

        void Create(IPost post);

        void Update(IPost post);

        void Delete(Guid id);

        IQueryable<IPost> GetAll();
    }
}