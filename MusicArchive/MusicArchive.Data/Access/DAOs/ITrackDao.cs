using MusicArchive.Data.Models;

namespace MusicArchive.Data;

public interface ITrackDao {
    List<Track> GetTracks();
    Track GetTrack(int id);
    void InsertTrack(Track track);
    void UpdateTrack(Track track);
    void DeleteTrack(int id);
}
