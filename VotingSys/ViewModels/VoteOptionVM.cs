using System.ComponentModel.DataAnnotations;

namespace VotingSys.ViewModels
{
    public class VoteOptionVM
    {
        public int Id { get; set; }
        [Required]
        public string OptionText { get; set; }
        [Required]
        public int VoteId { get; set; }
    }
}