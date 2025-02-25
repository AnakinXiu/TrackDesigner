using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using TrackDesigner.Util;
using MessageBox = System.Windows.MessageBox;

namespace TrackDesigner.ViewModels;

public class RibbonViewModel : INotifyPropertyChanged
{
    private const string TrackDesignFileFilterString = "Track Design files | *.tdn";

    public ICommand NewDesignCommand { get; }

    public ICommand OpenDesignCommand { get; }

    public ICommand SaveDesignCommand { get; }

    public ICommand SaveAsCommand { get; }

    public ICommand PrintCommand { get; }

    public ICommand ExitCommand { get; }

    public event PropertyChangedEventHandler? PropertyChanged;

    public RibbonViewModel()
    {
        NewDesignCommand = new RelayCommand(CreateNewDesign);
        OpenDesignCommand = new RelayCommand(OpenNewDesign);
        SaveDesignCommand = new RelayCommand(SaveDesign);
        SaveAsCommand = new RelayCommand(SaveDesignAs);
        PrintCommand = new RelayCommand(PrintDesign);
        ExitCommand = new RelayCommand<Window>(Exit);
    }

    private void CreateNewDesign()
    {

    }

    private void OpenNewDesign()
    {
        var dialogResult = new OpenFileDialog
        {
            Filter = TrackDesignFileFilterString
        }.ShowDialog();
    }

    private void SaveDesignAs()
    {
        var dialog = new SaveFileDialog
        {
            Filter = TrackDesignFileFilterString
        };

        var dialogResult = dialog.ShowDialog();

        if (dialogResult == DialogResult.OK)
            MessageBox.Show($"Save design to {dialog.FileName}");
    }

    private void SaveDesign()
    {
    }

    private void PrintDesign()
    {
        
    }

    private void Exit(Window mainWindow)
    {
        mainWindow?.Close();
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}