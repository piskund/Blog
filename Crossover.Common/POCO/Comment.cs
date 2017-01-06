using System;
using Crossover.Common.Abstractions;

namespace Crossover.Common.POCO
{
    public class Comment : IComment
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public string Text { get; set; }
    }
}