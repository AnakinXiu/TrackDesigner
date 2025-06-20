using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace TrackDesigner.Views;

[TemplatePart(Name = "Part_Border", Type = typeof(Border))]
public class TrackPieceControl : Control
{
    static TrackPieceControl()
    {
        // Override the default style
        DefaultStyleKeyProperty.OverrideMetadata(typeof(TrackPieceControl),
            new FrameworkPropertyMetadata(typeof(TrackPieceControl)));
    }

    public DrawingImage DrawingImage
    {
        get => (DrawingImage)GetValue(DrawingImageProperty);
        set => SetValue(DrawingImageProperty, value);
    }

    public static readonly DependencyProperty DrawingImageProperty = DependencyProperty.Register(
        nameof(DrawingImage), typeof(DrawingImage), typeof(TrackPieceControl),
        new FrameworkPropertyMetadata(default(DrawingImage), FrameworkPropertyMetadataOptions.AffectsRender));

    public RotateTransform RotateTransform
    {
        get => (RotateTransform)GetValue(RotateTransformProperty);
        set => SetValue(RotateTransformProperty, value);
    }

    public static readonly DependencyProperty RotateTransformProperty = DependencyProperty.Register(
        nameof(RotateTransform), typeof(RotateTransform), typeof(TrackPieceControl),
        new FrameworkPropertyMetadata(default(RotateTransform), FrameworkPropertyMetadataOptions.AffectsRender));

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