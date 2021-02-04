using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ConferencePlanner.GraphQL.Data;
using HotChocolate;
using ConferencePlanner.GraphQL.DataLoader;
using System.Threading;

namespace ConferencePlanner.GraphQL
{
  public class Query
  {
    [UseApplicationDbContext]
    public Task<List<Speaker>> GetSpeakers([ScopedService] ApplicationDbContext context) =>
        context.Speakers.ToListAsync();
    public Task<Speaker> GetSpeakerAsync(
      int id,
      SpeakerByIdDataLoader dataLoader,
      CancellationToken cancellationToken) =>
      dataLoader.LoadAsync(id, cancellationToken);
  }
}