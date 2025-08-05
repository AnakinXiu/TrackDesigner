namespace TrackDesigner.Persistence;

public class ProjectSettings
{
    public int TrackPieceWidth { get; set; }

    public Dictionary<TrackPieceType, int> TrackPieceCounts { get; set; }
}