namespace TrackDesigner.Persistence;

public class TrackDesignProjectDto
{
    public string ProjectName { get; set; }

    public string Description { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }

    public ProjectSettings ProjectSettings { get; set; }

    public IEnumerable<TrackPiece> TrackPieces { get; set; } 
}