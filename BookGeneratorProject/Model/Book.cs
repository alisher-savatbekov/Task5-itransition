using System.ComponentModel.DataAnnotations.Schema;

namespace BookGeneratorProject.Model
{
    public class Book
    {
        public int Index { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public List<string> Authors { get; set; } = new();
        public string Publisher { get; set; } = string.Empty;
        public int Likes { get; set; }
        public int Reviews { get; set; }
    }
}
