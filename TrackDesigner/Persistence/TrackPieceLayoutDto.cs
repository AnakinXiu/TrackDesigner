using TrackDesigner.Model;

namespace TrackDesigner.Persistence;

internal class TrackPieceLayoutDto
{
    public IEnumerable<TrackPieceDto> TrackPieces { get; private set; }

    public TrackPieceLayoutDto(TrackModel[, ] trackPieces)
    {
        TrackPieces = Enumerable.Range(0, trackPieces.GetLength(0))
            .SelectMany(i => Enumerable.Range(0, trackPieces.GetLength(1))
                .Select(j => new TrackPieceDto
                {
                    Type = trackPieces[i, j].TrackPieceType,
                    Orientation= trackPieces[i, j].TrackPieceOrientation,
                    X = j,
                    Y = i
                }));
    }
}