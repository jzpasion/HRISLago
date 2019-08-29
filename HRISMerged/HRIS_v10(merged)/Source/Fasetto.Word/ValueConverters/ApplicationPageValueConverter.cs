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
                case ApplicationPage.Login:
                    return new LoginPage();

                case ApplicationPage.Register:
                    return new RegisterPage();

                case ApplicationPage.Chat:
                    return new ChatPage();

                case ApplicationPage.LagoLogin:
                    return new LagoLogin();

                case ApplicationPage.EmpoyeeManagement:
                    return new EmployeeManagement();

                case ApplicationPage.Home:
                    return new Home();

                case ApplicationPage.DashboardPage:
                    return new DashboardPage();

                case ApplicationPage.PersistentSearch:
                    return new PersistentSearchPage();

                case ApplicationPage.Payroll:
                    return new ParollPage();

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
