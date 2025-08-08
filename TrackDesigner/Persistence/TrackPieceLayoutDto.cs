using TrackDesigner.Model;

namespace TrackDesigner.Persistence;

internal class TrackPieceLayoutDto
{
    public IEnumerable<TrackPieceDto> TrackPieces { get; private set; }

    public TrackPieceLayoutDto(TrackModel[][] trackPieces)
    {
        TrackPieces = trackPieces.SelectMany(
            (models, i) => models.Select(
                (model, j) => new TrackPieceDto
                {
                    Type = model.TrackPieceType,
                    X = j,
                    Y = i
                }));
    }
}