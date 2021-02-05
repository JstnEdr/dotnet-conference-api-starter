using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ConferencePlanner.GraphQL.Data;
using ConferencePlanner.GraphQL.DataLoader;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.GraphQL.Sessions
{
  [ExtendObjectType(Name = "Query")]
  public class SessionQueries
  {
    public async Task<IEnumerable<Session>> GetSessionsAsync(
      [ScopedService] ApplicationDbContext context,
      CancellationToken cancellationToken) =>
      await context.Sessions.ToListAsync(cancellationToken);

    public Task<Session> GetSessionByIdAsync(
      [ID(nameof(Session))] int id,
      SessionByIdDataLoader sessionById,
      CancellationToken cancellationToken) =>
      sessionById.LoadAsync(id, cancellationToken);

    public async Task<IEnumerable<Session>> GetSessionByIdAsync(
      [ID(nameof(Session))] int[] ids,
      SessionByIdDataLoader sessionById,
      CancellationToken cancellationToken) =>
      await sessionById.LoadAsync(ids, cancellationToken);
  }
}