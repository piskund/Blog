using System;
using System.Linq;
using Crossover.Common.Abstractions;
using Crossover.Core.Abstractions;

namespace Crossover.WebApi.Selfhosting
{
    internal class FakePostRepository : IPostRepository
    {
        public IPost Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Create(IPost post)
        {
            throw new NotImplementedException();
        }

        public void Update(IPost post)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<IPost> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}