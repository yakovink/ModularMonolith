 

namespace Werhouse.Items.Features.GetNewItem;
public class GetNewItemHandler(IWerhouseRepository repository) : WerhouseModuleStructre.GetNewItem.IMEndpointPostHandler
{
    public async Task<GenericResult<Guid>> Handle(WerhouseModuleStructre.GetNewItem.Command request, CancellationToken cancellationToken)
    {
        return new GenericResult<Guid>((await repository.GetNewItem(request.input, cancellationToken)).Id);
    }
}
