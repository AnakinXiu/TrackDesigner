
using TrackDesigner.Model;

namespace TrackDesigner.Util;

public static class RotationExtensions
{
    public static double ToDegree(this RotateDegree rotateDegree)
    {
        return rotateDegree switch
        {
            RotateDegree.None => 0,
            RotateDegree.Clockwise90 => 90,
            RotateDegree.Clockwise180 => 180,
            RotateDegree.Clockwise270 => 270,
            _ => throw new ArgumentOutOfRangeException(nameof(rotateDegree), rotateDegree, null)
        };
    }

    public static RotateDegree ToEnum(this double degree)
    {
        return degree switch
        {
            0 => RotateDegree.None,
            90 => RotateDegree.Clockwise90,
            180 => RotateDegree.Clockwise180,
            270 => RotateDegree.Clockwise270,
            _ => throw new ArgumentOutOfRangeException(nameof(degree), degree, null)
        };
    }
}