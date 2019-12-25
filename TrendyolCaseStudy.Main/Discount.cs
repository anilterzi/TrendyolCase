using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrendyolCaseStudy.Main
{
    public class Discount
    {
        public Discount(int minProductAmount, double discountAmount,DiscountType type)
        {
            this.MinProductAmount = minProductAmount;
            this.DiscountAmount = discountAmount;
            this.Type = type;
        }

        public int MinProductAmount { get; set; }

        public double DiscountAmount { get; set; }

        public DiscountType Type { get; set;}

        public enum DiscountType
        {
            Rate,
            Amount
        }
    }
}
