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

            var plus = Geometry.Parse(pathPlus);
            var minus = Geometry.Parse(pathMinus);
            
            var viewModel = DataContext as MainFormViewModel;
            for (var i = 0; i < viewModel?.HorizontalPieceCount; i++)
            {
                for (var j = 0; j < viewModel?.VerticalPieceCount; j++)
                {
                    var customShape = new TrackPiece
                    {
                        Width = 100,
                        Height = 100,
                        Path = (i+j) % 2 == 0 ? plus : minus
                    };

                    MainView.Children.Add(customShape);

                    Canvas.SetLeft(customShape, i * 100);
                    Canvas.SetTop(customShape, j * 100);
                }
            }
        }

        private static string pathPlus = "M 10,100 C 100,0 200,200 300,100";

        private static string pathMinus =
            "M251,468.83c15.517,0,28.14-12.623,28.14-28.14s-12.623-28.14-28.14-28.14c-15.516,0-28.14,12.623-28.14,28.14    S235.484,468.83,251,468.83z M251,432.551c4.488,0,8.14,3.651,8.14,8.14s-3.651,8.14-8.14,8.14s-8.14-3.651-8.14-8.14    S246.512,432.551,251,432.551z";


    }
}