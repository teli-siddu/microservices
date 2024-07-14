
namespace BuildingBlocks.Pagination;
    public class PaginatedResult<TEntity>
    (int pageIndex, int pageSize, long count, IEnumerable<TEntity> data)
    {
    public int PageIndex { get; } = pageIndex;
    public int PageSizwe { get; } = pageSize;
    public long Count { get; } = count;
    public IEnumerable<TEntity> Data { get; } = data;
}

