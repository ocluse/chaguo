

using MudBlazor;

namespace Chaguo.Data
{
    public class Constituency
    {
        public int Ruto { get; set; }
        public int Raila { get; set; }
        public int Wajackoyah { get; set; }
        public int Mwaure { get; set; }
        public int Rejected { get; set; }
        public bool Polled { get; set; }
    }

    public class Candidate
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Party { get; set; }
        public int TotalVotes { get; set; }
        public double Percent => (double)TotalVotes / _totalProvider.TotalVotes;
        public string ImageSource { get; set; }
        public Color Color { get; set; }
        private readonly ITotalProvider _totalProvider;
        public Candidate(string firstName, string lastName, string party, int totalVotes, ITotalProvider totalProvider, Color color, string imageSource)
        {
            FirstName = firstName;
            LastName = lastName;
            Party = party;
            TotalVotes = totalVotes;
            _totalProvider = totalProvider;
            Color = color;
            ImageSource = imageSource;
        }
    }

    public interface ITotalProvider
    {
        int TotalVotes { get; }
    }
}
