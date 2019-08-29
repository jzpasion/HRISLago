using Fasetto.Word.Core;
using System;
using System.Diagnostics;
using System.Globalization;

namespace Fasetto.Word
{
    /// <summary>
    /// Converts the <see cref="ApplicationPage"/> to an actual view/page
    /// </summary>
    public class ApplicationPageValueConverter : BaseValueConverter<ApplicationPageValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Find the appropriate page
            switch ((ApplicationPage)value)
            {
               

                

              
                case ApplicationPage.LagoLogin:
                    return new LagoLogin();

         

                case ApplicationPage.DashboardPage:
                    return new DashboardPage();

             

                default:
                    Debugger.Break();
                    return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
