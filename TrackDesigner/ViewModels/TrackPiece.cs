using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using TrackDesigner.Tracks;
using TrackDesigner.Util;

namespace TrackDesigner.ViewModels;

public class TrackPiece : INotifyPropertyChanged
{
    private TrackModel _trackModel;
    public int X { get; set; }

    public int Y { get; set; }

    public Size Size { get; set; }

    public DrawingImage DrawingImage => _trackModel.TrackImage;

    public TrackModel TrackModel
    {
        get => _trackModel;
        set
        {
            _trackModel = value;
            PropertyChanged?.Raise(this, nameof(TrackModel), nameof(DrawingImage));
        }
    }

    public RotateTransform Rotate { get; }

    public RotateDegree Rotation
    {
        get => Rotate.Angle.ToEnum();
        set
        {
            Rotate.Angle = value.ToDegree();
            PropertyChanged?.Raise(this, nameof(Rotate), nameof(Rotation));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public TrackPiece(Point location, Size size)
    {
        X = (int)location.X;
        Y = (int)location.Y;
        Size = size;
        TrackModel = TrackModel.GetTrackModel(TrackType.None);
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