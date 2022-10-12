using AcademicTimePlanner.DataMapping.Plan;
using System.IO;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;



namespace AcademicTimePlanner.JSONHandling
{
    public class importExportJSON
    {
        

        JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,  // set camelCase       
            WriteIndented = true                                // write pretty json
        };

        private string getDataPath()
        {
            string directory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName; ;
            string dataPath = directory + @"\AcademicTimePlanner\JSON_Files\ATP_data.json";
            return dataPath;
        }

        public PlanProject loadJson(string path) {
            string fileName = "PlanProject.json";
            string jsonString = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<PlanProject>(jsonString);
        }
        
        public void saveJson(PlanProject project)
        {
            string jsonString = JsonSerializer.Serialize(project, options);
            File.WriteAllText(getDataPath(), jsonString);
        }
    }
}
