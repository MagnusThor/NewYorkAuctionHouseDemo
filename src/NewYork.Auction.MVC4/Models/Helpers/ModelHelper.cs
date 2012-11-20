using System;
using System.Linq;
using NewYork.Core.Model;

namespace NewYork.AuctionHouse.Models.Helpers
{
    public static class ModelHelper
    {
        public static AuctionModel Auction(Auction auction)

        {
            var model = new AuctionModel()
                {
                    Id = auction.Id,
                    Caption = auction.Caption,
                    Description = auction.Description,
                    Price = auction.Price,
                    Created = DateTime.Parse(auction.Created),
                    LatestBid = auction.LatestBid,
                    Bids = (auction.Bids.Select(b => new BidModel
                        {
                            NickName = "John Doe",
                            Id = b.Id,
                            Amount = b.Amount,
                            Created = DateTime.Parse(b.Created),
                            AuctionId = auction.Id,
                        })).ToList()
                };
            return model;
        }
    }
}