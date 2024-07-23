using BethanysPieShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BethanysPieShop.Pages
{
    public class CheckoutPageModel : PageModel
    {
        private readonly IShoppingCart _shoppingcart;
        private readonly IOrderRepository _orderRepository;

        public CheckoutPageModel(IOrderRepository orderRepository, IShoppingCart shoppingCart)
        {
            _orderRepository = orderRepository;
            _shoppingcart = shoppingCart;
        }
        [BindProperty]
        public Order Order { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var items = _shoppingcart.GetShoppingCartItems();
            _shoppingcart.ShoppingCartItems = items;

                if (_shoppingcart.ShoppingCartItems.Count == 0)
                {
                    ModelState.AddModelError("", "Your cart is empty, add some pies first");
                }
            if (ModelState.IsValid)
            {
                _orderRepository.CreateOrder(Order);
                _shoppingcart.ClearCart();
                return RedirectToAction("CheckoutComplete");
            }
                return Page();
        }
    }
}
