using System.Data.Entity;

namespace VotingSys.Models
{
    public class DataContext : DbContext
    {
        public DataContext() : base("VoteSysConn")
        {
        }

        /// <summary>
        /// Votes Table.
        /// </summary>
        public DbSet<Vote> Votes { get; set; }


        /// <summary>
        /// VoteOptions Table.
        /// </summary>
        public DbSet<VoteOption> VotesOption { get; set; }


    }
}