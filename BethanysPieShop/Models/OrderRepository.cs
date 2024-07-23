namespace BethanysPieShop.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BethanysPieShopDbContext _bethanysPieShopDbContext;
        private readonly IShoppingCart _shoppingCart;

        public OrderRepository(BethanysPieShopDbContext bethanysPieShopDbContext, IShoppingCart shoppingCart)
        {
            _bethanysPieShopDbContext = bethanysPieShopDbContext;
            _shoppingCart = shoppingCart;
        }

        public void CreateOrder(Order order)
        {
            //order is created once method is evoked
            order.OrderPlaced = DateTime.Now;

            //get all items in the shopping cart
            List<ShoppingCartItem>? shoppingCartItems = _shoppingCart.ShoppingCartItems;
            
            //get shopping cart total to populate OrderTotal
            order.OrderTotal = _shoppingCart.GetShoppingCartTotal();

            //creating a new list
            order.OrderDetails = new List<OrderDetail>();

            //populate list by adding each order with details
            foreach (ShoppingCartItem? item in shoppingCartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Amount = item.Amount,
                    PieId = item.Pie.PieId,
                    Price = item.Pie.Price
                };
                order.OrderDetails.Add(orderDetail);
            }
            _bethanysPieShopDbContext.Add(order);
            _bethanysPieShopDbContext.SaveChanges();
        }
    }
}
