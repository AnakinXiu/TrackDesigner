using System.Windows;
using System.Windows.Controls.Ribbon;

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

            var customShape = new TrackPiece
            {
                Location = new Point(100, 100),
                Width = 100,
                Height = 100
            };
            MainView.Children.Add(customShape);
        }
    }
}