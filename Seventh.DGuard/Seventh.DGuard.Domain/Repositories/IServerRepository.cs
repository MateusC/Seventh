using Seventh.DGuard.Domain.Core;
using Seventh.DGuard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Seventh.DGuard.Domain.Repositories
{
    public interface IServerRepository : IRepository<Server>
    {
        Task<Server> AddAsync(Server newServer);

        Task<Server> GetAsync(Guid id, bool includeDeletedVideos = false);

        Task<List<Server>> GetAll(bool trackEntities);
        
        Task<List<Server>> GetAllAsync();

        Task<bool> RecoverAsync(Guid id);

        void RemoveVideo(Video currentVideo);

        void RecoverVideo(Video currentVideo);
    }
}
