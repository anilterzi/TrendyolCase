using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrendyolCaseStudy.Main;
using Moq;
using NUnit.Framework;

namespace TrendyolCaseStudy.Unit.Test
{
    [TestClass]
    public class DeliveryCostCalculatorTest
    {
        Mock<IShoppingCart> cart = new Mock<IShoppingCart>();
        
        [TestMethod]
        public void CalculateFor_GivenNullCart_ThrowsNullReferenceException()
        {
            DeliveryCostCalculator deliveryCostCalculator = new DeliveryCostCalculator(10, 2);
            NUnit.Framework.Assert.Throws<System.NullReferenceException>(() => deliveryCostCalculator.CalculateFor(null));
        }
        [TestMethod]
        public void CalculateFor_GivenNegativeCostOfPerDelivery_ThrowsException()
        {
            DeliveryCostCalculator deliveryCostCalculator = new DeliveryCostCalculator(-1, 10);
            cart.Setup(a => a.getNumberOfDeliveries()).Returns(2);
            cart.Setup(a => a.getNumberOfProducts()).Returns(3);
            NUnit.Framework.Assert.Throws<System.Exception>(() => deliveryCostCalculator.CalculateFor(cart.Object));
        }
        [TestMethod]
        public void CalculateFor_GivenNegativeCostOfPerProduct_ThrowsException()
        {
            DeliveryCostCalculator deliveryCostCalculator = new DeliveryCostCalculator(10,-1);
            cart.Setup(a => a.getNumberOfDeliveries()).Returns(2);
            cart.Setup(a => a.getNumberOfProducts()).Returns(3);
            NUnit.Framework.Assert.Throws<System.Exception>(() => deliveryCostCalculator.CalculateFor(cart.Object));
        }

        [TestMethod]
        public void CalculateFor_GivenNegativeNumberOfDeliveries_ThrowsException()
        {
            DeliveryCostCalculator deliveryCostCalculator = new DeliveryCostCalculator(10, 2);
            cart.Setup(a => a.getNumberOfDeliveries()).Returns(-2);
            cart.Setup(a => a.getNumberOfProducts()).Returns(3);
            NUnit.Framework.Assert.Throws<System.Exception>(() => deliveryCostCalculator.CalculateFor(cart.Object));
        }
        [TestMethod]
        public void CalculateFor_GivenNegativeNumberOfProducts_ThrowsException()
        {
            DeliveryCostCalculator deliveryCostCalculator = new DeliveryCostCalculator(10, 2);
            cart.Setup(a => a.getNumberOfDeliveries()).Returns(2);
            cart.Setup(a => a.getNumberOfProducts()).Returns(-3);
            NUnit.Framework.Assert.Throws<System.Exception>(() => deliveryCostCalculator.CalculateFor(cart.Object));
        }

        [TestMethod]
        public void CalculateFor_GivenZeroProductAndDeliveries_ReturnFixedCost()
        {
            DeliveryCostCalculator deliveryCostCalculator = new DeliveryCostCalculator(10, 2);

            cart.Setup(a => a.getNumberOfDeliveries()).Returns(0);
            cart.Setup(a => a.getNumberOfProducts()).Returns(0);
            NUnit.Framework.Assert.That(deliveryCostCalculator.CalculateFor(cart.Object) == 2.99);
        }
        [TestMethod]
        public void CalculateFor_GivenPositiveDeliveryPositiveProduct_ReturnsExceptedCalculation()
        {
            DeliveryCostCalculator deliveryCostCalculator = new DeliveryCostCalculator(10, 2);
            cart.Setup(m => m.getNumberOfDeliveries()).Returns(3);
            cart.Setup(m => m.getNumberOfProducts()).Returns(2);

            double expectedCalculation = (10 * 3) + (2 * 2) + 2.99;

            NUnit.Framework.Assert.That(deliveryCostCalculator.CalculateFor(cart.Object) == expectedCalculation);
        }


    }
}
