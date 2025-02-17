using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using TrackDesigner.Util;

namespace TrackDesigner;

public class MainFormViewModel : INotifyPropertyChanged
{
    private int _horizontalPieceCount;
    private int _verticalPieceCount;

    public int HorizontalPieceCount
    {
        get => _horizontalPieceCount;
        set => PropertyChanged?.RaiseIfChanged(this, ref _horizontalPieceCount, value, nameof(HorizontalPieceCount));
    }

    public int VerticalPieceCount
    {
        get => _verticalPieceCount;
        set => PropertyChanged.RaiseIfChanged(this, ref _verticalPieceCount, value, nameof(VerticalPieceCount));
    }

    public ObservableCollection<TrackPiece> TrackPieces { get; set; } = [];

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

public class TrackPiece : INotifyPropertyChanged
{
    public int X { get; set; }

    public int Y { get; set; }

    public Size Size { get; set; }

    public DrawingImage DrawingImage { get; set; }

    public RotateTransform Rotate { get; } = new();

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

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}