using System.Windows;
using System.Windows.Media;

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
            if (e.PropertyName is not (nameof(MainFormViewModel.HorizontalPieceCount)
                or nameof(MainFormViewModel.VerticalPieceCount))) 
                return;

            if (DataContext is not MainFormViewModel viewModel)
                return;

            viewModel.TrackPieces.Clear();

            var image = FindResource("SvgDrawingImage") as DrawingImage;
            for (var i = 0; i < viewModel?.HorizontalPieceCount; i++)
            {
                for (var j = 0; j < viewModel?.VerticalPieceCount; j++)
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
    }
}