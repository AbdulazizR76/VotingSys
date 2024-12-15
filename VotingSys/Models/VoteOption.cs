using System.ComponentModel.DataAnnotations.Schema;

namespace VotingSys.Models
{
    public class VoteOption
    {
        public int Id { get; set; }
        public string OptionText { get; set; }

        [ForeignKey("Vote")]
        public int VoteId { get; set; }
        public int VoteCount { get; set; }

        /// <summary>
        /// للربط مع جدول آخر
        /// </summary>
        public virtual Vote Vote { get; set; }
    }
}


