using System;

namespace Seventh.DGuard.Infra.Data.File.Options
{
    public class FileRepositoryOptions
    {
        public FileRepositoryOptions()
        {

        }

        public FileRepositoryOptions(String path)
        {
            Path = path;
        }

        public String Path { get; set; }
    }
}
