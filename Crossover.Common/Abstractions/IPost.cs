using System;

namespace Crossover.Common.Abstractions
{
    public interface IPost
    {
        int Id { get; set; }

        DateTime Date { get; set; }

        string Title { get; set; }

        string Body { get; set; }
    }
}