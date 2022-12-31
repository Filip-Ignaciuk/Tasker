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
using System.Runtime.Serialization;
using System.Xml;

namespace Tasker
{
    [Serializable()]
    public class Tasklet : ISerializable
    {
        public string? Id; 
        public string? title;
        public int? stackPanelNumber;
        public string? description;
        public StackPanel[]? stackPanels;
        public Border? border { get; private set; }

        // Constructors
        public Tasklet(string _title, int? _stackpanelnumber, string _description, ref StackPanel[] _stackpanels)
        {

            _stackpanels.ToString();

            title = _title;
            stackPanelNumber = _stackpanelnumber;
            description = _description;


            if (stackPanelNumber >= 0)
            {
                Id = "Id" + stackPanelNumber.ToString() + TaskerStore.Id.ToString();
            }
            if (stackPanelNumber == null)
            {
                Id = "Id" + stackPanelNumber.ToString();
            }

            stackPanels = _stackpanels;

        }

        public Tasklet(string _title, int? _stackpanelnumber, string _description, ref StackPanel[] _stackpanels, string _id)
        {



            title = _title;
            stackPanelNumber = _stackpanelnumber;
            description = _description;


            // Assigning ID
            Id = _id;

            stackPanels = _stackpanels;

        }

        


        public void Delete()
        {
            

            if (stackPanelNumber >= 0)
            {
                stackPanels[(int)stackPanelNumber].Children.Remove(this.border);
            }
            if (stackPanelNumber == null)
            {
                stackPanels[0].Children.Remove(this.border);
            }

            title = null;
            stackPanelNumber = null;
            description = null;
            stackPanels = null;
            Id = null;
            border = null;


        }


        public static void DisplayTask(Tasklet _task)
        {
            // Defining Border.
            Border border = new Border();
            border.Name = _task.Id;
            border.BorderThickness = new Thickness(2);
            border.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            border.CornerRadius = new CornerRadius(5);

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
            title.Name = "Title";
            title.Content = _task.title;
            title.FontSize = 12;
            title.HorizontalAlignment = HorizontalAlignment.Center;
            title.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetRow(title, 0);
            Grid.SetColumn(title, 0);



            // Defining description of task.
            Label description = new Label();
            description.Name = "Description";
            description.Content = _task.description;
            description.HorizontalAlignment = HorizontalAlignment.Center;
            description.VerticalAlignment = VerticalAlignment.Center;

            Grid.SetRow(description, 1);
            Grid.SetColumn(description, 0);


            grid.Children.Add(title);
            grid.Children.Add(description);
            border.Child = grid;
            border.Tag = _task;
            _task.border = border;



            if (_task.stackPanelNumber >= 0)
            {
                _task.stackPanels[(int)_task.stackPanelNumber].Children.Add(border);
            }
            if (_task.stackPanelNumber == null)
            {
                _task.stackPanels[0].Children.Add(border);
            }


            //switch (_task.level)
            //{
            //    case Level.Urgent:
            //        grid.Children.Add(title);
            //        grid.Children.Add(description);
            //        border.Child = grid;
            //        border.Tag = _task;
            //        _task.border = border;
            //        _task.stackPanels[0].Children.Add(border);
            //        break;
            //    case Level.Required:
            //
            //        grid.Children.Add(title);
            //        grid.Children.Add(description);
            //        border.Child = grid;
            //        border.Tag = _task;
            //        _task.border = border;
            //        _task.stackPanels[1].Children.Add(border);
            //        break;
            //    case Level.Optional:
            //        grid.Children.Add(title);
            //        grid.Children.Add(description);
            //        border.Child = grid;
            //        border.Tag = _task;
            //        _task.border = border;
            //        _task.stackPanels[2].Children.Add(border);
            //        break;
            //    case Level.Other:
            //        grid.Children.Add(title);
            //        grid.Children.Add(description);
            //        border.Child = grid;
            //        border.Tag = _task;
            //        _task.border = border;
            //        _task.stackPanels[3].Children.Add(border);
            //        break;
            //    case Level.RemovingList:
            //        grid.Children.Add(title);
            //        grid.Children.Add(description);
            //        border.Child = grid;
            //        border.Tag = _task;
            //        _task.border = border;
            //
            //
            //
            //
            //
            //
            //
            //
            //        _task.stackPanels[0].Children.Add(border);
            //        break;
            //
            //}
            //

        }

        public Tasklet(SerializationInfo info, StreamingContext context)
        {
            Id = (string)info.GetValue("Id", typeof(string));
            title = (string)info.GetValue("Title", typeof(string));
            stackPanelNumber = (int)info.GetValue("StackPanelNumber", typeof(int));
            description = (string)info.GetValue("Description", typeof(string));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Id", Id);
            info.AddValue("Title", title);
            info.AddValue("StackPanelNumber", stackPanelNumber);
            info.AddValue("Description", description);
        }
    }

        
}
