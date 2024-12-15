using VotingSys.Models;

namespace VotingSys.Services
{
    public interface IVoteData
    {
        Vote GetVote(int id);
        void Add(Vote vote);
        void Update(Vote vote);
        void Delete(Vote vote);

    }
}
