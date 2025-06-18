
namespace Werhouse.Items.Features.SendItem;


public class SendItemHandler(IWerhouseRepository repository) : WerhouseModuleStructre.SendItem.IMEndpointPutHandler
{
    public async Task<GenericResult<bool>> Handle(WerhouseModuleStructre.SendItem.Command request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request.input.id);
        ArgumentNullException.ThrowIfNull(request.input.InvoiceId);
        return new GenericResult<bool>(await repository.SendItem((Guid)request.input.id,(Guid)request.input.InvoiceId, cancellationToken));
    }
}
