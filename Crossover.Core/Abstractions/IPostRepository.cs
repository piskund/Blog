using System.Linq;
using Crossover.Common.Abstractions;

namespace Crossover.Core.Abstractions
{
    public interface IPostRepository
    {
        IPost Get(int id);

        void Create(IPost post);

        void Update(IPost post);

        void Delete(int id);

        IQueryable<IPost> GetAll();
    }
}