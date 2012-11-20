using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace NewYork.Core.Model
{
    public class Auction : PersistentEntity
    {
        public string Caption { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public virtual List<Bids> Bids { get; set; }
        public int LatestBid { get; set; }
        
       
    }
}