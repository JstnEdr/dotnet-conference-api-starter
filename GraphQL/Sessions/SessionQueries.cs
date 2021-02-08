using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ConferencePlanner.GraphQL.Data;
using ConferencePlanner.GraphQL.DataLoader;
using ConferencePlanner.GraphQL.Types;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.GraphQL.Sessions
{
  [ExtendObjectType(Name = "Query")]
  public class SessionQueries
  {
    [UseApplicationDbContext]
    [UsePaging(typeof(NonNullType<SessionType>))]
    public IQueryable<Session> GetSessions(
        [ScopedService] ApplicationDbContext context) =>
        context.Sessions;

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