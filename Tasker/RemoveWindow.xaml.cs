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
        public static Dictionary<Tasklet, bool> currentValidTasks = new Dictionary<Tasklet, bool>();
        public static bool currentValidTasksIsUsed = false;

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

                            currentValidTasks.Add(task, false);

                            StackPanel[] stackpanelremoval = new StackPanel[1];
                            stackpanelremoval[0] = ContainsTextStackPanel;
                            Tasklet task1 = new Tasklet(task.title, Level.RemovingList, task.description, ref stackpanelremoval, task.Id);
                            ContainsTextStackPanel.Children.Add(MakeSelectButton(task));
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

        public static void WaitForCurrentValidTasks()
        {
            while (currentValidTasksIsUsed)
            {
                System.Threading.Thread.Sleep(500);
            }

        }

        public static void IsTasksValid(string _text, StackPanel _stackpanel)
        {
            WaitForCurrentValidTasks();
            currentValidTasksIsUsed = true;
            List<Tasklet> temptasks = currentValidTasks.Keys.ToList();
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

            currentValidTasksIsUsed = false;

        }

        public static bool TaskExistsInRemovealStackpanel(string _id)
        {
            WaitForCurrentValidTasks();
            currentValidTasksIsUsed = true;
            foreach (var task in currentValidTasks.Keys)
            {
                if (task.Id == _id)
                {
                    currentValidTasksIsUsed = false;
                    return true;
                }
            }
            currentValidTasksIsUsed = false;
            return false;
        }
        public static bool TaskExistsInRemovealStackpanel(Tasklet _task)
        {
            WaitForCurrentValidTasks();
            currentValidTasksIsUsed = true;
            foreach (var task in currentValidTasks.Keys)
            {
                if (task.Id == _task.Id)
                {
                    currentValidTasksIsUsed = false;
                    return true;
                }
            }
            currentValidTasksIsUsed = false;
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



        public static CheckBox MakeSelectButton(Tasklet _task)
        {

            CheckBox checkbox = new CheckBox();
            checkbox.Name = _task.Id + "select";
            checkbox.Tag = _task;
            checkbox.Checked += Select_Check;
            checkbox.Unchecked += Select_Uncheck;

            return checkbox;
        }

        private static void Select_Check(object sender, RoutedEventArgs e)
        {
            WaitForCurrentValidTasks();
            currentValidTasksIsUsed = true;
            CheckBox checkbox = sender as CheckBox;

            if (checkbox.Tag.GetType() == typeof(Tasklet))
            {
                for (int i = 0; i < currentValidTasks.Count; i++)
                {
                    var currentKey = currentValidTasks.ElementAt(i).Key;
                    if (currentKey == checkbox.Tag)
                    {

                        currentValidTasks.Remove(currentKey);
                        currentValidTasks.Add(currentKey, true);
                        
                    }
                }
            }
            currentValidTasksIsUsed = false;

        }

        private static void Select_Uncheck(object sender, RoutedEventArgs e)
        {
            WaitForCurrentValidTasks();
            currentValidTasksIsUsed = true;
            CheckBox checkbox = sender as CheckBox;

            if (checkbox.Tag.GetType() == typeof(Tasklet))
            {
                for (int i = 0; i < currentValidTasks.Count; i++)
                {
                    var currentKey = currentValidTasks.ElementAt(i).Key;
                    if (currentKey == checkbox.Tag)
                    {

                        currentValidTasks.Remove(currentKey);
                        currentValidTasks.Add(currentKey, false);
                    }
                }
            }
            currentValidTasksIsUsed = false;
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
            WaitForCurrentValidTasks();
            currentValidTasksIsUsed = true;
            Tasklet[] toBeDeletedTasks = currentValidTasks.Keys.ToArray();
            DeleteTasks(toBeDeletedTasks);
            ContainsTextStackPanel.Children.Clear();
            currentValidTasks.Clear();
            currentValidTasksIsUsed = false;
        }

        private void Delete_Selected_Click(object sender, RoutedEventArgs e)
        {
            WaitForCurrentValidTasks();
            currentValidTasksIsUsed = true;
            
            List<Tasklet> uncheackedtasks = new List<Tasklet>();
            for (int i = 0; i < currentValidTasks.Count; i++)
            {
                var kvp = currentValidTasks.ElementAt(i);
                if (kvp.Value == true)
                {

                    for (int j = 0; i < ContainsTextStackPanel.Children.Count; i++)
                    {
                        bool checkboxcheck = false;
                        if (ContainsTextStackPanel.Children[j].GetType() == typeof(Border))
                        {
                            checkboxcheck = true;
                            Border border = (Border)ContainsTextStackPanel.Children[j];
                            Tasklet task = (Tasklet)border.Tag;

                            if (task.Id != null && kvp.Key.Id != null && task.Id.ToString() == kvp.Key.Id.ToString())
                            {
                                kvp.Key.Delete();
                                task.Delete();
                            }
                        }
                        if (ContainsTextStackPanel.Children[j].GetType() == typeof(CheckBox) && checkboxcheck)
                        {
                            ContainsTextStackPanel.Children.Remove(ContainsTextStackPanel.Children[j]);
                            
                            checkboxcheck = false;  
                        }
                    }

                }
                
            }
            currentValidTasksIsUsed = false;
        }


        public bool IsClosed { get; private set; }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            currentValidTasks.Clear();
            ContainsTextStackPanel.Children.Clear();


            IsClosed = true;
        }
    }
}
