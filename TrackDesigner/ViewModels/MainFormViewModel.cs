using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TrackDesigner.Tools;

namespace TrackDesigner.ViewModels;

public class MainFormViewModel : INotifyPropertyChanged
{
    public RibbonViewModel RibbonViewModel { get; set; }

    public ObservableCollection<TrackPiece> TrackPieces { get; set; } = [];

    public ITool CurrentTool { get; set; }

    public MainFormViewModel()
    {
        RibbonViewModel = new RibbonViewModel(SetCurrentTool);
        RibbonViewModel.PropertyChanged += OnRibbonViewModelPropertyChanged;
    }

    private void OnRibbonViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        OnPropertyChanged(e.PropertyName);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void SetCurrentTool(ITool tool)
    {
        CurrentTool = tool;
    }
}