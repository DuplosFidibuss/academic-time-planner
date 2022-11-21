using AcademicTimePlanner.DataMapping.Plan;
using Newtonsoft.Json;


namespace AcademicTimePlanner.JSONHandling
{   /// <summary>
    /// This class implements the saving and loading of JSON files. 
    /// </summary>
    public class JsonFileHandler
    {
        private string getDataPath(String filename)
        {
            string directory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName; ;
            string dataPath = directory + @"\AcademicTimePlanner\JSON_Files\" + filename + ".json";
            return dataPath;
        }

        public PlanProject loadJson(string path)
        {
            string jsonString = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<PlanProject>(jsonString, new JsonSerializerSettings
            {
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
            });
        }

        public void saveJson(PlanProject project)
        {
            string jsonString = JsonConvert.SerializeObject(project, Formatting.Indented);
            File.WriteAllText(getDataPath(project.Name), jsonString);
        }
    }
}
