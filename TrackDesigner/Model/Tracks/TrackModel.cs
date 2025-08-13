using System.Windows;
using System.Windows.Media;

namespace TrackDesigner.Model;

public class TrackModel
{
    public TrackPieceType TrackPieceType { get; }

    public TrackPieceOrientation Orientation { get; set; }

    public DrawingImage TrackImage { get; }

    private TrackModel(TrackPieceType trackPieceType, DrawingImage trackImage)
    {
        TrackPieceType = trackPieceType;
        TrackImage = trackImage;
    }

    public static readonly TrackModel OuterCorner
        = new(TrackPieceType.OuterCorner, Application.Current.FindResource("OuterCorner") as DrawingImage);

    public static readonly TrackModel Apex
        = new(TrackPieceType.Apex, Application.Current.FindResource("Apex") as DrawingImage);

    public static readonly TrackModel None
        = new(TrackPieceType.None, null);

    public static readonly TrackModel Straight
        = new(TrackPieceType.Straight, Application.Current.FindResource("Straight") as DrawingImage);

    public static TrackModel GetTrackModel(TrackPieceType trackPieceType)
    {
        return trackPieceType switch
        {
            TrackPieceType.OuterCorner => OuterCorner,
            TrackPieceType.Straight => Straight,
            TrackPieceType.Apex => Apex,
            TrackPieceType.None => None,
            _ => throw new ArgumentOutOfRangeException(nameof(trackPieceType), trackPieceType, null)
        };
    }
}