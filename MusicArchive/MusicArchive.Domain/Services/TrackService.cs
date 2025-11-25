using MusicArchive.Data;
using MusicArchive.Domain.Mappers;
using MusicArchive.Domain.Models;

namespace MusicArchive.Domain.Services;

public class TrackService(IDataConnector connector) : ITrackService {
    private readonly ITrackDao _trackDao = connector.CreateTrackDao();
    private readonly IReleaseDao _releaseDao = connector.CreateReleaseDao();

    public List<Track> GetTracks() {
        return _trackDao.GetTracks().ToDomain();
    }

    public Track GetTrack(int id) {
        var track = _trackDao.GetTrack(id).ToDomain();
        track.Release = _releaseDao.GetRelease(track.ReleaseId).ToDomain();
        return track;
    }

    public void AddTrack(Track track) {
        var data = track.ToData();
        _trackDao.AddTrack(data);
        track.Id = data.Id;
    }
}
