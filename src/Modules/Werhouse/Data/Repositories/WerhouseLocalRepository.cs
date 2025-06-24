


namespace Werhouse.Data.Repositories;

public class WerhouseLocalRepository(IGenericRepository<WerhouseItem> repository, IHttpController controller) : WerhouseModuleStructre.WerhouseRepository(repository), IWerhouseRepository
{
    
    public async Task<IEnumerable<WerhouseItemHistory>> GetItemHistory(Guid itemId, bool AsNoTracking = true, CancellationToken cancellationToken = default)
    {
        WerhouseItem items = await repository.GetElementById(itemId, AsNoTracking, cancellationToken, c => c.checkpoints);
        
        return items.checkpoints;
    }
    public async Task<IEnumerable<WerhouseItem>> GetItemsByCondition(bool AsNoTracking, Expression<Func<WerhouseItem, bool>> condition = default!, CancellationToken cancellationToken = default)
    {

        return await repository.GetElements(condition, AsNoTracking, cancellationToken, c => c.checkpoints);
    }

    public async Task<WerhouseItem> GetNewItem(Guid productId, CancellationToken cancellationToken = default)
    {
        JsonElement productElement = await controller.Get($"products/get?input={productId}",cancellationToken);
        if (productElement.ValueKind == JsonValueKind.Null)
        {
            throw new ProductNotFoundException(productId);
        }
        Console.WriteLine(productElement.ToString());


        WerhouseItem newItem = WerhouseItem.Create(productId);

        await repository.CreateElement(newItem, cancellationToken);
        // Reload the item as Tracked item
        newItem = await repository.GetElementById(newItem.Id, false, cancellationToken, c => c.checkpoints);
        // Create a history entry for the new item
        WerhouseItemHistory history = WerhouseItemHistory.Create(null, 1, "Get In", $"New item created from ProductId {productId}", newItem);
        // Add the history entry to the item
        await PerformItemOperation(history, cancellationToken);
        return newItem;
        
    }

    public async Task<Guid> PerformItemOperation(WerhouseItemHistory itemHistory, CancellationToken cancellationToken = default)
    {
        itemHistory = await repository.AddElementToCollection(itemHistory,
         h => h.WerhouseItemId,
          h => h.Id == itemHistory.Id,
           h => h
           , cancellationToken);
        // Reload the item to get the updated checkpoints
        WerhouseItem item = await repository.GetElementById(itemHistory.WerhouseItemId, false, cancellationToken, c => c.checkpoints);
        item.UpdateWerhouse();
        await repository.SaveChangesAsync(item.Id, cancellationToken);
        return itemHistory.Id;
    }

    public async Task<bool> ReloadItems(WerhouseItem item, CancellationToken cancellationToken = default)
    {
        return await repository.ReloadElementCollection(item, c => c.checkpoints, cancellationToken);
    }

    public async Task<bool> SendItem(Guid ItemId, Guid InvoiceId, CancellationToken cancellationToken = default)
    {

        WerhouseItem item = (await GetItemsByCondition(false, i => i.Id == ItemId, cancellationToken)).SingleOrDefault() 
                            ?? throw new NotFoundException($"Item with id {ItemId} not found");

        if (item.Werhouse == null || item.InvoiceId !=null)
        {
            throw new ArgumentNullException(nameof(item.Werhouse), "Item was already being sold");
        }
        item.InvoiceId = InvoiceId;
        WerhouseItemHistory history = WerhouseItemHistory.Create(item.Werhouse, null, "Send", $"Item {item.Id} sent", item);
        await PerformItemOperation(history, cancellationToken);

        return true;


    }
}
