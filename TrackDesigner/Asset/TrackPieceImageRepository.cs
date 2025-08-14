using System.Windows;
using System.Windows.Media;
using TrackDesigner.Model;

namespace TrackDesigner.Asset;

public class TrackPieceImageRepository
{
    public DrawingImage GetImage(TrackPieceType trackPieceType)
    {
        return trackPieceType switch
        {
            TrackPieceType.OuterCorner => OuterCorner,
            TrackPieceType.Straight => Straight,
            TrackPieceType.Apex => Apex,
            TrackPieceType.None => None,
            TrackPieceType.Start => Start,
            _ => throw new ArgumentOutOfRangeException(nameof(trackPieceType), trackPieceType, null)
        };
    }

    private static readonly DrawingImage OuterCorner = Application.Current.FindResource("OuterCorner") as DrawingImage;
    private static readonly DrawingImage Apex = Application.Current.FindResource("Apex") as DrawingImage;
    private static readonly DrawingImage None = null;
    private static readonly DrawingImage Straight = Application.Current.FindResource("Straight") as DrawingImage;
    private static readonly DrawingImage Start = Application.Current.FindResource("Start ") as DrawingImage;
}