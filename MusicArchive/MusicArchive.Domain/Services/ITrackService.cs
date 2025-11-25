using MusicArchive.Domain.Models;

namespace MusicArchive.Domain.Services;

public interface ITrackService {
    List<Track> GetTracks();
    Track GetTrack(int id);
    void AddTrack(Track track);
}
