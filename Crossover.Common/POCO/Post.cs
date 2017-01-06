using System;
using Crossover.Common.Abstractions;

namespace Crossover.Common.POCO
{
    public class Post : IPost
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}