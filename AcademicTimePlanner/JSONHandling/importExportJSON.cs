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
            //TODO fixing this attempt at getting the data path for the save files.
            string directory = this.getDataPath().ToString();
            string dataPath = Path.Combine(directory, @"\JSON_Files\ATP_data.json");
            return dataPath;
        }

        // pass options to serializer
        //var json = JsonSerializer.Serialize(jsonDataToSerialize, options);
        // pass options to deserializer
        //var order = JsonSerializer.Deserialize<ModelClass>(json, options);

        /*public PlanProject loadJson() {
            string fileName = "PlanProject.json";
            string jsonString = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<PlanProject>(jsonString);
        }
        */
        public void saveJson(PlanProject project)
        {
            string jsonString = JsonSerializer.Serialize(project, options);
            File.WriteAllText(getDataPath(), jsonString);
        }
    }
}
