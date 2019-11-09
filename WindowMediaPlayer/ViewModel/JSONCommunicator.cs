using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowMediaPlayer.Model;

namespace WindowMediaPlayer.ViewModel
{
    public class JSONCommunicator
    {
        /// <summary>
        /// Serializes an object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializableObject"></param>
        /// <param name="fileName"></param>
        public void SerializeObject<T>(ObservableCollection<T> listOfSerializableObject, string fileName)
        {
            if (listOfSerializableObject == null) { return; }
            string output = JsonConvert.SerializeObject(listOfSerializableObject, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Newtonsoft.Json.Formatting.Indented }); ;
            File.WriteAllText(fileName, output);
        }

        /// <summary>
        /// Deserializes an json file into an object list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public ObservableCollection<T> DeSerializeObject<T>(string fileName)
        {
            ObservableCollection<T> list;
            using (StreamReader reader = new StreamReader(fileName))
            {
                string json = reader.ReadToEnd();
                list = JsonConvert.DeserializeObject<ObservableCollection<T>>(json);
            }
            return list;
        }
    }
}
