using System;
using Shared.Data;
using Werhouse.Items.Models;

namespace Werhouse.Data.Repositories;

public interface IWerhouseRepository : IGenericRepository<WerhouseItem>
{
    public Task<IEnumerable<WerhouseItemHistory>> GetItemHistory(Guid itemId, bool AsNoTracking=true,CancellationToken cancellationToken = default);
    public Task<Guid> PerformItemOperation(WerhouseItemHistory itemHistory, CancellationToken cancellationToken = default);
    public Task<WerhouseItem> GetNewItem(Guid productId, CancellationToken cancellationToken = default);
    public Task<bool> SendItem(Guid ItemId, Guid InvoiceId, CancellationToken cancellationToken = default);
    public Task<IEnumerable<WerhouseItem>> GetItemsByCondition(bool AsNoTracking, Expression<Func<WerhouseItem, bool>> condition, CancellationToken cancellationToken = default);
    public Task<bool> ReloadItems(WerhouseItem item, CancellationToken cancellationToken = default);




}
