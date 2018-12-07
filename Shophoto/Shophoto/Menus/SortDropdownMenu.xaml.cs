using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Shophoto.Menus
{
    /// <summary>
    /// Interaction logic for DropdownMenu.xaml
    /// </summary>
    public partial class SortDropdownMenu : UserControl
    {
        public SortDropdownMenu()
        {
            InitializeComponent();
            this.LostFocus += SortDropdownMenu_LostFocus;
        }

        public SortDropdownMenuVM VM { get { return DataContext as SortDropdownMenuVM; } }

        private void SortDropdownMenu_LostFocus(object sender, RoutedEventArgs e)
        {
            if (VM != null)
            {
                VM.IsDropdownOpen = false;
            }
        }
    }
}
