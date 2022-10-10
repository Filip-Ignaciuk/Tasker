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

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public TaskerStore? TaskerStore;
        public MainWindow()
        {
            InitializeComponent();


            //Definitions of Stackpanels
            StackPanel[] stackPanelsLive = new StackPanel[4];
            stackPanelsLive[0] = Urgent;
            stackPanelsLive[1] = Required;
            stackPanelsLive[2] = Optional;
            stackPanelsLive[3] = Other;
            TaskerStore.CurrentStackpanels = stackPanelsLive;


            TaskerStore.MainGrid = MainGrid;


        }
        
        
        

        private void About_me(object sender, RoutedEventArgs e)
        {

        }

        private void Add(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow();
            addWindow.Show();
            
        }

        private void Remove(object sender, RoutedEventArgs e)
        {
            RemoveWindow removeWindow = new RemoveWindow();
            removeWindow.Show();
        }

        private void Settings(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.Show();
        }
    }

    public enum Level { Urgent, Required, Optional, Other, RemovingList }
    public class Task
    {
        public string title;
        public Level level;
        public string description;
        public StackPanel[] stackPanels;


        public Task(string _title, Level _level, string _description, ref StackPanel[] _stackpanels)
        {
            

            
            title = _title;
            level = _level;
            description = _description;
            stackPanels = _stackpanels;
            DisplayTask(this);
            _stackpanels = stackPanels;
            
        }

        


        public static void DisplayTask(Task _task)
        {
            // Defining Border.
            Border border = new Border();
            border.BorderThickness = new Thickness(2);
            border.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            border.CornerRadius = new CornerRadius(2);
             
            // Defining grid.
            Grid grid = new Grid();
            grid.HorizontalAlignment = HorizontalAlignment.Center;
            grid.VerticalAlignment = VerticalAlignment.Stretch;

            // Defining columns.
            ColumnDefinition columnDefinition = new ColumnDefinition();
            columnDefinition.Width = new GridLength(2, GridUnitType.Auto);
            grid.ColumnDefinitions.Add(columnDefinition);
            columnDefinition = new ColumnDefinition();
            columnDefinition.Width = new GridLength(1, GridUnitType.Auto);
            grid.ColumnDefinitions.Add(columnDefinition);

            // Defining rows.
            RowDefinition rowDefinition = new RowDefinition();
            rowDefinition.Height = new GridLength(1, GridUnitType.Auto);
            grid.RowDefinitions.Add(rowDefinition);
            rowDefinition = new RowDefinition();
            rowDefinition.Height = new GridLength(2, GridUnitType.Auto);
            grid.RowDefinitions.Add(rowDefinition);

            // Defining title of task.
            Label title = new Label();
            title.Content = _task.title;
            title.FontSize = 12;
            title.HorizontalAlignment = HorizontalAlignment.Center;
            title.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetRow(title, 0);
            Grid.SetColumn(title, 0);

            

            // Defining description of task.
            Label description = new Label();
            description.Content = _task.description;
            description.HorizontalAlignment = HorizontalAlignment.Center;
            description.VerticalAlignment = VerticalAlignment.Center;
            
            Grid.SetRow(description, 1);
            Grid.SetColumn(description, 0);
            

            switch (_task.level)
            {
                case Level.Urgent:

                    title.Background = new SolidColorBrush(Color.FromRgb(222, 67, 92));


                    description.Background = new SolidColorBrush(Color.FromRgb(222, 67, 92));

                    grid.Children.Add(title);
                    grid.Children.Add(description);
                    border.Child = grid;

                    _task.stackPanels[0].Children.Add(border);
                    break;
                case Level.Required:

                    grid.Children.Add(title);
                    grid.Children.Add(description);
                    border.Child = grid;
                    _task.stackPanels[1].Children.Add(border);
                    break;
                case Level.Optional:
                    grid.Children.Add(title);
                    grid.Children.Add(description);
                    border.Child = grid;
                    _task.stackPanels[2].Children.Add(border);
                    break;
                case Level.Other:
                    grid.Children.Add(title);
                    grid.Children.Add(description);
                    border.Child = grid;
                    _task.stackPanels[3].Children.Add(border);
                    break;
                case Level.RemovingList:
                    grid.Children.Add(title);
                    grid.Children.Add(description);
                    border.Child = grid;
                    _task.stackPanels[0].Children.Add(border);
                    break;

            }

            
        }





    }
}
