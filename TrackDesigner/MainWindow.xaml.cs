using System.Windows;
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

            var customShape = new TrackPiece
            {
                Location = new Point(100, 100),
                Width = 100,
                Height = 100
            };
            MainView.Children.Add(customShape);
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
                        Location = new Point(i * 100, j * 100),
                        Width = 100,
                        Height = 100
                    };
                    
                    MainView.Children.Add(customShape);
                }
            }
        }

        private static string pathPlus =
            "M394.345,407.354h-27.776v-27.776c0-5.523-4.478-10-10-10s-10,4.477-10,10v27.776h-27.776c-5.522,0-10,4.477-10,10    s4.478,10,10,10h27.776v27.776c0,5.523,4.478,10,10,10s10-4.477,10-10v-27.776h27.776c5.522,0,10-4.477,10-10    S399.868,407.354,394.345,407.354z";

        private static string pathMinus =
            "M251,468.83c15.517,0,28.14-12.623,28.14-28.14s-12.623-28.14-28.14-28.14c-15.516,0-28.14,12.623-28.14,28.14    S235.484,468.83,251,468.83z M251,432.551c4.488,0,8.14,3.651,8.14,8.14s-3.651,8.14-8.14,8.14s-8.14-3.651-8.14-8.14    S246.512,432.551,251,432.551z";


    }
}