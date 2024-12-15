using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VotingSys.ViewModels
{
    public class VoteVM
    {
        [Key]
        public int Id { get; }
        [Required]
        public string QuestionText { get; set; }
        [Required]
        public bool IsCurrent { get; set; }

        public List<VoteOptionVM> Options { get; set; }
    }
}