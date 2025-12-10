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

    public List<Track> GetUnapprovedTracks() {
        return GetTracks().Where(r => !r.IsApproved).ToList();
    }

    public List<Track> GetApprovedTracks() {
        return GetTracks().Where(r => r.IsApproved).ToList();
    }

    public Track GetTrack(int id) {
        var track = _trackDao.GetTrack(id).ToDomain();
        track.Release = _releaseDao.GetRelease(track.ReleaseId).ToDomain();
        return track;
    }

    public void AddTrack(Track track) {
        connector.BeginTransaction();
        try {
            var data = track.ToData();
            _trackDao.InsertTrack(data);
            track.Id = data.Id;

            connector.Commit();
        } catch {
            connector.Rollback();
            throw;
        }
    }

    public void ApproveTrack(Track track) {
        connector.BeginTransaction();
        try {
            track.IsApproved = true;
            var data = track.ToData();
            _trackDao.UpdateTrack(data);

            connector.Commit();
        } catch {
            connector.Rollback();
            throw;
        }
    }

    public void RejectTrack(Track track) {
        connector.BeginTransaction();
        try {
            _trackDao.DeleteTrack(track.Id);
            connector.Commit();
        } catch {
            connector.Rollback();
            throw;
        }
    }
}
