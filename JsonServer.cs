using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace pract_5_Hojaeva
{
    internal class JsonServer
    {
        static private string file = "data.json";

        static public void Save(ObservableCollection<Training> training)
        {
            var option = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(training, option);
            File.WriteAllText(file, jsonString);
        }

        static public ObservableCollection<Training> Load()
        {
            if (!File.Exists(file))
                return new ObservableCollection<Training>();

            string jsonString = File.ReadAllText(file);
            ObservableCollection<Training> training = JsonSerializer.Deserialize<ObservableCollection<Training>>(jsonString);
            return training;
        }
    }
}
