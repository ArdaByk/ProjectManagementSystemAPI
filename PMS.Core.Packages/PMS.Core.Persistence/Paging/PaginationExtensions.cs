using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Core.Persistence.Paging;

public static class PaginationExtensions
{
    public static async Task<Paginate<T>> ToPaginateAsync<T>(
        this IQueryable<T> source,
        int size,
        int index,
        CancellationToken cancellationToken= default)
    {
        int count = await source.CountAsync(cancellationToken).ConfigureAwait(false);

        List<T> items = await source.Skip(index*size).Take(size).ToListAsync(cancellationToken).ConfigureAwait(false);

        Paginate<T> paginate = new()
        {
            Items = items,
            Count = count,
            Index = index,
            Pages = (int)Math.Ceiling(count / (double)size),
            Size = size
            
        };

        return paginate;
    }

    public static Paginate<T> ToPaginate<T>(
        this IQueryable<T> source,
        int size,
        int index)
    {
        int count = source.Count();

        List<T> items = source.Skip(index * size).Take(size).ToList();

        Paginate<T> paginate = new()
        {
            Items = items,
            Count = count,
            Index = index,
            Pages = (int)Math.Ceiling(count / (double)size),
            Size = size

        };

        return paginate;
    }
}
