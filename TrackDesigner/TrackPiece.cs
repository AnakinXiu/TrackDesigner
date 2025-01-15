using System.Globalization;
using System.Windows;
using System.Windows.Input;
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

        drawingContext.DrawRectangle(Brushes.Chartreuse, new Pen(Brushes.DarkGray, 1), new Rect(Location, new Size(Width, Height)));

        if (MouseState.TryPeek(out var result))
            drawingContext.DrawText(
                new FormattedText(result, CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                    new Typeface("MicroSoft Yahei"), 10d, Brushes.Transparent, new NumberSubstitution(), 96d),
                new Point(10, 10));

        drawingContext.Pop();
    }

    public Stack<string> MouseState = new(3);

    protected override void OnMouseDown(MouseButtonEventArgs e)
    {
        base.OnMouseDown(e);

        MouseState.Push($"MouseDown: {e.GetPosition(this)}");
    }

    protected override void OnMouseUp(MouseButtonEventArgs e)
    {
        base.OnMouseUp(e);
        MouseState.Push($"MouseUp: {e.GetPosition(this)}");
    }

    protected override void OnMouseEnter(MouseEventArgs e)
    {
        base.OnMouseEnter(e);
        MouseState.Push($"MouseEnter: {e.GetPosition(this)}");
    }

    protected override void OnMouseLeave(MouseEventArgs e)
    {
        base.OnMouseLeave(e);
        MouseState.Push($"MouseLeave: {e.GetPosition(this)}");
    }
}

public interface IPieceRender
{
    void RenderPiece(DrawingContext drawingContext);
}