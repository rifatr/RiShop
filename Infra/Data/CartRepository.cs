using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;

namespace Infra.Data
{
    public class CartRepository : ICartRepository
    {
        public Task<bool> DeleteCartAsync(string cartId)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerCart> GetCartAsync(string cartId)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerCart> UpdateCartAsync(CustomerCart customerCart)
        {
            throw new NotImplementedException();
        }
    }
}