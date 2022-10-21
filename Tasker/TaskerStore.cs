﻿using System;
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

        

        private static Grid? _mainGrid;
        public static Grid MainGrid
        {
            get { return _mainGrid; }
            set { _mainGrid = value; }
        }

        private static int _id = 1;

        public static int Id
        {
            get
            {
                int id = _id;
                _id++;
                return id;
            }
            
        }
    }
}
