namespace Crossover.Common.Abstractions
{
    public interface IComment
    {
        int Id { get; set; }

        int PostId { get; set; }

        string Text { get; set; }
    }
}