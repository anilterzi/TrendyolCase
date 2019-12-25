using System;
using TrendyolCaseStudy.Main;

namespace TrendyolCaseStudy.Console
{
    class Console
    {
        static void Main(string[] args)
        {
            Category electronic = new Category("Electronic");
            Category food = new Category("Food");
            Category fruit = new Category("Fruit",food);

            Product computer = new Product(title:"Computer", price:100, category:electronic);
            Product orange = new Product(title:"Orange", price:50, category:fruit);
            Product soup = new Product(title: "Soup", price: 10, category: food);

            ShoppingCart cart = new ShoppingCart(new DeliveryCostCalculator(1.5, 10));
            cart.addItem(computer, 10);
            cart.addItem(orange, 5);
            cart.addItem(soup, 2);

            Campaign c1 = new Campaign(category : fruit, minProductAmount: 5, discountAmount: 10, type : Discount.DiscountType.Rate);
            Campaign c2 = new Campaign(category : electronic, minProductAmount: 5, discountAmount: 30, type : Discount.DiscountType.Rate);
            Coupon c3 = new Coupon(minProductAmount: 3, discountAmount: 10, type: Discount.DiscountType.Rate);

            cart.applyDiscount(c1, c2);
            cart.applyCoupon(c3);


            cart.print();
            System.Console.ReadLine();

        }
    }
}
