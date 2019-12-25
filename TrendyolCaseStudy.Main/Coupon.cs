using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrendyolCaseStudy.Main
{
    public class Coupon :Discount
    {
        public Coupon(int minProductAmount, double discountAmount,DiscountType type):base(minProductAmount,discountAmount,type)
        {
        }
    }
}
