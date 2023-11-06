using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskApp.Converter
{
    public class ColorConverter : IValueConverter
    {
        public object Convert(object val, Type typeTarget, object param, CultureInfo cul) // converts colour into a usable rgb string values
        {
            var colour = val.ToString();
            return Color.FromArgb(colour);
        }

        public object ConvertBack(object val, Type typeTarget, object param, CultureInfo cul) // reverts previous function
        {
            throw new NotImplementedException();
        }
    }
}
