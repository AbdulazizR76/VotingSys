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




        [HttpPost]
        public ActionResult Submit(IEnumerable<VotingVM> model)
        {


            foreach (var questions in model)
            {
                foreach (var option in questions.VotingOptions.Where(x => x.IsSelected == true))
                {
                    // 
                    var opentionEntity = context.VotesOption.FirstOrDefault(o => o.Id == option.Id);
                    if (opentionEntity != null)
                    {
                        option.VoteCount++;
                        context.SaveChanges();
                    }
                }
            }


            context.SaveChanges();
            ViewBag.Message = "Your votes have been submitted successfully!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Submit222(Dictionary<int, int> SelectedOptions)
        {

            foreach (var selection in SelectedOptions)
            {
                int questionId = selection.Key;
                int selectedOptionId = selection.Value;


                var option = context.VotesOption.FirstOrDefault(o => o.Id == selectedOptionId && o.VoteId == questionId);
                if (option != null)
                {

                    option.VoteCount += 1;
                }
                else
                {

                    return HttpNotFound();
                }
            }


            context.SaveChanges();
            ViewBag.Message = "Your votes have been submitted successfully!";
            return RedirectToAction("Index");
        }
    }
}