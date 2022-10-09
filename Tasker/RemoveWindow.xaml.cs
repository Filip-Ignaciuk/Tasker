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
        public RemoveWindow()
        {
            InitializeComponent();
        }

        private void TextBoxTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxTitle.Text == "")
            {
                ContainsTextStackPanel.Children.Clear();
            }


            foreach (var stackpanel in TaskerStore.CurrentStackpanels)
            {
                StackPanel tempstackpanel = stackpanel;
                if (tempstackpanel == null)
                {
                    break;
                    
                }

                foreach (var child in tempstackpanel.Children)
                {
                    if (child.GetType() == typeof(Grid))
                    {
                        int i = 0;
                        Grid grid = (Grid)child;
                        string Title = string.Empty;
                        string Description = string.Empty;
                        
                        // You have to completely delete the rooted child of the stackpanel, containing the spcific task, hence the creation of a new task.
                        foreach (var gridChild in grid.Children)
                        {
                            

                            if (gridChild.GetType() == typeof(Label))
                            {
                                Label label = (Label)gridChild;
                                string labelContent = (string)label.Content;
                                if (labelContent.Contains(TextBoxTitle.Text))
                                {
                                    i++;
                                    
                                }
                                if (i == 1)
                                {
                                    Title = labelContent;
                                }


                                if (i == 2 && Title != string.Empty) //  && ContainsTheTask(Title, Description, ContainsTextStackPanel)
                                {
                                    Description = labelContent;
                                    StackPanel[] stackpanels = new StackPanel[1];
                                    stackpanels[0] = ContainsTextStackPanel;
                                    Task task = new Task(Title, Level.RemovingList, Description, ref stackpanels);
                                }
                            }

                            

                            
                        }    
                    }
                }
            }

            // Fix this 

            static bool ContainsTheTask(string _title, string _description, StackPanel _stackpanel)
            {
                
                foreach(var child in _stackpanel.Children)
                {
                    if (child.GetType() == typeof(Grid))
                    {
                        Grid grid = (Grid)child;
                        foreach (var gridChild in grid.Children)
                        {


                            if (gridChild.GetType() == typeof(Label))
                            {
                                Label label = (Label)gridChild;
                                if (_title == (string)label.Content)
                                {
                                    return true;
                                }

                                if (_description == (string)label.Content)
                                {
                                    return true;
                                }
                                
                            }




                        }
                    }
                }
                return false;
            }

        }
    }
}
