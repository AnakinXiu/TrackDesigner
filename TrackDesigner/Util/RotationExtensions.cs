
namespace TrackDesigner.Util;

public static class RotationExtensions
{
    public static double ToDegree(this RotateDegree rotateDegree)
    {
        return rotateDegree switch
        {
            RotateDegree.None => 0,
            RotateDegree._90 => 90,
            RotateDegree._180 => 180,
            RotateDegree._270 => 270,
            _ => throw new ArgumentOutOfRangeException(nameof(rotateDegree), rotateDegree, null)
        };
    }

    public static RotateDegree ToEnum(this double degree)
    {
        return degree switch
        {
            0 => RotateDegree.None,
            90 => RotateDegree._90,
            180 => RotateDegree._180,
            270 => RotateDegree._270,
            _ => throw new ArgumentOutOfRangeException(nameof(degree), degree, null)
        };
    }
}