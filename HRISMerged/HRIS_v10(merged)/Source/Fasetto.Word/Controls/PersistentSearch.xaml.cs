using MaterialDesignExtensions.Controls;
using System.Windows.Controls;

namespace Fasetto.Word
{
    /// <summary>
    /// Interaction logic for PersistentSearch.xaml
    /// </summary>
    public partial class PersistentSearch : UserControl
    {
        public PersistentSearch()
        {
            InitializeComponent();
        }

        private void SearchHandler1(object sender, SearchEventArgs args)
        {
            searchResultTextBlock1.Text = "Your are looking for '" + args.SearchTerm + "'.";
        }
    }
}
