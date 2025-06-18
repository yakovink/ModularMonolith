
namespace Werhouse.Items.Features.PerformOperation;


public class PerformOperationHandler(IWerhouseRepository repository) : WerhouseModuleStructre.PerformOperation.IMEndpointPostHandler
{
    public async Task<GenericResult<Guid>> Handle(WerhouseModuleStructre.PerformOperation.Command request, CancellationToken cancellationToken)
    {
        if (request.input == null)
        {
            throw new ArgumentNullException(nameof(request.input), "Item history cannot be null");
        }

        WerhouseItem item = (await repository.GetItemsByCondition(false, i => i.Id == request.input.WerhouseItemId, cancellationToken)).SingleOrDefault() ?? throw new NotFoundException($"Item with id {request.input.WerhouseItemId} not found");
        WerhouseItemHistory itemHistory = WerhouseItemHistory.Create(
            request.input.In,
            request.input.Out,
            request.input.Operation,
            request.input.description,
            item
        );
        Guid itemHistoryId = await repository.PerformItemOperation(itemHistory, cancellationToken);
        return new GenericResult<Guid>(itemHistoryId);
    }
}
