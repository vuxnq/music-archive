using MusicArchive.Domain.Models;

namespace MusicArchive.Domain.Services;

public interface ITrackService {
    List<Track> GetTracks();
    List<Track> GetUnapprovedTracks();
    List<Track> GetApprovedTracks();
    Track GetTrack(int id);
    void AddTrack(Track track);
}
