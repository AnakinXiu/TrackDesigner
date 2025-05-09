using System.Drawing;
using System.Windows.Input;
using TrackDesigner.ViewModels;

namespace TrackDesigner.Tools;

public enum TrackType
{
    Straight,
    Start,
    Corner,
    Apex,
}

public class CornerTool : ITool
{
    public bool IsAvailable { get; private set; }

    public bool OnMouseClick(object sender, MouseFloatEventArgs args)
    {
        if (sender is not TrackPiece trackPiece)
            return false;

        if (trackPiece.TrackType is TrackType.Corner)
        {
            trackPiece.Rotation = (RotateDegree)(((int)trackPiece.Rotation + 1) % 4);
            return true;
        }

        return false;
    }
}

public interface ITool
{
    bool IsAvailable { get; }

    bool OnMouseClick(object sender, MouseFloatEventArgs args);
}

public class MouseFloatEventArgs : EventArgs
{
    public MouseFloatEventArgs(MouseButton button, int clicks, float x, float y, int delta)
    {
        Button = button;
        Clicks = clicks;
        X = x;
        Y = y;
        Delta = delta;
    }

    public MouseButton Button { get; }

    public int Clicks { get; }

    public int Delta { get; }

    public PointF Location => new PointF(X, Y);

    /// <summary>
    /// Gets or sets the X (in world coordinates).
    /// </summary>
    public float X { get; set; }

    /// <summary>
    /// Gets or sets the Y (in world coordinates).
    /// </summary>
    public float Y { get; set; }
}