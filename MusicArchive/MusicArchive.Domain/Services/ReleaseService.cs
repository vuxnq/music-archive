using MusicArchive.Data;
using MusicArchive.Domain.Mappers;
using MusicArchive.Domain.Models;

namespace MusicArchive.Domain.Services;

public class ReleaseService(IDataConnector connector) : IReleaseService {
    public List<Release> GetReleases() {
        return connector.CreateReleaseDao().GetReleases().ToDomain();
    }
}