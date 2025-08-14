namespace TrackDesigner.Model;

public class TrackModel
{
    public TrackPieceType TrackPieceType { get; }

    public TrackPieceOrientation Orientation { get; set; }

    public TrackModel()
        : this(TrackPieceType.None, TrackPieceOrientation.Upwards)
    { }

    public TrackModel(TrackPieceType trackPieceType)
        : this(trackPieceType, TrackPieceOrientation.Upwards)
    { }

    public TrackModel(TrackPieceType trackPieceType, TrackPieceOrientation orientation)
    {
        TrackPieceType = trackPieceType;
        Orientation = orientation;
    }
}