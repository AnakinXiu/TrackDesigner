using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Media;

namespace TrackDesigner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
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

            MainView.Children.Clear();

            var viewModel = DataContext as MainFormViewModel;
            var image = FindResource("SvgDrawingImage") as DrawingImage;
            for (var i = 0; i < viewModel?.HorizontalPieceCount; i++)
            {
                for (var j = 0; j < viewModel?.VerticalPieceCount; j++)
                {
                    var customShape = new TrackPiece
                    {
                        Width = 100,
                        Height = 100,
                        DrawingImage = image
                    };

                    MainView.Children.Add(customShape);

                    Canvas.SetLeft(customShape, i * 100);
                    Canvas.SetTop(customShape, j * 100);
                }
            }
        }
    }
}