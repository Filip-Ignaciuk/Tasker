using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;


namespace Tasker
{
    public class TaskerConfigurator
    {
        public static bool isinitialised = System.IO.Directory.Exists(TaskerStore.DocumentDir + @"\Tasker");
        public static void TaskerSetup()
        {
            System.IO.Directory.CreateDirectory(TaskerStore.DocumentDir + @"\Tasker\");
            System.IO.Directory.CreateDirectory(TaskerStore.DocumentDir + @"\Tasker\Tasklets");
            System.IO.Directory.CreateDirectory(TaskerStore.DocumentDir + @"\Tasker\Profiles");

            StreamWriter lp = new StreamWriter(TaskerStore.DocumentDir + @"\Tasker\lp.txt");
            lp.WriteLine(TaskerStore.DocumentDir + @"\Tasker\Profiles\Profile_default.dat");
            lp.Close();
            

            List<String> names = new List<String>();
            names.Add("Urgent");
            names.Add("Required");
            names.Add("Optional");
            names.Add("Other");

            Profile profile = new Profile(names, "default");
            TaskerConfigurator.SaveProfile(profile);

            StreamWriter lts = new StreamWriter(TaskerStore.DocumentDir + @"\Tasker\lts.txt");
            lts.WriteLine("0");
            lts.Close();

            

            StreamWriter streamWriter = new StreamWriter(TaskerStore.DocumentDir + @"\Tasker\IdNumber.txt");
            streamWriter.WriteLine(0);
            streamWriter.Close();
            isinitialised = true;
        }


        

        //Recommend to save in one heap to contain order within the file
        public static void SaveTasklet(Tasklet _tasklet)
        {
            Stream stream = File.Open(TaskerStore.DocumentDir + @"\Tasker\Tasklets\Tasklet" + _tasklet.Id + ".dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, _tasklet);
            stream.Close();
            StreamWriter streamwriter = new StreamWriter(TaskerStore.DocumentDir + @"\Tasker\IdNumber.txt");
            streamwriter.WriteLine(TaskerStore.Id - 1);
            streamwriter.Close();
        }

        public static void SaveTasklets()
        {
            foreach (Tasklet _tasklet in TaskerStore.CurrentTasks)
            {
                Stream stream = File.Open(TaskerStore.DocumentDir + @"\Tasker\Tasklets\Tasklet" + _tasklet.Id + ".dat", FileMode.Create);
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, _tasklet);
                stream.Close();

                
                
            }
            StreamWriter streamwriter = new StreamWriter(TaskerStore.DocumentDir + @"\Tasker\IdNumber.txt");
            streamwriter.WriteLine(TaskerStore.Id);
            streamwriter.Close();
        }

        //public static Tasklet LoadTasklet(Tasklet _tasklet)
        //{
        //    Stream stream = File.Open(@"\Tasklets\Tasklet" + _tasklet.Id + ".dat", FileMode.Open);
        //    BinaryFormatter formatter = new BinaryFormatter();
        //    Tasklet tasklet = (Tasklet)formatter.Deserialize(stream);
        //    stream.Close();
        //}

        public static Tasklet[] LoadTasklets()
        {
            List<Tasklet> list = new List<Tasklet>();
            foreach (string fileName in Directory.GetFiles(TaskerStore.DocumentDir + @"\Tasker\Tasklets"))
            {
                Stream stream = File.Open(fileName, FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                Tasklet tasklet = (Tasklet)formatter.Deserialize(stream);
                tasklet.stackPanels = TaskerStore.CurrentStackpanels;
                list.Add(tasklet);
                stream.Close();

            }

            return list.ToArray();
        }

        public static void SaveProfile(Profile _profile)
        {
            Stream stream = File.Open(TaskerStore.DocumentDir + @"\Tasker\Profiles\Profile_" + _profile.nameOfProfile + ".dat", FileMode.Create);
            StreamWriter lp = new StreamWriter(TaskerStore.DocumentDir + @"\Tasker\lp.txt");
            lp.WriteLine(TaskerStore.DocumentDir + @"\Tasker\Profiles\Profile_" + _profile.nameOfProfile + ".dat");
            lp.Close();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, _profile);
            stream.Close();
        }

        public static Profile LoadProfile(Profile _profile)
        {
            Stream stream = File.Open(TaskerStore.DocumentDir + @"\Tasker\Profiles\Profile_" + _profile.nameOfProfile + ".dat", FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            Profile profile = (Profile)formatter.Deserialize(stream);
            stream.Close();
            return profile;
        }

        public static Profile LoadProfile(string _path)
        {
            Profile profile;
            _path = _path.Replace("\r", "");
            _path = _path.Replace("\n", "");
            try
            {
                Stream stream = File.Open(_path, FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                profile = (Profile)formatter.Deserialize(stream);

                stream.Close();
            }
            catch (Exception ex)
            {
                Stream stream = File.Open(TaskerStore.DocumentDir + @"\Tasker\Profiles\Profile_default.dat", FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                profile = (Profile)formatter.Deserialize(stream);

                stream.Close();
            }
            finally
            {
                
            }



            
            return profile;
        }
    }
}
