using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrendyolCaseStudy.Main
{
    public class Campaign:Discount
    {
        public Campaign(int minProductAmount,double discountAmount,DiscountType type, Category category):base(minProductAmount, discountAmount,type)
        {
            this.Category = category;
        }

        public Category Category { get; set; }

    }
}
