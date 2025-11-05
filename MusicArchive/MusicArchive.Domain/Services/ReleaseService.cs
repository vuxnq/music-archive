using MusicArchive.Data;
using MusicArchive.Domain.Mappers;
using MusicArchive.Domain.Models;

namespace MusicArchive.Domain.Services;

public class ReleaseService(IDataConnector connector) : IReleaseService {
    public List<Release> GetReleases() {
        return connector.CreateReleaseDao().GetReleases().ToDomain();
    }

    public void AddRelease(Release release) {
        var data = release.ToData();
        connector.CreateReleaseDao().AddRelease(data);
        release.Id = data.Id;
    }
}