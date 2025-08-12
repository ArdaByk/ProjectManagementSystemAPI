using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Core.Persistence.Paging;

public class Paginate<T>
{
    public int Size { get; set; }
    public int Index { get; set; }
    public int Pages { get; set; }
    public int Count { get; set; }
    public IList<T> Items { get; set; }
    public bool HasNext => Index + 1 < Pages;
    public bool HasPrevious => Index > 0;

    public Paginate()
    {
        Items = Array.Empty<T>();
    }
}
