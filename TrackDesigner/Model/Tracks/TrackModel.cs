using System.Windows;
using System.Windows.Media;
using TrackDesigner.Tracks;

namespace TrackDesigner.Model.Tracks;

public class TrackModel
{
    public TrackType TrackType { get; }

    public DrawingImage TrackImage { get; }

    private TrackModel(TrackType trackType, DrawingImage trackImage)
    {
        TrackType = trackType;
        TrackImage = trackImage;

    }

    public static readonly TrackModel OuterCorner
        = new(TrackType.Corner, Application.Current.FindResource("SvgDrawingImage") as DrawingImage);

    public static readonly TrackModel Apex
        = new(TrackType.Apex, Application.Current.FindResource("SvgDrawingImage") as DrawingImage);

    public static readonly TrackModel None
        = new(TrackType.None, null);

    public static TrackModel GetTrackModel(TrackType trackType)
    {
        return trackType switch
        {
            TrackType.Corner => OuterCorner,
            TrackType.Apex => Apex,
            TrackType.None => None,
            _ => throw new ArgumentOutOfRangeException(nameof(trackType), trackType, null)
        };
    }
}