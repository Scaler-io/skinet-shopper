﻿using Skinet.Entities.Common;

namespace Skinet.Entities.Entities.OrderAggregate
{
    public class DeliveryMethod : BaseEntity
    {
        public string ShortName { get; set; }
        public string DeliveryTime { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
