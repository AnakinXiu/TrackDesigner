using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using TrackDesigner.Model;
using TrackDesigner.Persistence;
using TrackDesigner.Tools;
using TrackDesigner.Util;
using MessageBox = System.Windows.MessageBox;

namespace TrackDesigner.ViewModels;

public class RibbonViewModel : INotifyPropertyChanged
{
    private readonly Action<ITool> _setCurrentTool;

    private const string TrackDesignFileFilterString =
        $"{ProjectPersistence.ProjectFileNameString} | *.{ProjectPersistence.ProjectFileExtension}";

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

    public ICommand NewDesignCommand { get; }

    public ICommand OpenDesignCommand { get; }

    public ICommand SaveDesignCommand { get; }

    public ICommand SaveAsCommand { get; }

    public ICommand PrintCommand { get; }

    public ICommand ExitCommand { get; }

    public ICommand CornerCommand { get; }

    public ICommand StraightCommand { get; }
    
    public ICommand ApexCommand { get; }

    public event PropertyChangedEventHandler? PropertyChanged;

    [DesignOnly(true)]
    public RibbonViewModel() {}

    public RibbonViewModel(Action<ITool> setCurrentTool)
    {
        _setCurrentTool = setCurrentTool;
        NewDesignCommand = new RelayCommand(CreateNewDesign);
        OpenDesignCommand = new RelayCommand(OpenNewDesign);
        SaveDesignCommand = new RelayCommand(SaveDesign);
        SaveAsCommand = new RelayCommand(SaveDesignAs);
        PrintCommand = new RelayCommand(PrintDesign);
        ExitCommand = new RelayCommand<Window>(Exit);
        CornerCommand = new RelayCommand(ActiveCornerTool);
        StraightCommand = new RelayCommand(ActiveStraightTool);
        ApexCommand = new RelayCommand(ActiveApexTool);
    }

    private void ActiveApexTool()
    {
        _setCurrentTool(new ApexTool());
    }

    private void ActiveCornerTool()
    {
        _setCurrentTool(new CornerTool());
    }

    private void ActiveStraightTool()
    {
        _setCurrentTool(new StraightTool());
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
        if (TrackDesignProject.Instance is null)
        {
            MessageBox.Show("No project to save. Please create a new design project first.");
            return;
        }

        var saveFileDialog = new SaveFileDialog
        {
            CreatePrompt = true,
            AddExtension = true,
            CheckFileExists = true,
            CheckPathExists = true,
            DefaultExt = ProjectPersistence.ProjectFileExtension,
            Filter = TrackDesignFileFilterString,
            FilterIndex = 1,
        };

        var dialogResult = saveFileDialog.ShowDialog();
        if (dialogResult != DialogResult.OK)
            return;

        // Enforce the expected extension even if the user types a different one.
        var requiredExt = "." + ProjectPersistence.ProjectFileExtension.TrimStart('.');
        if (!saveFileDialog.FileName.EndsWith(requiredExt, StringComparison.OrdinalIgnoreCase))
        {
            saveFileDialog.FileName = Path.ChangeExtension(saveFileDialog.FileName, requiredExt);
        }

        using var stream = saveFileDialog.OpenFile();
        ProjectPersistence.SaveProject(TrackDesignProject.Instance, stream);
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