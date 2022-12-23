using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Runtime.Serialization;
using System.Xml;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;

namespace Tasker
{
    [Serializable()]
    public class Profile : ISerializable
    {
        public string nameOfProfile;
        public List<>
        public List<StackPanel> stackPanels = new List<StackPanel>();
        public List<Label> labels = new List<Label>();
        public List<ScrollViewer> scrollViewers = new List<ScrollViewer>();
        public int stackpanelcount = 0;
        public Profile(List<string> _namesOfContainters, string _nameOfProfile)
        {
            foreach (var name in _namesOfContainters)
            {
                Label label = new Label();
                label.Content = name;
                label.HorizontalAlignment = HorizontalAlignment.Center;
                label.VerticalAlignment = VerticalAlignment.Center;

                Grid.SetColumn(label, _namesOfContainters.IndexOf(name));

                ScrollViewer scrollViewer = new ScrollViewer();
                
                Grid.SetColumn(scrollViewer, _namesOfContainters.IndexOf(name));
                Grid.SetRow(scrollViewer, 1);

                StackPanel stackpanel = new StackPanel();
                stackpanel.HorizontalAlignment = HorizontalAlignment.Stretch;
                stackpanel.Name = name;

                scrollViewer.Content = stackpanel;

                stackPanels.Add(stackpanel);
                labels.Add(label);
                scrollViewers.Add(scrollViewer);


                stackpanelcount++;
            }
        }

        public static void DisplayProfile(Profile _profile)
        {
            TaskerStore.Profile = _profile;
        }

        public void DisplayProfile()
        {
            TaskerStore.Profile = this;
        }


        public Profile(SerializationInfo info, StreamingContext context)
        {
            nameOfProfile = (string)info.GetValue("NameOfProfile", typeof(string));
            labels = (List<Label>)info.GetValue("Labels", typeof(List<Label>));

            foreach (var name in labels)
            {
                Label label = new Label();
                label.Content = name;
                label.HorizontalAlignment = HorizontalAlignment.Center;
                label.VerticalAlignment = VerticalAlignment.Center;

                Grid.SetColumn(label, labels.IndexOf(name));

                ScrollViewer scrollViewer = new ScrollViewer();

                Grid.SetColumn(scrollViewer, labels.IndexOf(name));
                Grid.SetRow(scrollViewer, 1);

                StackPanel stackpanel = new StackPanel();
                stackpanel.HorizontalAlignment = HorizontalAlignment.Stretch;
                stackpanel.Name = name;

                scrollViewer.Content = stackpanel;

                stackPanels.Add(stackpanel);
                labels.Add(label);
                scrollViewers.Add(scrollViewer);


                stackpanelcount++;
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("NameOfProfile", nameOfProfile);
            info.AddValue("Labels", labels);
        }

    }
}
