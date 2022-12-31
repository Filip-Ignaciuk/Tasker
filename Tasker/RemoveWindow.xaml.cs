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
        static bool IsCaseSensitive = false;
        static bool IsEmpty = false;
        static StackPanel[] containsTextStackPanel = new StackPanel[1];


        static List<Tasklet> validTasks = new List<Tasklet>();
        static List<Border> borders = new List<Border>();
        static List<int> stackPanelNumber = new List<int>();
        static List<bool> IsChecked = new List<bool>();
        public RemoveWindow()
        {
            InitializeComponent();
            containsTextStackPanel[0] = ContainsTextStackPanel;
        }

        private void TextBoxInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsEmpty = false;
            string text = TextBoxInput.Text;
            CheckIfTasksAreValid(text, ContainsTextStackPanel);
            if (text == String.Empty)
            {
                containsTextStackPanel[0].Children.Clear();
                validTasks.Clear();
                borders.Clear();
                stackPanelNumber.Clear();
                IsChecked.Clear();
                IsEmpty = true;
            }

            if (!IsEmpty)
            {
                for (int i = 0; i < TaskerStore.CurrentTasks.Count; i++)
                {
                    if (IsCaseSensitive)
                    {
                        if ((TaskerStore.CurrentTasks[i].title.Contains(text) || TaskerStore.CurrentTasks[i].description.Contains(text)) && !IsAlreadyIn(TaskerStore.CurrentTasks[i]))
                        {
                            validTasks.Add(TaskerStore.CurrentTasks[i]);
                            stackPanelNumber.Add((int)TaskerStore.CurrentTasks[i].stackPanelNumber);

                            TaskerStore.CurrentTasks[i].stackPanelNumber = 0;

                            TaskerStore.CurrentTasks[i].stackPanels = containsTextStackPanel;


                            IsChecked.Add(false);

                            Tasklet.DisplayTask(TaskerStore.CurrentTasks[i]);
                            borders.Add(TaskerStore.CurrentTasks[i].border);

                            containsTextStackPanel[0].Children.Add(MakeSelectButton(TaskerStore.CurrentTasks[i]));
                        }
                    }
                    else if (!IsCaseSensitive)
                    {
                        if ((TaskerStore.CurrentTasks[i].title.ToLower().Contains(text.ToLower()) || TaskerStore.CurrentTasks[i].description.ToLower().Contains(text.ToLower())) && !IsAlreadyIn(TaskerStore.CurrentTasks[i]))
                        {
                            validTasks.Add(TaskerStore.CurrentTasks[i]);
                            stackPanelNumber.Add((int)TaskerStore.CurrentTasks[i].stackPanelNumber);

                            TaskerStore.CurrentTasks[i].stackPanelNumber = 0;

                            TaskerStore.CurrentTasks[i].stackPanels = containsTextStackPanel;

                            IsChecked.Add(false);

                            Tasklet.DisplayTask(TaskerStore.CurrentTasks[i]);
                            borders.Add(TaskerStore.CurrentTasks[i].border);

                            containsTextStackPanel[0].Children.Add(MakeSelectButton(TaskerStore.CurrentTasks[i]));


                                
                        }
                    }
                }
            }


        }
        private static void CheckStackPanel()
        {
            for (int i = 0; i < containsTextStackPanel[0].Children.Count; i++)
            {
                for (int j = 0; j < containsTextStackPanel[0].Children.Count; j++)
                {
                    if(containsTextStackPanel[0].Children[i].GetType() == typeof(Border) && containsTextStackPanel[0].Children[j].GetType() == typeof(Border))
                    {
                        Border border = (Border)containsTextStackPanel[0].Children[i];
                        Border border1 = (Border)containsTextStackPanel[0].Children[j];
                        Tasklet tasklet = (Tasklet)border.Tag;
                        Tasklet tasklet1 = (Tasklet)border1.Tag;

                        if (border != border1 && tasklet.Id == tasklet1.Id)
                        {
                            containsTextStackPanel[0].Children.Remove(border1);
                        }
                    }
                }
            }
            
        }

        private static bool IsAlreadyIn(Tasklet _tasklet)
        {
            for (int i = 0; i < validTasks.Count; i++)
            {
                if(_tasklet.Id == validTasks[i].Id)
                {
                    return true;
                }
            }
            return false;
        }

        private static void CheckIfTasksAreValid(string _text, StackPanel _stackpanel)
        {
            for (int i = 0; i < validTasks.Count; i++)
            {
                if (IsCaseSensitive)
                {
                    if (!(validTasks[i].title.Contains(_text)) && !(validTasks[i].description.Contains(_text)))
                    {
                        
                        Border tasklet = borders[i];
                        containsTextStackPanel[i].Children.RemoveAt(containsTextStackPanel[i].Children.IndexOf(tasklet) + 1);
                        _stackpanel.Children.Remove(tasklet);

                        validTasks[i].stackPanels = TaskerStore.CurrentStackpanels;
                        validTasks[i].stackPanelNumber = stackPanelNumber[i];

                        validTasks.RemoveAt(i);
                        borders.RemoveAt(i);
                        stackPanelNumber.RemoveAt(i);
                        IsChecked.RemoveAt(i);
                        


                        i = 0;
                    }
                }
                else if (!IsCaseSensitive)
                {
                    if (!(validTasks[i].title.ToLower().Contains(_text.ToLower())) && !(validTasks[i].description.ToLower().Contains(_text.ToLower())))
                    {
                        Border tasklet = borders[i];
                        containsTextStackPanel[i].Children.RemoveAt(containsTextStackPanel[i].Children.IndexOf(tasklet) + 1);
                        _stackpanel.Children.Remove(tasklet);

                        validTasks[i].stackPanels = TaskerStore.CurrentStackpanels;
                        validTasks[i].stackPanelNumber = stackPanelNumber[i];

                        validTasks.RemoveAt(i);
                        borders.RemoveAt(i);
                        stackPanelNumber.RemoveAt(i);
                        IsChecked.RemoveAt(i);



                        i = 0;
                    }
                }
            }
        }


        private void CaseSensitiveCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            IsCaseSensitive = true;
        }

        private void CaseSensitiveCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            IsCaseSensitive = false;
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
            CheckBox checkbox = sender as CheckBox;

            if (checkbox.Tag.GetType() == typeof(Tasklet))
            {
                for (int i = 0; i < validTasks.Count; i++)
                {
                    if (validTasks[i] == checkbox.Tag)
                    {
                        IsChecked[i] = true;
                    }
                }
            }

        }

        private static void Select_Uncheck(object sender, RoutedEventArgs e)
        {
            CheckBox checkbox = sender as CheckBox;

            if (checkbox.Tag.GetType() == typeof(Tasklet))
            {
                for (int i = 0; i < validTasks.Count; i++)
                {
                    if (validTasks[i] == checkbox.Tag)
                    {
                        IsChecked[i] = false;
                    }
                }
            }
        }

        private void Delete_All_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < validTasks.Count; i++)
            {
                
                TaskerStore.CurrentTasks.Remove(validTasks[i]);
                validTasks[i].Delete();
            }
            validTasks.Clear();
            borders.Clear();
            stackPanelNumber.Clear();
            IsChecked.Clear();
            containsTextStackPanel[0].Children.Clear();

            TaskerStore.CurrentTasks = TaskerStore.CurrentTasks;
        }

        private void Delete_Selected_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < IsChecked.Count; i++)
            {
                
                if (IsChecked[i])
                {
                    containsTextStackPanel[0].Children.RemoveAt(containsTextStackPanel[0].Children.IndexOf(borders[i]) + 1);
                    containsTextStackPanel[0].Children.Remove(borders[i]);

                    TaskerStore.CurrentTasks.Remove(validTasks[i]);

                    validTasks[i].Delete();
                    validTasks.RemoveAt(i);
                    borders.RemoveAt(i);
                    stackPanelNumber.RemoveAt(i);
                    IsChecked.RemoveAt(i);
                    i = 0;
                }


                if (validTasks.Count != 0)  
                {
                    validTasks[i].stackPanelNumber = stackPanelNumber[i];
                }
            }

            if (IsChecked[0])
            {
                containsTextStackPanel[0].Children.RemoveAt(containsTextStackPanel[0].Children.IndexOf(borders[0]) + 1);
                containsTextStackPanel[0].Children.Remove(borders[0]);

                TaskerStore.CurrentTasks.Remove(validTasks[0]);

                validTasks[0].Delete();
                validTasks.RemoveAt(0);
                borders.RemoveAt(0);
                stackPanelNumber.RemoveAt(0);
                IsChecked.RemoveAt(0);
            }

            if (validTasks.Count != 0)
            {
                validTasks[0].stackPanelNumber = stackPanelNumber[0];
            }

            TaskerStore.CurrentTasks = TaskerStore.CurrentTasks;

            for (int i = 0; i < validTasks.Count; i++)
            {
                validTasks[i].stackPanelNumber = 0;
            }

            CheckStackPanel();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            for(int i = 0;i < validTasks.Count;i++)
            {
                validTasks[i].stackPanels = TaskerStore.CurrentStackpanels;
                validTasks[i].stackPanelNumber = stackPanelNumber[i];

            }
            validTasks.Clear();
            borders.Clear();
            stackPanelNumber.Clear();
            IsChecked.Clear();
            containsTextStackPanel[0].Children.Clear();


        }



    }
}
