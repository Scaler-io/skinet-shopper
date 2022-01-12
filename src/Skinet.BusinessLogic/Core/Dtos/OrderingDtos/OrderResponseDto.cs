using System;
using System.Collections.Generic;
using Skinet.Entities.Entities.OrderAggregate;

namespace Skinet.BusinessLogic.Core.Dtos.OrderingDtos
{
    public class OrderResponseDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public Address ShipToAddress { get; set; }
        public string DeliveryMethod { get; set; }
        public IReadOnlyList<OrderItemDto> OrderItems { get; set; }
        public decimal ShippingPrice { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
    }
}