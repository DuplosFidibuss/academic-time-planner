using AcademicTimePlanner.DataMapping.Plan;
using System.IO;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;



namespace AcademicTimePlanner.JSONHandling
{   /// <summary>
    /// This class implements the saving and loading of JSON files. 
    /// </summary>
    public class importExportJSON
    {

        JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,  // set camelCase       
            WriteIndented = true,                              // write pretty json
            IncludeFields = true
        };

        private string getDataPath()
        {
            string directory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName; ;
            string dataPath = directory + @"\AcademicTimePlanner\JSON_Files\ATP_data.json";
            return dataPath;
        }

        public PlanProject loadJson(string path) {
            string jsonString = File.ReadAllText(path);
            return JsonSerializer.Deserialize<PlanProject>(jsonString);
        }
        
        public void saveJson(PlanProject project)
        {
            string jsonString = JsonSerializer.Serialize(project, options);
            File.WriteAllText(getDataPath(), jsonString);
        }
    }
}
