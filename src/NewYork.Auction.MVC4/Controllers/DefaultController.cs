using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NewYork.AuctionHouse.Models;
using NewYork.Core.Interfaces.Service;
using NewYork.Core.Model;

namespace NewYork.AuctionHouse.Controllers


{


    public static class Helpers
    {
        public static AuctionModel AuctionModelFromEntiry(Auction auction)

        {
            var model = new AuctionModel()
                {
                    Id = auction.Id,
                    Caption = auction.Caption,
                    Description = auction.Description,
                    Price = auction.Price,
                    Created = DateTime.Parse(auction.Created),
                    LatestBid = auction.LatestBid,
                    Bids = (from b in auction.Bids
                            select new BidModel()
                                {
                                    Id = b.Id,
                                    Amount = b.Amount,
                                    Created = DateTime.Parse(b.Created),
                                    AuctionId = auction.Id
                                }).ToList()
                };
            return model;
        }
    }

    public class DefaultController : Controller
    {

      //  private static readonly List<Auction> Auctions;


        private IAuctionService _auctionService;

        public DefaultController(IAuctionService auctionService)
        {
            _auctionService = auctionService;
        }

        
        //
        // GET: /Default/
    
        public ActionResult Index()
        {
            return View();
        }

        // Just add the "model" to our static list 
        [HttpPost]
        public JsonResult AddAuctinon(AuctionModel auctionModel)
        {

            var auction = new Auction();

            auction.Caption = auctionModel.Caption;
            auction.Price = auctionModel.Price;
            auction.Description = auctionModel.Description;
            auction.Bids = new List<Bids>();

            auction.LatestBid = 0;

            _auctionService.SaveOrUpdate(auction);



            return new JsonResult { Data = Helpers.AuctionModelFromEntiry(auction), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult PlaceBid(BidModel bidModel)
        {

            var auction = _auctionService.GetById(bidModel.AuctionId);

            auction.LatestBid = bidModel.Amount;

            var bid = new Bids {Amount = bidModel.Amount, NickName = bidModel.NickName};
            auction.Bids.Add(bid);

            _auctionService.SaveOrUpdate(auction);
           


            return new JsonResult { Data = Helpers.AuctionModelFromEntiry(auction), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
         

            //try
            //{
            //    var auction = Auctions.Single(a => a.Guid.Equals(bids.AuctionGuid));
                
            //    bids.Created = DateTime.Now;

            //    auction.LatestBid = bids.Amount;
            //    auction.Bids.Add(bids);

            //    return new JsonResult {Data = auction, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
            //}
            //catch
            //{
            //    return new JsonResult { Data = bids, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
          
            //}
            return null;
        }

        /// <summary>
        /// Just a simple Auction that returns all the Auction Model from our static list
        /// </summary>
        /// <returns></returns>
        public JsonResult All()
        {
         
            var auctions = _auctionService.GetAll().OrderByDescending(o => o.Created).Take(20);
            var auctionModels = new List<AuctionModel>();
            foreach (var auction in auctions)
            {
                auctionModels.Add(Helpers.AuctionModelFromEntiry(auction));
            }

            return new JsonResult {Data = auctionModels, JsonRequestBehavior = JsonRequestBehavior.AllowGet};

            //return new JsonResult { Data = Auctions.OrderByDescending(o => o.Created), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        //public ActionResult WebRTC()
        //{
        //    return View();
        //}

    }


}
