using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1.Components
{
    public partial class Service
    {
        public string StrDiscount
        {
            get
            {
                if (Discount == 0 || Discount == null)
                    return "";
                else
                    return $"* скидка {Discount} %";
            }
        }
        public string CostDuration
        {
            get
            {
                if (Discount == 0 || Discount == null)
                    return $"{Cost:F} рублей за  {DurationInSeconds / 60} минут";
                else
                    return $" {(double)Cost - (double)Cost * (double)Discount/100:F} рублей за {DurationInSeconds/60} минут";
            }
        }
        public Visibility DiscountVisability
        {
            get
            {
                if (Discount == 0 || Discount == null)
                    return Visibility.Collapsed;
                else
                    return Visibility.Visible;
            }

        }
        public string ColorDis
        {
            get
            {
                if (Discount == 0 || Discount == null)
                    return "#ffffff";
                else
                    return "#D1FFD1";
            }
        }

    }
}
