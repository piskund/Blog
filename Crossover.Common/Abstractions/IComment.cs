using System;

namespace Crossover.Common.Abstractions
{
    public interface IComment
    {
        Guid Id { get; set; }

        Guid PostId { get; set; }

        string Text { get; set; }
    }
}