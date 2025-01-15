using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TrackDesigner;

public class GridCanvas : Canvas
{
    protected override void OnRender(DrawingContext dc)
    {
        base.OnRender(dc);

        var gridPen = new Pen(Brushes.DarkGray, 2);

        var width = 6;
        var height = 6;

        for (var i = 1; i < width; i++) 
            dc.DrawLine(gridPen, new Point(i * 50, 0), new Point(i * 50, height * 50));

        for (var i = 1; i < height; i++)
            dc.DrawLine(gridPen, new Point(0, i * 50), new Point(width * 50, i * 50));
    }
}