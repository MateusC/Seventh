using Microsoft.EntityFrameworkCore;
using Seventh.DGuard.Domain.Entities;
using Seventh.DGuard.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Seventh.DGuard.Infra.Data.Sql.Repositories
{
    public class ServerRepository : Repository<Server>, IServerRepository
    {
        public ServerRepository(DGuardContext context) : base(context)
        {
        }

        public async Task<Server> AddAsync(Server newServer)
        {
            var dbServer = await _set.FindAsync(newServer.Id);

            if (dbServer is null)
            {
                var result = await _set.AddAsync(newServer);

                return result.Entity;
            }

            return newServer;
        }

        public Task<List<Server>> GetAll(bool trackEntities)
        {
            var query = _set
                    .Where(x => !EF.Property<bool>(x, "IsDeleted"));

            if (trackEntities)
                return query
                    .ToListAsync();

            return query
                .AsNoTracking()
                .ToListAsync();
        }

        public override void Update(Server entity)
        {
            base.Update(entity);

            foreach (var video in entity.Videos)
            {
                if (!_context.Videos.Any(x => x.Id == video.Id))
                    _context.Videos.Add(video);
            }
        }

        public Task<List<Server>> GetAllAsync()
        {
            return _set
                .Include(x => x.Videos)
                .Where(x => !EF.Property<bool>(x, "IsDeleted"))
                .ToListAsync();
        }

        public override bool Delete(Guid id)
        {
            var dbEntry = _set.Find(id);

            if (dbEntry is null)
                return false;

            _context.Entry(dbEntry).Property("IsDeleted").CurrentValue = true;

            return true;
        }

        public async Task<Server> GetAsync(Guid id, bool includeDeletedVideos = false)
        {
            return await _set
                .Include(x => x.Videos.Where(x => includeDeletedVideos || !EF.Property<bool>(x, "IsDeleted")))
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> RecoverAsync(Guid id)
        {
            var dbEntry = await _set.FindAsync(id);

            if (dbEntry is null)
                return false;

            _context.Entry(dbEntry).Property("IsDeleted").CurrentValue = false;

            return true;
        }

        public void RemoveVideo(Video currentVideo)
        {
            _context.Entry(currentVideo).Property("IsDeleted").CurrentValue = true;
        }

        public void RecoverVideo(Video currentVideo)
        {
            _context.Entry(currentVideo).Property("IsDeleted").CurrentValue = false;
        }
    }
}
