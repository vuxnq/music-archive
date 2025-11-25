using MusicArchive.Data.Models;

namespace MusicArchive.Data;

public interface ITrackDao {
    List<Track> GetTracks();
    Track GetTrack(int id);
    void AddTrack(Track track);
}
