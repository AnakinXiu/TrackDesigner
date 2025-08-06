using System.IO;
using System.IO.Compression;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Shapes;

namespace TrackDesigner.Persistence;

public class ProjectPersistence
{
    public void LoadProject(string path)
    {
        var zipArchive = ZipFile.OpenRead(path);
        foreach (var entry in zipArchive.Entries)
        {
            if (entry.Name == "settings.json")
            {
                using var stream = entry.Open();
                using var reader = new StreamReader(stream, Encoding.UTF8);
                var settingsJson = reader.ReadToEnd();
                // Deserialize settingsJson to ProjectSettings object
            }
            else if (entry.Name.EndsWith(".trackpiece"))
            {
                using var stream = entry.Open();
                using var reader = new StreamReader(stream, Encoding.UTF8);
                var trackPieceData = reader.ReadToEnd();
                // Deserialize trackPieceData to TrackPieceDto object
            }
        }
    }

    public void SaveProject(TrackDesignProjectDto projectDto, string path)
    {
        var zipArchive = ZipFile.Open(path, ZipArchiveMode.Update, Encoding.UTF8);
        zipArchive.GetEntry("").Delete();

        var entry = zipArchive.CreateEntry("");
        using var stream = entry.Open();
        JsonSerializer.Serialize(stream, projectDto, JsonSerializerOptions.Default);
    }
}