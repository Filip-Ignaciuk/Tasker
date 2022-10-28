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

namespace Tasker
{
    /// <summary>
    /// Interaction logic for RemoveWindow.xaml
    /// </summary>
    public partial class RemoveWindow : Window
    {
        public static List<Tasklet> currentValidTasks = new List<Tasklet>();
        public static List<Button> currentValidButtons = new List<Button>();

        public bool isUrgent = true;
        public bool isRequired = true;
        public bool isOptional = true;
        public bool isOther = true;
        public RemoveWindow()
        {
            InitializeComponent();
        }

        private void TextBoxTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsTasksValid(TextBoxInput.Text, ContainsTextStackPanel);
            


            foreach (var stackpanel in TaskerStore.CurrentStackpanels)
            {
                StackPanel tempstackpanel = stackpanel;
                
                if (tempstackpanel == null)
                {
                    break;
                    
                }

                if (tempstackpanel.Name == "Urgent" && !isUrgent)
                {
                    break;
                }
                else if (tempstackpanel.Name == "Required" && !isRequired)
                {
                    break;
                }
                else if (tempstackpanel.Name == "Optional" && !isOptional)
                {
                    break;
                }
                else if (tempstackpanel.Name == "Other" && !isOther)
                {
                    break;
                }

                for (int i = 0; i < tempstackpanel.Children.Count; i++)
                {
                    var currentChild = tempstackpanel.Children[i];
                    if (currentChild.GetType() == typeof(Border))
                    {
                        Border border = (Border)currentChild;
                        Tasklet task = border.Tag as Tasklet;
                        if (TaskExistsInRemovealStackpanel(border.Name))
                        {
                            break;
                        }

                        bool doesContain = false;
                        string Title = string.Empty;
                        string Description = string.Empty;

                        Title = task.title;
                        Description = task.description;

                        if (Title != null && Description != null)
                        {
                            if (Title.Contains(TextBoxInput.Text))
                            {
                                doesContain = true;
                            }

                            if (Description.Contains(TextBoxInput.Text))
                            {
                                doesContain = true;
                            }
                        }
                        else
                        {
                            doesContain = false;
                        }

                        if (doesContain && !TaskExistsInRemovealStackpanel(task))
                        {
                            
                            currentValidTasks.Add(task);
                            StackPanel[] stackpanelremoval = new StackPanel[1];
                            stackpanelremoval[0] = ContainsTextStackPanel;
                            Tasklet task1 = new Tasklet(task.title, Level.RemovingList, task.description, ref stackpanelremoval);

                        }

                    }
                }
            }

            if (TextBoxInput.Text == "")
            {
                ContainsTextStackPanel.Children.Clear();
                currentValidTasks.Clear();
            }


        }

        public static void IsTasksValid(string _text, StackPanel _stackpanel)
        {
            List<Tasklet> temptasks = currentValidTasks.ToList();
            if (currentValidTasks.Count != 0)
            {
                foreach (var task in temptasks)
                {
                    if (task.title.Contains(_text) == false && task.description.Contains(_text) == false)
                    {
                        
                        RemoveTaskFromStackPanel(task, _stackpanel);
                        currentValidTasks.Remove(task);
                    }
                }
            }

            
        }

        public static bool TaskExistsInRemovealStackpanel(string _id)
        {
            foreach(var task in currentValidTasks)
            {
                if (task.Id == _id)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool TaskExistsInRemovealStackpanel(Tasklet _task)
        {
            foreach (var task in currentValidTasks)
            {
                if (task.Id == _task.Id)
                {
                    return true;
                }
            }
            return false;
        }

        public static void DeleteTasks(Tasklet[] _tasks)
        {
            foreach(Tasklet task in _tasks)
            {
                task.Delete();
            }
        }

        public static void RemoveTasksFromStackPanel(Tasklet[] _tasks, StackPanel _stackpanel)
        {
            foreach (Tasklet task in _tasks)
            {
                _stackpanel.Children.Remove(task.border);
            }
        }

        public static void RemoveTaskFromStackPanel(Tasklet _task, StackPanel _stackpanel)
        {
            _stackpanel.Children.Remove(_task.border);
        }

        public static void MakeSelectButton(Tasklet _task)
        {

            CheckBox checkbox = new CheckBox();
            checkbox.Name = _task.Id + "select";


        }

        

        private void CheckBox1_Checked(object sender, RoutedEventArgs e)
        {
            isUrgent = true;
        }

        private void CheckBox0_Checked(object sender, RoutedEventArgs e)
        {
            isRequired = true;
        }

        private void CheckBox2_Checked(object sender, RoutedEventArgs e)
        {
            isOptional = true;
        }

        private void CheckBox3_Checked(object sender, RoutedEventArgs e)
        {
            isOther = true;
        }

        private void CheckBox0_Unchecked(object sender, RoutedEventArgs e)
        {
            isUrgent = false;
        }

        private void CheckBox1_Unchecked(object sender, RoutedEventArgs e)
        {
            isRequired = false;
        }

        private void CheckBox2_Unchecked(object sender, RoutedEventArgs e)
        {
            isOptional = false;
        }

        private void CheckBox3_Unchecked(object sender, RoutedEventArgs e)
        {
            isOther = false;
        }

        private void Delete_All_Click(object sender, RoutedEventArgs e)
        {
            Tasklet[] toBeDeletedTasks = currentValidTasks.ToArray();
            DeleteTasks(toBeDeletedTasks);
            ContainsTextStackPanel.Children.Clear();
            currentValidTasks.Clear();
        }

        private void Delete_Selected_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
