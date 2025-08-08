using TrackDesigner.Persistence;

namespace TrackDesigner.Model;

public class TrackDesignProject
{
    public ProjectInfo ProjectInfo { get; set; }

    public TrackModel[, ] TrackPieces { get; set; }

    internal TrackDesignProject(ProjectInfo projectInfo, TrackPieceLayoutDto tackPieceDto)
    {
        ProjectInfo = projectInfo;
        TrackPieces = new TrackModel[projectInfo.Width, projectInfo.Height];

        foreach (var trackPiece in tackPieceDto.TrackPieces)
        {
            
        }
    }
}