using System.Drawing;
using System.Windows.Input;

namespace TrackDesigner.Tools;

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