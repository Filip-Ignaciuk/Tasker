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
        public static List<Task> tasks = new List<Task>();

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
            IsTasksValid(TextBoxInput.Text);
            


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



                foreach (var child in tempstackpanel.Children)
                {

                    if (child.GetType() == typeof(Border))
                    {
                        Border border = (Border)child;
                        Task task = border.Tag as Task;
                        if (TaskExistsInRemovealStackpanel(border.Name))
                        {
                            break;
                        }

                        bool doesContain = false;
                        string Title = string.Empty;
                        string Description = string.Empty;

                        Title = task.title;
                        Description = task.description; 

                        if (Title.Contains(TextBoxInput.Text))
                        {
                            doesContain = true;
                        }

                        if (Description.Contains(TextBoxInput.Text))
                        {
                            doesContain = true;
                        }

                        if (doesContain)
                        {
                            Task.DisplayTask(task);
                            tasks.Add(task);
                        }

                    }
                }
            }

            if (TextBoxInput.Text == "")
            {
                ContainsTextStackPanel.Children.Clear();
                tasks.Clear();


            }


        }

        public static void IsTasksValid(string _text)
        {
            List<Task> temptasks = tasks.ToList();
            if (!(tasks.Count == 0))
            {
                foreach (var task in temptasks)
                {
                    if (task.title.Contains(_text) == false && task.title.Contains(_text) == false)
                    {
                        tasks.Remove(task);
                    }
                }
            }

            
        }

        public static bool TaskExistsInRemovealStackpanel(string _id)
        {
            foreach(var task in tasks)
            {
                if (task.Id == _id)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool TaskExistsInRemovealStackpanel(Task _task)
        {
            foreach (var task in tasks)
            {
                if (task.Id == _task.Id)
                {
                    return true;
                }
            }
            return false;
        }

        public static void DeleteTasks(Task[] _tasks)
        {
            foreach(Task task in _tasks)
            {
                task.Delete();
            }
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
            Task[] toBeDeletedTasks = tasks.ToArray();
            DeleteTasks(toBeDeletedTasks);
        }

        private void Delete_Selected_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
