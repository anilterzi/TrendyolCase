using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrendyolCaseStudy.Main;
using Moq;
using NUnit.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace TrendyolCaseStudy.Unit.Test
{

    [TestClass]
    public class ShoppingCartTest
    {
        Mock<IDeliveryCostCalculator> deliveryCostCalculator = new Mock<IDeliveryCostCalculator>();
        ShoppingCart cart;

        #region addproduct Tests
        [TestMethod]
        public void addProduct_OneProductPositiveQuantity_AddSuccess()
        {
            cart = new ShoppingCart(deliveryCostCalculator.Object);
            Product product = new Product("Computer", 1000.0, new Category("Electronic"));

            cart.addItem(product, 5);

            NUnit.Framework.Assert.AreEqual(cart.QuantityOfProduct.Count, 1);
            NUnit.Framework.Assert.IsTrue(cart.QuantityOfProduct.ContainsKey(product));
            NUnit.Framework.Assert.AreEqual(cart.QuantityOfProduct[product], 5);
        }

        [TestMethod]
        public void addProduct_OneProductNegativeQuantity_ThrowException()
        {
            cart = new ShoppingCart(deliveryCostCalculator.Object);
            Product product = new Product("Computer", 1000.0, new Category("Electronic"));

            NUnit.Framework.Assert.Throws<System.Exception>(() => cart.addItem(product, -5));
        }

        [TestMethod]
        public void addProduct_NullProductValidQuantity_ThrowException()
        {
            cart = new ShoppingCart(deliveryCostCalculator.Object);
            NUnit.Framework.Assert.Throws<System.Exception>(() => cart.addItem(null, 5));
        }

        [TestMethod]
        public void addProduct_ExistProductValidQuantity_AddSuccess()
        {
            cart = new ShoppingCart(deliveryCostCalculator.Object);
            Product product = new Product("Computer", 1000.0, new Category("Electronic"));

            cart.addItem(product, 5);
            cart.addItem(product, 3);

            NUnit.Framework.Assert.AreEqual(cart.QuantityOfProduct.Count, 1);
            NUnit.Framework.Assert.IsTrue(cart.QuantityOfProduct.ContainsKey(product));
            NUnit.Framework.Assert.AreEqual(cart.QuantityOfProduct[product], 8);
        }

        [TestMethod]
        public void addProduct_DifferentProductValidQuantity_TwoAddSuccess()
        {
            cart = new ShoppingCart(deliveryCostCalculator.Object);
            Product product1 = new Product("Computer", 1000.0, new Category("Electronic"));
            Product product2 = new Product("Tea", 10.0, new Category("Drink"));

            cart.addItem(product1, 3);
            cart.addItem(product2, 5);

            NUnit.Framework.Assert.AreEqual(cart.QuantityOfProduct.Count, 2);
            NUnit.Framework.Assert.IsTrue(cart.QuantityOfProduct.ContainsKey(product1));
            NUnit.Framework.Assert.IsTrue(cart.QuantityOfProduct.ContainsKey(product2));

            NUnit.Framework.Assert.AreEqual(cart.QuantityOfProduct[product1], 3);
            NUnit.Framework.Assert.AreEqual(cart.QuantityOfProduct[product2], 5);

        }
        #endregion

        #region getNumberOfDeliveries Tests
        [TestMethod]
        public void getNumberOfDeliveries_OneProductOneCategory_ReturnsOne()
        {
            cart = new ShoppingCart(deliveryCostCalculator.Object);
            Product product1 = new Product("Computer", 1000.0, new Category("Electronic"));

            cart.addItem(product1, 2);

            NUnit.Framework.Assert.AreEqual(cart.getNumberOfDeliveries(), 1);
        }

        [TestMethod]
        public void getNumberOfDeliveries_ZeroProduct_ReturnsZero()
        {
            cart = new ShoppingCart(deliveryCostCalculator.Object);
            NUnit.Framework.Assert.AreEqual(cart.getNumberOfDeliveries(), 0);
        }

        [TestMethod]
        public void getNumberOfDeliveries_TwoProductSameCategory_ReturnsOne()
        {
            cart = new ShoppingCart(deliveryCostCalculator.Object);
            Product product1 = new Product("Computer", 1000.0, new Category("Electronic"));
            Product product2 = new Product("Mobile Phone", 10.0, new Category("Electronic"));

            cart.addItem(product1, 3);
            cart.addItem(product2, 5);

            NUnit.Framework.Assert.AreEqual(cart.getNumberOfDeliveries(), 1);
        }

        [TestMethod]
        public void getNumberOfDeliveries_TwoProductDifferentCategory_ReturnsTwo()
        {
            cart = new ShoppingCart(deliveryCostCalculator.Object);
            Product product1 = new Product("Computer", 1000.0, new Category("Electronic"));
            Product product2 = new Product("Tea", 10.0, new Category("Drink"));

            cart.addItem(product1, 3);
            cart.addItem(product2, 5);

            NUnit.Framework.Assert.AreEqual(cart.getNumberOfDeliveries(), 2);
        }
        #endregion

        #region getNumberOfProdcuts Tests
        [TestMethod]
        public void getNumberOfProducts_QuantityOfProductsIsNull_ReturnsZero()
        {
            cart = new ShoppingCart(deliveryCostCalculator.Object);
            NUnit.Framework.Assert.AreEqual(cart.getNumberOfDeliveries(), 0);
        }

        [TestMethod]
        public void getNumberOfProducts_QuantityOfProductsIsNotNull_ReturnsQuantityOfProductsCount()
        {
            cart = new ShoppingCart(deliveryCostCalculator.Object);
            Product product1 = new Product("Computer", 1000.0, new Category("Electronic"));
            Product product2 = new Product("Tea", 10.0, new Category("Drink"));
            Product product3 = new Product("Mobile Phone", 500.0, new Category("Electronic"));

            cart.addItem(product1, 3);
            cart.addItem(product2, 5);
            cart.addItem(product3, 1);

            NUnit.Framework.Assert.AreEqual(cart.getNumberOfProducts(), cart.QuantityOfProduct.Count);
        }
        #endregion

        #region getCouponDiscount Tests

        [TestMethod]
        public void getCouponDiscount_NoCoupon_ReturnsZero()
        {
            cart = new ShoppingCart(deliveryCostCalculator.Object);
            Product product = new Product("Computer", 1000.0, new Category("Electronic"));

            cart.addItem(product, 5);
            NUnit.Framework.Assert.AreEqual(cart.getCouponDiscount(), 0);
        }

        [TestMethod]
        public void getCouponDiscount_OneProductLessThanMinProductTypeRate_ReturnsZero()
        {
            cart = new ShoppingCart(deliveryCostCalculator.Object);
            Product product = new Product("Computer", 100.0, new Category("Electronic"));
            Coupon coupon = new Coupon(400, 10, Discount.DiscountType.Rate);

            cart.addItem(product, 3);
            cart.applyCoupon(coupon);

            NUnit.Framework.Assert.AreEqual(cart.getCouponDiscount(), 0);
        }

        [TestMethod]
        public void getCouponDiscount_OneProductGreaterThanMinProductTypeRate_ReturnsThirty()
        {
            cart = new ShoppingCart(deliveryCostCalculator.Object);
            Product product = new Product("Computer", 100.0, new Category("Electronic"));
            Coupon coupon = new Coupon(100, 10, Discount.DiscountType.Rate);

            cart.addItem(product, 3);
            cart.applyCoupon(coupon);

            NUnit.Framework.Assert.AreEqual(cart.getCouponDiscount(), 30);
        }

        [TestMethod]
        public void getCouponDiscount_OneProductLessThanMinProductTypeAmount_ReturnsZero()
        {
            cart = new ShoppingCart(deliveryCostCalculator.Object);
            Product product = new Product("Computer", 100.0, new Category("Electronic"));
            Coupon coupon = new Coupon(400, 10, Discount.DiscountType.Amount);

            cart.addItem(product, 3);
            cart.applyCoupon(coupon);

            NUnit.Framework.Assert.AreEqual(cart.getCouponDiscount(), 0);
        }

        [TestMethod]
        public void getCouponDiscount_OneProductGreaterThanMinProductTypeAmount_ReturnsOneHundred()
        {
            cart = new ShoppingCart(deliveryCostCalculator.Object);
            Product product = new Product("Computer", 100.0, new Category("Electronic"));
            Coupon coupon = new Coupon(100, 100, Discount.DiscountType.Amount);

            cart.addItem(product, 3);
            cart.applyCoupon(coupon);

            NUnit.Framework.Assert.AreEqual(cart.getCouponDiscount(), 100);
        }

        [TestMethod]
        public void getCouponDiscount_TwoProductLessThanMinProductTypeRate_ReturnsZero()
        {
            cart = new ShoppingCart(deliveryCostCalculator.Object);
            Product product1 = new Product("Computer", 100.0, new Category("Electronic"));
            Product product2 = new Product("Tea", 2.0, new Category("Drink"));
            Coupon coupon = new Coupon(400, 10, Discount.DiscountType.Rate);

            cart.addItem(product1, 3);
            cart.addItem(product2, 3);
            cart.applyCoupon(coupon);

            NUnit.Framework.Assert.AreEqual(cart.getCouponDiscount(), 0);
        }

        [TestMethod]
        public void getCouponDiscount_TwoProductGreaterThanMinProductTypeRate_ReturnsFourty()
        {
            cart = new ShoppingCart(deliveryCostCalculator.Object);
            Product product1 = new Product("Computer", 100.0, new Category("Electronic"));
            Product product2 = new Product("Tea", 2.0, new Category("Drink"));
            Coupon coupon = new Coupon(100, 10, Discount.DiscountType.Rate);

            cart.addItem(product1, 3);
            cart.addItem(product2, 50);
            cart.applyCoupon(coupon);

            NUnit.Framework.Assert.AreEqual(cart.getCouponDiscount(), 40);
        }

        [TestMethod]
        public void getCouponDiscount_TwoProductLessThanMinProductTypeAmount_ReturnsZero()
        {
            cart = new ShoppingCart(deliveryCostCalculator.Object);
            Product product1 = new Product("Computer", 100.0, new Category("Electronic"));
            Product product2 = new Product("Tea", 2.0, new Category("Drink"));
            Coupon coupon = new Coupon(400, 10, Discount.DiscountType.Amount);

            cart.addItem(product1, 3);
            cart.addItem(product2, 3);
            cart.applyCoupon(coupon);

            NUnit.Framework.Assert.AreEqual(cart.getCouponDiscount(), 0);
        }

        [TestMethod]
        public void getCouponDiscount_TwoProductGreaterThanMinProductTypeAmount_ReturnsOneHundred()
        {
            cart = new ShoppingCart(deliveryCostCalculator.Object);
            Product product1 = new Product("Computer", 100.0, new Category("Electronic"));
            Product product2 = new Product("Tea", 2.0, new Category("Drink"));
            Coupon coupon = new Coupon(100, 100, Discount.DiscountType.Amount);

            cart.addItem(product1, 3);
            cart.addItem(product2, 50);
            cart.applyCoupon(coupon);

            NUnit.Framework.Assert.AreEqual(cart.getCouponDiscount(), 100);
        }

        #endregion

        #region getCampaignDiscount Tests

        [TestMethod]
        public void getCampaignDiscount_NoCampaign_ReturnsZero()
        {
            cart = new ShoppingCart(deliveryCostCalculator.Object);
            Product product = new Product("Computer", 1000.0, new Category("Electronic"));
            cart.addItem(product, 5);

            NUnit.Framework.Assert.AreEqual(cart.getCampaignDiscount(), 0);
        }
        [TestMethod]
        public void getCampaignDiscount_ValidCampaignLessThanThreeProductGreaterThanMinProductTypeRate_ReturnsExpected()
        {
            cart = new ShoppingCart(deliveryCostCalculator.Object);
            Product product = new Product("Computer", 1000.0, new Category("Electronic"));

            cart.addItem(product, 2);
            Campaign campaign = new Campaign(1, 10, Discount.DiscountType.Rate,new Category("Electronic"));

            double expected = 2000 * (10 / 100);
            NUnit.Framework.Assert.AreEqual(cart.getCampaignDiscount(), expected);
        }
        [TestMethod]
        public void getCampaignDiscount_ValidCampaignLessThanThreeProductLessThanMinProductTypeRate_ReturnsZero()
        {
            cart = new ShoppingCart(deliveryCostCalculator.Object);
            Product product = new Product("Computer", 1000.0, new Category("Electronic"));

            cart.addItem(product, 2);
            Campaign campaign = new Campaign(3, 10, Discount.DiscountType.Rate, new Category("Electronic"));
            NUnit.Framework.Assert.AreEqual(cart.getCampaignDiscount(), 0);
        }
        [TestMethod]
        public void getCampaignDiscount_ValidCampaignBetweenThreeAndFiveProductGreaterThanMinProductTypeRate_ReturnsExpected()
        {
            cart = new ShoppingCart(deliveryCostCalculator.Object);
            Product product = new Product("Computer", 1000.0, new Category("Electronic"));

            cart.addItem(product, 2);
            Campaign campaign = new Campaign(1, 10, Discount.DiscountType.Rate, new Category("Electronic"));

            double expected = 2000 * (20 / 100);
            NUnit.Framework.Assert.AreEqual(cart.getCampaignDiscount(), expected);
        }
        [TestMethod]
        public void getCampaignDiscount_ValidCampaignBetweenThreeAndFiveProductLessThanMinProductTypeRate_ReturnsZero()
        {
            cart = new ShoppingCart(deliveryCostCalculator.Object);
            Product product = new Product("Computer", 1000.0, new Category("Electronic"));

            cart.addItem(product, 2);
            Campaign campaign = new Campaign(3, 10, Discount.DiscountType.Rate, new Category("Electronic"));
            NUnit.Framework.Assert.AreEqual(cart.getCampaignDiscount(), 0);
        }
       
        [TestMethod]
        public void getCampaignDiscount_ValidCampaignLessThanFiveProductLessThanMinProductTypeAmount_ReturnsZero()
        {
            cart = new ShoppingCart(deliveryCostCalculator.Object);
            Product product = new Product("Computer", 1000.0, new Category("Electronic"));

            cart.addItem(product, 2);
            Campaign campaign = new Campaign(3, 100.0, Discount.DiscountType.Amount, new Category("Electronic"));

            NUnit.Framework.Assert.AreEqual(cart.getCampaignDiscount(), 0);
        }
        
        [TestMethod]
        public void getCampaignDiscount_ValidCampaignGreaterThanFiveProductLessThanMinProductTypeAmount_ReturnsZero()
        {
            cart = new ShoppingCart(deliveryCostCalculator.Object);
            Product product = new Product("Computer", 1000.0, new Category("Electronic"));

            cart.addItem(product, 6);
            Campaign campaign = new Campaign(7, 100.0, Discount.DiscountType.Amount, new Category("Electronic"));

            NUnit.Framework.Assert.AreEqual(0, cart.getCampaignDiscount());
        }
        #endregion

        #region getDeliveryCost Tests
        [TestMethod]
        public void getDeliveryCost_CalculateForReturnsValidValue_ReturnsValidValue()
        {
            ShoppingCart cart = new ShoppingCart(deliveryCostCalculator.Object);
            deliveryCostCalculator.Setup(a => a.CalculateFor(cart)).Returns(100);
            NUnit.Framework.Assert.AreEqual(cart.getDeliveryCost(), 100);
        }
        #endregion

        #region getTotalAmountAfterDiscounts Test
        [TestMethod]
        public void getTotalAmountAfterDiscounts_OneProductNoDiscount_ReturnsTotalAmount()
        {
            cart = new ShoppingCart(deliveryCostCalculator.Object);
            Product product = new Product("Computer", 1000.0, new Category("Electronic"));
            cart.addItem(product, 2);
            double expected = 2000.0;
            NUnit.Framework.Assert.AreEqual(expected, cart.getTotalAmountAfterDiscounts());
        }

        public void getTotalAmountAfterDiscounts_SumOfCampaignAndCouponGreaterThanTotal_ReturnsZero()
        {
            Mock<IShoppingCart> shoppingCart = new Mock<IShoppingCart>();
            cart = new ShoppingCart(deliveryCostCalculator.Object);
            Product product = new Product("Computer", 1000.0, new Category("Electronic"));

            cart.addItem(product, 2);
            shoppingCart.Setup(a => a.applyCampaign(1)).Returns(1000);
            shoppingCart.Setup(a => a.applyCoupon(2)).Returns(2000);

            NUnit.Framework.Assert.AreEqual(0,cart.getTotalAmountAfterDiscounts());
        }
        #endregion
    }
}