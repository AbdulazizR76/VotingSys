using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using VotingSys.Models;
using VotingSys.ViewModels;

namespace VotingSys.Controllers
{

    public class VoteController : Controller
    {
        private DataContext context = new DataContext();

        [HttpGet]
        public ActionResult Index()
        {
            List<Vote> votes = context.Votes.ToList();
            return View(votes);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,QuestionText")] VoteVM vote)
        {
            if (ModelState.IsValid)
            {
                var v = new Vote
                {
                    QuestionText = vote.QuestionText,
                    IsCurrent = vote.IsCurrent,
                    CreationDate = DateTime.Now,
                };
                context.Votes.Add(v);
                context.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(vote);

        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var vote = context.Votes.SingleOrDefault(x => x.Id == Id);
            if (vote == null)
            {
                return HttpNotFound();
            }
            return View(vote);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Vote vote)
        {
            var Votes = context.Votes.SingleOrDefault(v => v.Id == vote.Id);


            if (ModelState.IsValid)
            {
                Votes.QuestionText = vote.QuestionText;
                Votes.IsCurrent = vote.IsCurrent;

                context.SaveChanges();
                return RedirectToAction("Index");
            }


            if (Votes == null)
            {
                return HttpNotFound();
            }

            return View(Votes);
        }


        [HttpPost]
        public ActionResult Delete(int Id)
        {
            var vote = context.Votes.SingleOrDefault(y => y.Id == Id);
            if (ModelState.IsValid)
            {
                context.Votes.Remove(vote);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            {

            }
            if (vote == null)
            {
                return HttpNotFound();
            }

            return View(vote);
        }

        [HttpGet]
        public ActionResult AddOption(int voteId)
        {
            var vote = context.Votes.SingleOrDefault(v => v.Id == voteId);
            if (vote == null)
            {
                return HttpNotFound("Vote not found.");
            }

            var model = new VoteOptionVM
            {
                VoteId = voteId
            };
            return View(model);

        }
        [HttpPost]
        public ActionResult AddOption(VoteOptionVM option)
        {
            if (ModelState.IsValid)
            {
                var model = new VoteOption
                {
                    OptionText = option.OptionText,
                    VoteId = option.VoteId,
                };

                context.VotesOption.Add(model);
                context.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(option);
        }



        [HttpGet]
        public ActionResult ShowOptions(int voteId)
        {
            var result = context.Votes
                .Where(x => x.Id == voteId).Include(x => x.VoteOptions).Select(x => new
                {
                    Id = x.Id,
                    IsCurrent = x.IsCurrent,
                    QuestionText = x.QuestionText,
                    Options = x.VoteOptions

                }).FirstOrDefault();

            if (result != null)
            {
                var model = new VoteVM();

                model.QuestionText = result.QuestionText;
                model.IsCurrent = result.IsCurrent;
                model.Id = result.Id;


                foreach (var option in result.Options)
                {
                    model.Options.Add(new VoteOptionVM
                    {
                        OptionText = option.OptionText,
                        VoteId = option.VoteId,
                        Id = option.Id
                    });
                }


                return View(model);
            }


            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult EditOptions(int optionId)
        {

            var option = context.VotesOption.FirstOrDefault(x => x.Id == optionId);
            if (option != null)
            {
                var model = new VoteOptionVM
                {
                    OptionText = option.OptionText,
                    VoteId = option.VoteId,
                    Id = option.Id
                };
                return View(model);
            }
            return HttpNotFound();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOptions(VoteOptionVM model)
        {

            if (ModelState.IsValid)
            {
                var option = context.VotesOption.FirstOrDefault(v => v.Id == model.Id);
                if (option == null) return HttpNotFound();

                option.OptionText = model.OptionText;
                context.SaveChanges();
                return RedirectToAction("index");


            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult DeleteOption(int optionId)
        {
            if (ModelState.IsValid)
            {
                var option = context.VotesOption.FirstOrDefault(v => v.Id == optionId);
                if (option == null) return HttpNotFound();

                context.VotesOption.Remove(option);
                context.SaveChanges();
                return RedirectToAction("ShowOptions");
            }
            return HttpNotFound();

        }








    }
}