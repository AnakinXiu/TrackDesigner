using TrackDesigner.Model;

namespace TrackDesigner.Persistence;

public class TrackPieceDto
{
    public int X { get; set; }

    public int Y { get; set; }

    public TrackPieceType Type { get; set; }

    public TrackPieceOrientation Orientation { get; set; }
}