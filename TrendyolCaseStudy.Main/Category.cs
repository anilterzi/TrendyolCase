using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrendyolCaseStudy.Main
{
    public class Category
    {
        public Category(string title)
        {
            this.Title =title;
        }

        public Category(string title, Category parentCategory )
        {
            this.Title=title;
            this.ParentCategory= parentCategory;
        }

        public string Title { get; set; }

        public Category ParentCategory { get; set; }
    }
}
