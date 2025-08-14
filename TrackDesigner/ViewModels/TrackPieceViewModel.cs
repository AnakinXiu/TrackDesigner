using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using TrackDesigner.Asset;
using TrackDesigner.Model;
using TrackDesigner.Util;

namespace TrackDesigner.ViewModels;

public class TrackPieceViewModel : INotifyPropertyChanged
{
    private TrackModel _trackModel;
    private readonly TrackPieceImageRepository _imageRepository;

    public int X { get; set; }

    public int Y { get; set; }

    public Size Size { get; set; }

    public DrawingImage DrawingImage => _imageRepository.GetImage(_trackModel.TrackPieceType);

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

    public TrackPieceOrientation Orientation
    {
        get => _trackModel.Orientation;
        set
        {
            _trackModel.Orientation = value;
            Rotate.Angle = value.ToDegree();
            PropertyChanged?.Raise(this, nameof(Rotate), nameof(Orientation));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public TrackPieceViewModel(Point location, Size size, TrackPieceImageRepository imageRepository)
    {
        X = (int)location.X;
        Y = (int)location.Y;
        Size = size;
        _imageRepository = imageRepository ?? throw new ArgumentNullException(nameof(imageRepository));
        TrackModel = new TrackModel(TrackPieceType.None);
        Rotate = new RotateTransform(0, (Size.Width - 2) / 2, (Size.Height - 2) / 2);
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}