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
    /// Interaction logic for AddProfileWindow.xaml
    /// </summary>
    public partial class AddProfileWindow : Window
    {
        public static List<string> names = new List<string>();
        public static List<string> selectedNames = new List<string>();

        public AddProfileWindow()
        {
            InitializeComponent();
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            bool IsValid = true;
            foreach(var name in names)
            {
                if (name.ToLower() == TextBoxProfileInput.Text.ToLower())
                {
                    MessageBox.Show("Cannot create title with the same name.");
                    IsValid = false;
                }
            }
            if (IsValid)
            {
                names.Add(TextBoxProfileInput.Text);
                Label label = new Label();
                label.Content = TextBoxProfileInput.Text;
                CurrentNames.Children.Add(label);
                CheckBox checkbox = MakeCheckBox(TextBoxProfileInput.Text);
                CurrentNames.Children.Add(checkbox);
            }
        }
        

        private void Remove(object sender, RoutedEventArgs e)
        {
            for (int h = 0; h < names.Count; h++)
            {
                for (int i = 0; i < selectedNames.Count; i++)
                {
                    if (names[h] == selectedNames[i])
                    {
                        for (int j = 0; j < CurrentNames.Children.Count; j++)
                        {
                            if (CurrentNames.Children[j].GetType() == typeof(Label))
                            {
                                Label label = (Label)CurrentNames.Children[j];
                                if (label.Content == names[h])
                                {
                                    CurrentNames.Children.RemoveAt(j + 1);
                                    CurrentNames.Children.Remove(label);
                                    
                                    selectedNames.Remove(names[h]);
                                    names.Remove(names[h]);
                                    
                                }
                            }
                        }
                    }
                }
            }
        }
        public static CheckBox MakeCheckBox(string _title)
        {

            CheckBox checkbox = new CheckBox();
            checkbox.Name = _title + "select";
            checkbox.Tag = _title;
            checkbox.Checked += Select_Check;
            checkbox.Unchecked += Select_Uncheck;

            return checkbox;
        }

        private static void Select_Check(object sender, RoutedEventArgs e)
        {
            CheckBox checkbox = sender as CheckBox;

            if (checkbox.Tag.GetType() == typeof(string))
            {
                for (int i = 0; i < names.Count; i++)
                {
                    if (checkbox.Tag == names[i])
                    {
                        selectedNames.Add(names[i]);
                    }
                }
            }

        }

        private static void Select_Uncheck(object sender, RoutedEventArgs e)
        {
            CheckBox checkbox = sender as CheckBox;

            if (checkbox.Tag.GetType() == typeof(string))
            {
                for (int i = 0; i < names.Count; i++)
                {
                    if (checkbox.Tag == names[i])
                    {
                        selectedNames.Remove(names[i]);
                    }
                }
            }
        }

        private void CreateProfile(object sender, RoutedEventArgs e)
        {
            Profile profile = new Profile(names, TextBoxProfileName.Text);
            profile.DisplayProfile();
        }
    }
}
