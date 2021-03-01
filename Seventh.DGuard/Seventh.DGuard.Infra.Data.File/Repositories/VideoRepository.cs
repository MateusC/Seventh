using Microsoft.Extensions.Options;
using Seventh.DGuard.Domain.Repositories;
using Seventh.DGuard.Infra.Data.File.Options;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Seventh.DGuard.Infra.Data.File.Repositories
{
    public class VideoRepository : IVideoRepository
    {
        private readonly FileRepositoryOptions _options;

        public VideoRepository(IOptionsMonitor<FileRepositoryOptions> options)
        {
            _options = options.CurrentValue;
        }

        public String GetContent(String path)
        {
            if (System.IO.File.Exists(path))
                return System.IO.File.ReadAllText(path);

            return null;
        }

        public Task RemoveContent(String path)
        {
            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            return Task.CompletedTask;
        }

        public async Task<String> SaveContent(String content)
        {
            if (!Directory.Exists(_options.Path))
            {
                var info = Directory.CreateDirectory(_options.Path);

                if (!info.Exists)
                    throw new InvalidOperationException("Não foi possível criar o diretório para salvar os arquivos de vídeos.");
            }

            var filePath = $"{_options.Path}/{Guid.NewGuid()}.txt";

            Byte[] encodedText = Encoding.UTF8.GetBytes(content);

            using (FileStream sourceStream = new FileStream(filePath,
                FileMode.Append, FileAccess.Write, FileShare.None,
                bufferSize: 4096, useAsync: true))
            {
                await sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
            };

            return filePath;
        }
    }
}
