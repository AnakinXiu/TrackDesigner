using TrackDesigner.Persistence;

namespace TrackDesigner.Model;

public class DesignProject
{
    public string ProjectName { get; set; }

    public string Description { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }

    public int TrackPieceWidth { get; set; }

    public Dictionary<TrackPieceType, int> TrackPieceCounts { get; set; }

    public IEnumerable<TrackPieceDto> TrackPieces { get; set; }
}