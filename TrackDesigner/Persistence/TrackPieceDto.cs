using TrackDesigner.Model;

namespace TrackDesigner.Persistence;

internal class TrackPieceDto
{
    public int X { get; set; }

    public int Y { get; set; }

    public TrackPieceType Type { get; set; }

    public TrackPieceOrientation Orientation { get; set; }
}