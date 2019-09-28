using Newtonsoft.Json;
using System.IO;

namespace Jjson
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Createson JsonObj = new Createson();
            JsonData jsonData = new JsonData();
            JsonObj = jsonData.CreatesonOps();

            string JSONresult = JsonConvert.SerializeObject(JsonObj);
            string path = @"C:\Users\kitti\source\repos\json\Create.json";

            if (File.Exists(path))
            {
                File.Delete(path);
                using(var DD = new StreamWriter(path, true))
                {
                    DD.WriteLine(JSONresult.ToString());
                    DD.Close();
                }

            }
            else if (!File.Exists(path))
            {
                using (var DD = new StreamWriter(path, true))
                {
                    DD.WriteLine(JSONresult.ToString());
                    DD.Close();
                }
            }
        }
    }
}
