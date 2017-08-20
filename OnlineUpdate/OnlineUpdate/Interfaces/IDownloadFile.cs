
namespace OnlineUpdate
{
    public interface IDownloadFile
    {
        bool DownloadFile(string urlFileDescriptione, string _patchFileDescriptione);

        bool TryDownloadFile(string urlSource, string _patchFile);
    }
}
