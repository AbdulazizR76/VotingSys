using System;
using System.Collections.Generic;

namespace VotingSys.Models
{
    //[Table("Votes")] Data Annotation
    public class Vote
    {
        public int Id { get; set; }


        public string QuestionText { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsCurrent { get; set; }

        /// <summary>
        /// One To Many With VoteOption
        /// </summary>
        public virtual ICollection<VoteOption> VoteOptions { get; set; }
    }
}

