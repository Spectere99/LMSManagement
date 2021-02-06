using System.Collections.Generic;
using System.Web;

namespace LMSDataService.Helpers
{
    public interface IFileImportHelper
    {
        List<string> Import(HttpFileCollection files, string saveFolder, string user, string hostIP);
    }
}