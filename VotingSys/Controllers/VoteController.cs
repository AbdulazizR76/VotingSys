using System;
using System.Collections.Generic;
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

        public ActionResult ShowOptions(int voteId)
        {
            var vote = context.Votes.SingleOrDefault(v => v.Id == voteId);
            if (vote == null)
            {
                return HttpNotFound();
            }
            var model = new VoteOptionVM { VoteId = voteId };

            return View(model);
        }


    }
}