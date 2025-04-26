using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using TrackDesigner.Controls;
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

            var image = FindResource("SvgDrawingImage") as DrawingImage;
            for (var i = 0; i < viewModel?.RibbonViewModel.HorizontalPieceCount; i++)
            {
                for (var j = 0; j < viewModel?.RibbonViewModel.VerticalPieceCount; j++)
                {
                    var customShape = new TrackPiece
                    {
                        X = i * 100, 
                        Y = j * 100,
                        Size = new Size(100, 100),
                        DrawingImage = image
                    };

                    viewModel.TrackPieces.Add(customShape);
                }
            }
        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is not MainFormViewModel viewModel)
                return;

            if (sender is not TrackPieceControl trackPiece)
                return;

            viewModel.TrackPieces.Where(piece => piece.Equals(trackPiece));
        }
    }
}