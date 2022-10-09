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

namespace Tasker
{
    // Allows other pages to share data
    public class TaskerStore
    {
        // Allows pages like Add, to add to the main pages stackpanel.
        private static StackPanel[]? _currentStackpanels;
        public static StackPanel[] CurrentStackpanels
        {
            get { return _currentStackpanels; }
            set { _currentStackpanels = value; }
        }
    }
}
