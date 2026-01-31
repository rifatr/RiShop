namespace Core.Entities
{
    public class CustomerCart(string id)
    {
        public string Id { get; set; } = id;
        public List<CartItem> Items { get; set; } = [];
    }
}