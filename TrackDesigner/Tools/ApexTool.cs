using TrackDesigner.Model;
using TrackDesigner.Util;
using TrackDesigner.ViewModels;

namespace TrackDesigner.Tools;

public class ApexTool : ITool
{
    public bool IsAvailable { get; private set; }
    public bool OnMouseClick(object sender, MouseFloatEventArgs args)
    {
        if (sender is not TrackPieceViewModel trackPiece)
            return false;

        if (trackPiece.TrackModel.TrackPieceType is not TrackPieceType.Apex)
            trackPiece.TrackModel = TrackModel.Apex;
        else
            trackPiece.Orientation = trackPiece.Orientation.Next();

        return true;
    }
}