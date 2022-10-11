using AcademicTimePlanner.DataMapping.Plan;
using System.Text.Json;



namespace AcademicTimePlanner.JSONHandling
{
    public class importExportJSON
    {
        private const string DataPath = @"..\..\AcademicTimePlanner.Tests\JSON_Files\ATP_data.json";

        JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,  // set camelCase       
            WriteIndented = true                                // write pretty json
        };

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
        public void safeJson(PlanProject project)
        {
            string jsonString = JsonSerializer.Serialize(project, options);
            File.WriteAllText(DataPath, jsonString);
        }
    }
}
