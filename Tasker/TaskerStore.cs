using System;
using System.IO;
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
        private static List<UIElement> _activeElements = new List<UIElement>();

        public static List<UIElement> ActiveElements { get { return _activeElements; } set { _activeElements = value; } }


        public readonly static string DocumentDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        private static List<Tasklet> _currentTasks = new List<Tasklet>();

        public static List<Tasklet> CurrentTasks
        {
            get
            {
                return _currentTasks;
            }
            set
            {
                _currentTasks = value;
                for (int i = 0; i < Profile.stackpanelcount; i++)
                {
                    CurrentStackpanels[i].Children.Clear();
                }
                foreach (var tasklet in _currentTasks)
                {
                    tasklet.stackPanels = CurrentStackpanels;
                    Tasklet.DisplayTask(tasklet);
                }
            }
        }

        // Allows pages like Add, to add to the main pages stackpanel.
        private static StackPanel[]? _currentStackpanels;
        public static StackPanel[] CurrentStackpanels
        {
            get { return _currentStackpanels; }
            set { _currentStackpanels = value; }
        }

        public static void SetProfile()
        {
            string dir = File.ReadAllText(DocumentDir + @"\Tasker\lp.txt");
            Profile profile = TaskerConfigurator.LoadProfile(dir);
            Profile = profile;

        }

        public static List<string>  LabelName = new List<string>();

        private static Profile _profile;

        public static Profile Profile
        {

            get { return _profile; }
            set 
            {
                



                _profile = value;
                CurrentStackpanels = _profile.stackPanels.ToArray();
                StackPanelsContainerGrid.Children.Clear();
                StackPanelsContainerGrid.ColumnDefinitions.Clear();
                StackPanelsContainerGrid.RowDefinitions.Clear();
                for (int i = 0; i < value.stackpanelcount; i++)
                {
                    // Defining columns.
                    ColumnDefinition columnDefinition = new ColumnDefinition();
                    columnDefinition.Width = new GridLength(2, GridUnitType.Star);
                    StackPanelsContainerGrid.ColumnDefinitions.Add(columnDefinition);

                    
                }

                // Defining rows.
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = new GridLength(40, GridUnitType.Pixel);
                StackPanelsContainerGrid.RowDefinitions.Add(rowDefinition);
                rowDefinition = new RowDefinition();
                rowDefinition.Height = new GridLength(2, GridUnitType.Star);
                StackPanelsContainerGrid.RowDefinitions.Add(rowDefinition);

                LabelName.Clear();

                foreach (var label in _profile.labels)
                {
                    StackPanelsContainerGrid.Children.Add(label);
                    LabelName.Add(label.Content.ToString());

                }

                foreach (var scrollViewer in _profile.scrollViewers)
                {
                    StackPanelsContainerGrid.Children.Add(scrollViewer);
                }



            }
        }


        private static Grid _stackPanelsContainerGrid;
        public static Grid StackPanelsContainerGrid
        {
            get { return _stackPanelsContainerGrid; }
            set { _stackPanelsContainerGrid = value; }
        }


        private static Grid? _mainGrid;
        public static Grid MainGrid
        {
            get { return _mainGrid; }
            set { _mainGrid = value; }
        }
        private static int num = 0;
        public static void SetIdNumber()
        {
            string text = File.ReadAllText(DocumentDir + @"\Tasker\IdNumber.txt");
            num = Convert.ToInt32(text);
            _id = num;
        }

        private static int _id = 0;

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
