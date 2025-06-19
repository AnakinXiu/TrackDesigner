namespace TrackDesigner.Tools;

public interface ITool
{
    bool IsAvailable { get; }

    bool OnMouseClick(object sender, MouseFloatEventArgs args);
}