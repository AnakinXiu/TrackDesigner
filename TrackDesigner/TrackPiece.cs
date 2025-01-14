using System.Windows;
using System.Windows.Media;

namespace TrackDesigner;

public class TrackPiece : FrameworkElement
{
    public Point Location { get; set; }

    public IPieceRender PieceRender { get; set; }

    protected override void OnRender(DrawingContext drawingContext)
    {
        base.OnRender(drawingContext);

        var transform = Transform.Identity;
        transform.Value.Translate(Location.X, Location.Y);
        drawingContext.PushTransform(transform);

        PieceRender?.RenderPiece(drawingContext);

        drawingContext.DrawRectangle(null, new Pen(Brushes.DarkGray, 1), new Rect(Location, new Size(Width, Height)));

        drawingContext.Pop();
    }
}

public interface IPieceRender
{
    void RenderPiece(DrawingContext drawingContext);
}