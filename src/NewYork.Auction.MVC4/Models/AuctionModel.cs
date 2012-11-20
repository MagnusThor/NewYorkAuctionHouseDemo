using System;
using System.Collections.Generic;

namespace NewYork.AuctionHouse.Models
{
    public class AuctionModel
    {
        public void Action()
        {
            this.Bids = new List<BidModel>();
        }
        public int Id { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public DateTime Created { get; set; }
        public List<BidModel> Bids { get; set; }
        public int LatestBid { get; set; }
        
       
    }
}