using System.IO;
using System.IO.Compression;
using System.Text;
using System.Text.Json;
using TrackDesigner.Model;

namespace TrackDesigner.Persistence;

public class ProjectPersistence
{
    private const string ProjectInfoEntryName = "ProjectInfo.json";
    private const string TrackPiecesEntryName = "TrackPieceLayout.json";

    public TrackDesignProject LoadProject(string path)
    {
        var zipArchive = ZipFile.OpenRead(path);
        var projectInfo = LoadZipEntryAsJson<ProjectInfo>(zipArchive, ProjectInfoEntryName);
        var tackPieceDto = LoadZipEntryAsJson<TrackPieceLayoutDto>(zipArchive, TrackPiecesEntryName);

        return new TrackDesignProject(projectInfo, tackPieceDto);
    }

    private static T LoadZipEntryAsJson<T>(ZipArchive zipArchive, string entryName)
    {
        var entry = zipArchive.GetEntry(entryName);
        if (entry is null)
        {
            // TODO: Handle missing entry case, maybe throw an exception or return default
        }

        using var stream = entry.Open();

        using var reader = new StreamReader(stream, Encoding.UTF8);
        var settingsJson = reader.ReadToEnd();
        var result = JsonSerializer.Deserialize<T>(settingsJson);

        if (result is null)
        {
            // 
        }

        return result;
    }

    public void SaveProject(TrackDesignProject project, string path)
    {
        var zipArchive = ZipFile.Open(path, ZipArchiveMode.Update, Encoding.UTF8);

        WriteZipEntry(zipArchive, ProjectInfoEntryName, project.ProjectInfo);
        WriteZipEntry(zipArchive, TrackPiecesEntryName, new TrackPieceLayoutDto(project.TrackPieces));
    }

    private static void WriteZipEntry(ZipArchive zipArchive, string entryName, object data)
    {
        zipArchive.GetEntry(entryName)?.Delete();
    
        var entry = zipArchive.CreateEntry(entryName);
        using var stream = entry.Open();
        JsonSerializer.Serialize(stream, data, JsonSerializerOptions.Default);
    }
}