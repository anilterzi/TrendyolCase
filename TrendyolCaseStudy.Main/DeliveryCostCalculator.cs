using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrendyolCaseStudy.Main
{
    public class DeliveryCostCalculator : IDeliveryCostCalculator
    {
        public DeliveryCostCalculator(double costOfPerDelivery, double costOfPerProduct, double costOfFix = 2.99)
        {

            this.CostOfPerDelivery = costOfPerDelivery;
            this.CostOfPerProduct = costOfPerProduct;
            this.CostOfFix = costOfFix;

        }

        public double CostOfPerDelivery { get; set; }

        public double CostOfPerProduct { get; set; }

        public double CostOfFix { get; set; }

        public double CalculateFor(IShoppingCart cart)
        {
            if (cart == null)
            {
                throw new NullReferenceException();
            }
            int numberOfDelivery = cart.getNumberOfDeliveries();
            int numberOfProduct = cart.getNumberOfProducts();

            if (CostOfPerDelivery >= 0 && CostOfPerProduct >= 0 && numberOfDelivery >= 0 && numberOfProduct >= 0)
            {
                double totalCost = (CostOfPerDelivery * numberOfDelivery) + (CostOfPerProduct * numberOfProduct) + CostOfFix;
                return totalCost;
            }
            else
            {
                throw new Exception();
            }
        }


    }
}
