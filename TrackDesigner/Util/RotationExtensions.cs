
using TrackDesigner.Model;

namespace TrackDesigner.Util;

public static class RotationExtensions
{
    public static double ToDegree(this TrackPieceOrientation orientation)
    {
        return orientation switch
        {
            TrackPieceOrientation.Upwards => 0,
            TrackPieceOrientation.Rightwards => 90,
            TrackPieceOrientation.Downwards => 180,
            TrackPieceOrientation.Leftwards=> 270,
            _ => throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null)
        };
    }

    public static TrackPieceOrientation ToEnum(this double degree)
    {
        return degree switch
        {
            0 => TrackPieceOrientation.Upwards,
            90 => TrackPieceOrientation.Rightwards,
            180 => TrackPieceOrientation.Downwards,
            270 => TrackPieceOrientation.Leftwards,
            _ => throw new ArgumentOutOfRangeException(nameof(degree), degree, null)
        };
    }

    public static TrackPieceOrientation Next(this TrackPieceOrientation orientation)
    {
        return orientation switch
        {
            TrackPieceOrientation.Upwards => TrackPieceOrientation.Rightwards,
            TrackPieceOrientation.Rightwards => TrackPieceOrientation.Downwards,
            TrackPieceOrientation.Downwards => TrackPieceOrientation.Leftwards,
            TrackPieceOrientation.Leftwards => TrackPieceOrientation.Upwards,
            _ => throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null)
        };
    }
}