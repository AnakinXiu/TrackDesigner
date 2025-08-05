using TrackDesigner.Model;
using TrackDesigner.Model.Tracks;
using TrackDesigner.Tracks;
using TrackDesigner.ViewModels;

namespace TrackDesigner.Tools;

public class StraightTool : ITool
{
    public bool IsAvailable { get; private set; }

    public bool OnMouseClick(object sender, MouseFloatEventArgs args)
    {
        if (sender is not TrackPieceViewModel trackPiece)
            return false;

        if (trackPiece.TrackModel.TrackPieceType is not TrackPieceType.Straight)
            trackPiece.TrackModel = TrackModel.Straight;
        else
            trackPiece.Rotation = (RotateDegree)(((int)trackPiece.Rotation + 1) % 4);

        return true;
    }
}