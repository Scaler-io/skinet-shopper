using System;
using System.Linq.Expressions;
using Skinet.Entities.Entities.OrderAggregate;

namespace Skinet.BusinessLogic.Contracts.Persistence.Specifications
{
    public class OrderByPaymentIntentIdWithItemsSpecification : BaseSpecification<Order>
    {
        public OrderByPaymentIntentIdWithItemsSpecification(string paymentIntentId) : 
            base(o => o.PaymentIntentId == paymentIntentId)
        {
        }
    }
}