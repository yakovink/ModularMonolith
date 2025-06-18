
namespace Werhouse.Items.Features.GetItemById;






public class GetItemByIdHandler(IWerhouseRepository repository) : WerhouseModuleStructre.GetItemById.IMEndpointGetHandler
{
    public async Task<GenericResult<HashSet<WerhouseItemHistoryDto>>> Handle(WerhouseModuleStructre.GetItemById.Query request, CancellationToken cancellationToken)
    {
        IEnumerable<WerhouseItemHistoryDto> itemHistory = (await repository.GetItemHistory(request.input, true, cancellationToken)).Select(i=>i.ToDto());
        Console.WriteLine($"Item history count for item {request.input}: {itemHistory.Count()}");
        
        return new GenericResult<HashSet<WerhouseItemHistoryDto>>(itemHistory.ToHashSet());

    }


}
