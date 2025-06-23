using TrackDesigner.Model.Tracks;

namespace TrackDesigner.Model;

public class TrackDesignProject
{
    public string ProjectName { get; set; }

    public string Description { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }

    public int TrackPieceWidth { get; set; }

    public IEnumerable<TrackModel> TrackPieces { get; set; }
}