using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrendyolCaseStudy.Main
{
    public interface IShoppingCart
    {
        void addItem(Product product, int amount);
        void applyDiscount(params Campaign[] campaigns);
        double applyCampaign(double totalAmount);
        void applyCoupon(Coupon coupon);
        double applyCoupon(double totalAmount);
        double getCouponDiscount();
        double getCampaignDiscount();
        double getDeliveryCost();
        int getNumberOfDeliveries();
        int getNumberOfProducts();
        double getTotalAmountAfterDiscounts();
        void print();
    }
}
