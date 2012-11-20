using System;

namespace NewYork.AuctionHouse.Models
{
    public class BidModel
    {
        public int Id { get; set; }
        public int AuctionId { get; set; }
        public int Amount { get; set; }
        public string NickName { get; set; }
        public DateTime Created { get; set; }
    }
}
