using System.Windows;
using System.Windows.Input;
using TrackDesigner.Controls;
using TrackDesigner.Tools;
using TrackDesigner.ViewModels;

namespace TrackDesigner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            var viewModel = new MainFormViewModel();
            viewModel.PropertyChanged += OnViewModelPropertyChanged;
            DataContext = viewModel;
        }

        private void OnViewModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName is not (nameof(RibbonViewModel.HorizontalPieceCount)
                or nameof(RibbonViewModel.VerticalPieceCount))) 
                return;

            if (DataContext is not MainFormViewModel viewModel)
                return;

            viewModel.TrackPieces.Clear();

            for (var i = 0; i < viewModel?.RibbonViewModel.HorizontalPieceCount; i++)
            {
                for (var j = 0; j < viewModel?.RibbonViewModel.VerticalPieceCount; j++)
                {
                    var customShape = new TrackPiece(new Point(i * 100, j * 100), new Size(100, 100));
                    viewModel.TrackPieces.Add(customShape);
                }
            }
        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is not MainFormViewModel viewModel)
                return;

            if (sender is not TrackPieceControl { DataContext: TrackPiece trackPiece })
                return;

            if(!viewModel.TrackPieces.Contains(trackPiece))
                return;

            viewModel.CurrentTool?.OnMouseClick(trackPiece,
                new MouseFloatEventArgs(e.ChangedButton, e.ClickCount, trackPiece.X, trackPiece.Y, 0));
        }
    }
}