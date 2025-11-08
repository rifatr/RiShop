using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using StackExchange.Redis;

namespace Infra.Data
{
    public class CartRepository(IConnectionMultiplexer redis) : ICartRepository
    {
        private readonly IDatabase _database = redis.GetDatabase();

        public async Task<bool> DeleteCartAsync(string cartId)
        {
            return await _database.KeyDeleteAsync(cartId); 
        }

        public async Task<CustomerCart> GetCartAsync(string cartId)
        {
            var cart = await _database.StringGetAsync(cartId);

            return cart.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerCart>(cart);
        }

        public async Task<CustomerCart> CreateOrUpdateCartAsync(CustomerCart customerCart)
        {
            var createdCart = await _database.StringSetAsync(customerCart.Id, JsonSerializer.Serialize(customerCart), TimeSpan.FromDays(30.0));

            if (!createdCart) return null;

            return await GetCartAsync(customerCart.Id);
        }
    }
}