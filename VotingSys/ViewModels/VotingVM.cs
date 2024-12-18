using System;
using System.Collections.Generic;

namespace VotingSys.ViewModels
{
    public class VotingVM
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }

        public bool IsCurrent { get; set; }

        public DateTime CreationDate { get; set; }
        public List<VotingOptionVM> VotingOptions { get; set; } = new List<VotingOptionVM>();
    }

    public class VotingOptionVM
    {
        public int Id { get; set; }
        public string OptionText { get; set; }

        public int VoteId { get; set; }

        public int VoteCount { get; set; }
    }
}