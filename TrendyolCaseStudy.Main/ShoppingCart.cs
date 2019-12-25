using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrendyolCaseStudy.Main
{
    public class ShoppingCart : IShoppingCart
    {

        public Dictionary<Product, int> QuantityOfProduct { get; set; }
        public Coupon Coupon { get; set; }
        public List<Campaign> Campaigns { get; set; }
        private IDeliveryCostCalculator DeliveryCostCalculator { get; set; }
        public ShoppingCart(IDeliveryCostCalculator deliveryCostCalculator)
        {
            QuantityOfProduct = new Dictionary<Product, int>();
            Campaigns = new List<Campaign>();
            DeliveryCostCalculator = deliveryCostCalculator;
        }

        public void addItem(Product product, int quantity)
        {
            if (product != null && quantity > 0)
            {
                if (QuantityOfProduct != null)
                {
                    if (QuantityOfProduct.ContainsKey(product))
                    {
                        QuantityOfProduct[product] += quantity;
                    }
                    else
                    {
                        QuantityOfProduct.Add(product, quantity);
                    }
                }
                else
                {
                    QuantityOfProduct.Add(product, quantity);
                }
            }
            else
            {
                throw new Exception();
            }
        }
        public void applyDiscount(params Campaign[] campaigns)
        {
            Campaigns.AddRange(campaigns);
        }
        public void applyCoupon(Coupon coupon)
        {
            Coupon = coupon;
        }
        public double applyCoupon(double totalAmount)
        {
            double discountAmount = 0;

            if (Coupon != null && totalAmount >= Coupon.MinProductAmount)
            {
                if (Coupon.Type.ToString() == "Rate")
                {
                    discountAmount = totalAmount * Coupon.DiscountAmount / 100;
                }
                else if (Coupon.Type.ToString() == "Amount")
                {
                    discountAmount = Coupon.DiscountAmount;
                }
            }

            return discountAmount;
        }
        public double applyCampaign(double totalAmount)
        {
            double discountAmount = 0;
            foreach (Campaign campaign in Campaigns)
            {
                Dictionary<Product, int> product =
                QuantityOfProduct.Where(a => a.Key.Category.Title == campaign.Category.Title).ToDictionary(a => a.Key, a => a.Value);
                if (product.Values.Sum() >= campaign.MinProductAmount)
                {
                    if (campaign.Type.ToString() == "Rate")
                    {
                        if (product.Values.Sum() >= 3 && product.Values.Sum() <= 5)
                        {
                            campaign.DiscountAmount = 20.0;
                        }
                        else if (product.Values.Sum() > 5)
                        {
                            campaign.DiscountAmount = 50.0; 
                        }
                        double currentDiscountAmount = totalAmount * (campaign.DiscountAmount / 100);
                        if (currentDiscountAmount > discountAmount)
                        {
                            discountAmount = currentDiscountAmount;
                        }
                        continue;
                    }
                    else if (campaign.Type.ToString() == "Amount")
                    {
                        if (campaign.DiscountAmount > discountAmount)
                        {
                            if (product.Values.Sum() >= 5)
                            {
                                campaign.DiscountAmount += 5;
                            }
                            discountAmount = campaign.DiscountAmount;
                        }
                        continue;
                    }
                }
            }
            return discountAmount;
        }

        public double getCampaignDiscount()
        {
            double totalAmount = QuantityOfProduct.Sum(a => a.Key.Price * a.Value);
            return applyCampaign(totalAmount);
        }
        public double getCouponDiscount()
        {
            double totalAmount = QuantityOfProduct.Sum(a => a.Key.Price * a.Value);
            return applyCoupon(totalAmount);
        }

        public double getTotalAmountAfterDiscounts()
        {
            double totalAmount = QuantityOfProduct.Sum(a => a.Key.Price * a.Value);

            totalAmount = totalAmount - applyCampaign(totalAmount);
            totalAmount = totalAmount - applyCoupon(totalAmount);
            if(totalAmount < 0)
            {
                return 0;
            }
            return totalAmount;
        }

        public double getDeliveryCost()
        {
            return DeliveryCostCalculator.CalculateFor(this);
        }

        public int getNumberOfDeliveries()
        {
            if (QuantityOfProduct == null)
            {
                return 0;
            }
            return QuantityOfProduct.GroupBy(a => a.Key.Category.Title).Count();
        }

        public int getNumberOfProducts()
        {
            if (QuantityOfProduct == null)
            {
                return 0;
            }
            return QuantityOfProduct.Count();
        }

        public void print()
        {
            var products = QuantityOfProduct.GroupBy(a => a.Key.Category.Title).ToDictionary(s => s.Key, s => s.ToList());
            List<Result> resultList = new List<Result>();
            foreach (var product in products)
            {
                foreach (var prod in product.Value)
                {
                    Result result = new Result();
                    result.categoryName = product.Key;
                    result.productName = prod.Key.Title;
                    result.quantity = prod.Value;
                    result.unitPrice = prod.Key.Price;
                    result.totalPrice = prod.Value * prod.Key.Price;
                    resultList.Add(result);
                }
            }
            foreach (Result resultPrint in resultList)
            {
                Console.WriteLine("Category Name = " + resultPrint.categoryName + "  " +
                                  "Product Name = " + resultPrint.productName + "  " +
                                  "Quantity = " + resultPrint.quantity + "  " +
                                  "Unit Price = " + resultPrint.unitPrice + "  " +
                                  "Total Price = " + resultPrint.totalPrice
                                  );
            }
            double getTotalAmountAfterDiscount = getTotalAmountAfterDiscounts();
            double getDeliveryCosts = getDeliveryCost();
            Console.WriteLine("Total Amount = " + QuantityOfProduct.Sum(a => a.Key.Price * a.Value) + "  " +
                              "Total Amount After Discounts = " + getTotalAmountAfterDiscount + "  " +
                              "Total Discount = " + (QuantityOfProduct.Sum(a => a.Key.Price * a.Value) - getTotalAmountAfterDiscount) + "  " +
                              "Delivery Cost = " + getDeliveryCosts);
        }

    }
}
