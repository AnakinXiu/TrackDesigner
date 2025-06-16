using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using TrackDesigner.Tools;
using TrackDesigner.Util;

namespace TrackDesigner.ViewModels;

public class TrackPiece : INotifyPropertyChanged
{
    public int X { get; set; }

    public int Y { get; set; }

    public Size Size { get; set; }

    public DrawingImage DrawingImage { get; set; }

    public TrackType TrackType { get; set; }

    public RotateTransform Rotate { get; }

    public RotateDegree Rotation
    {
        get => Rotate.Angle.ToEnum();
        set
        {
            Rotate.Angle = value.ToDegree();
            PropertyChanged?.Raise(this, nameof(RotateTransform), nameof(Rotation));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public TrackPiece(Point location, Size size, DrawingImage image, TrackType trackType)
    {
        X = (int)location.X;
        Y = (int)location.Y;
        Size = size;
        DrawingImage = image;
        TrackType = trackType;
        Rotate = new RotateTransform(0, Size.Width / 2, Size.Height / 2);
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) 
            return false;

        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}