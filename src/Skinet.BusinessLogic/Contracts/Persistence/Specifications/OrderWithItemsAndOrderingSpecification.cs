using System;
using System.Linq.Expressions;
using Skinet.Entities.Entities.OrderAggregate;

namespace Skinet.BusinessLogic.Contracts.Persistence.Specifications
{
    public class OrderWithItemsAndOrderingSpecification : BaseSpecification<Order>
    {
        public OrderWithItemsAndOrderingSpecification(string email)
        :base(o => o.BuyerEmail == email)
        {
            AddIncludes(o => o.DeliveryMethod);
            AddIncludes(o => o.OrderItems);
            AddOrderByDescending(o => o.OrderDate);
        }

        public OrderWithItemsAndOrderingSpecification(int id, string email) 
        : base(o => o.Id == id && o.BuyerEmail == email)
        {
            AddIncludes(o => o.DeliveryMethod);
            AddIncludes(o => o.OrderItems);
        }
    }
}