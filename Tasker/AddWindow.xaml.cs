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
using System.Windows.Shapes;
using System.Windows.Navigation;

namespace Tasker
{
    /// <summary>
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        private int radioButNum = TaskerStore.CurrentStackpanels.Count();
        private static List<RadioButton> currentRadioButtons = new List<RadioButton>();
        public AddWindow()
        {
            InitializeComponent();
            CreateRadialButtons(RadialButGrid);
        }

        private void Create(object sender, RoutedEventArgs e)
        {
            var stackpanels = TaskerStore.CurrentStackpanels;
            for (int i = 0; i < currentRadioButtons.Count; i++)
            {
                if (currentRadioButtons[i].IsChecked == true)
                {
                    Tasklet task = new Tasklet(TextBoxTitle.Text, i, TextBoxDescription.Text, ref stackpanels);
                    Tasklet.DisplayTask(task);
                    TaskerStore.CurrentTasks.Add(task);
                    break;
                }
            }
            TaskerStore.CurrentStackpanels = stackpanels;
        }
        int _columnNum = 0;
        int ColumNum
        {
            get
            {
                int num = _columnNum;
                _columnNum++;
                
                if (_columnNum > 1)
                {
                    _columnNum = 0;
                }
                return num;
                
            }
            set
            {
                _columnNum = value;
            }
        }

        private void CreateRadialButtons(Grid _grid)
        {
            int total = radioButNum;
            int row = 0;
            if (total % 2 == 0)
            {
                row = total / 2;
            }
            else if ((total - 1) % 2 == 0)
            {
                row = ((total - 1) / 2) + 1;
            }

            ColumnDefinition columnDefinition = new ColumnDefinition();
            columnDefinition.Width = new GridLength(2, GridUnitType.Star);
            RadialButGrid.ColumnDefinitions.Add(columnDefinition);
            columnDefinition = new ColumnDefinition();
            columnDefinition.Width = new GridLength(2, GridUnitType.Star);
            RadialButGrid.ColumnDefinitions.Add(columnDefinition);

            for (int i = 0; i < row; i++)
            {
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = new GridLength(2, GridUnitType.Star);
                RadialButGrid.RowDefinitions.Add(rowDefinition);
            }

            int num = 1;
            row = 0;
            int contentNum = 0;

            while (radioButNum != 0)
            {
                


                string name = "radiobut" + radioButNum.ToString();
                radioButNum--;

                Label label = new Label();
                label.Content = TaskerStore.LabelName[contentNum];
                contentNum++;

                RadioButton radioButton = new RadioButton();
                radioButton.Checked += rb_Checked;
                radioButton.Unchecked += rb_UnChecked;
                radioButton.Name = name;
                radioButton.Content = label;
     

                Grid.SetColumn(radioButton, ColumNum);
                currentRadioButtons.Add(radioButton);

                if (num == 2)
                {
                    num = 1;

                    Grid.SetRow(radioButton, row);
                    row++;
                }
                else
                {
                    Grid.SetRow(radioButton, row);
                    num++;
                }
                RadialButGrid.Children.Add(radioButton);
            }

            



        }

        private void rb_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;

            currentRadioButtons[currentRadioButtons.IndexOf(radioButton)].IsChecked = true;


        }

        private void rb_UnChecked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;

            currentRadioButtons[currentRadioButtons.IndexOf(radioButton)].IsChecked = false;


        }

        protected override void OnClosed(EventArgs e)
        {
            currentRadioButtons.Clear();

            base.OnClosed(e);

            


        }
    }
}
