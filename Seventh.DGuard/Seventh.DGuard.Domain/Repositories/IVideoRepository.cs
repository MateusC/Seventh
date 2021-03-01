using System;
using System.Threading.Tasks;

namespace Seventh.DGuard.Domain.Repositories
{
    public interface IVideoRepository
    {
        Task<String> SaveContent(String content);

        Task RemoveContent(String path);
        
        String GetContent(String path);
    }
}
