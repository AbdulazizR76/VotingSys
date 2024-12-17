using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VotingSys.ViewModels
{
    public class VoteVM
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string QuestionText { get; set; }
        [Required]
        public bool IsCurrent { get; set; }
        public DateTime CreationDate { get; set; }

        public List<VoteOptionVM> Options { get; set; } = new List<VoteOptionVM>();
    }
}