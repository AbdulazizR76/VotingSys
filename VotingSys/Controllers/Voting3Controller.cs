using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using VotingSys.Models;
using VotingSys.ViewModels;

namespace VotingSys.Controllers
{
    public class Voting3Controller : Controller
    {
        private DataContext context = new DataContext();
        // GET: Voting3

        public ActionResult Index()
        {
            var votes = context.Votes
                .Include(x => x.VoteOptions)
             .Select(v => new
             {
                 Id = v.Id,
                 QuestionText = v.QuestionText,
                 IsCurrent = v.IsCurrent,
                 CreationDate = v.CreationDate,
                 VotingOptions = v.VoteOptions,
             }).ToList();


            var Li = new List<VotingVM>();
            foreach (var vote in votes)
            {
                Li.Add(new VotingVM
                {
                    Id = vote.Id,
                    CreationDate = vote.CreationDate,
                    IsCurrent = vote.IsCurrent,
                    QuestionText = vote.QuestionText,
                    VotingOptions = vote.VotingOptions.Select(x => new VotingOptionVM
                    {
                        Id = x.Id,
                        OptionText = x.OptionText,
                        VoteCount = x.VoteCount,
                        VoteId = x.VoteId
                    }).ToList()
                });

            }

            return View(Li);
        }

        public ActionResult Submit(IEnumerable<VotingVM> model)
        {


            var votes = context.Votes
                  .Include(x => x.VoteOptions)
               .Select(v => new
               {
                   Id = v.Id,
                   QuestionText = v.QuestionText,
                   IsCurrent = v.IsCurrent,
                   CreationDate = v.CreationDate,
                   VotingOptions = v.VoteOptions,
               }).ToList();

            var Li = new List<VotingVM>();
            foreach (var vote in votes)
            {
                Li.Add(new VotingVM
                {
                    Id = vote.Id,
                    CreationDate = vote.CreationDate,
                    IsCurrent = vote.IsCurrent,
                    QuestionText = vote.QuestionText,
                    VotingOptions = vote.VotingOptions.Select(x => new VotingOptionVM
                    {
                        Id = x.Id,
                        OptionText = x.OptionText,
                        VoteCount = x.VoteCount,
                        VoteId = x.VoteId
                    }).ToList()
                });
            }





            return View(votes);

        }
    }
}