using System;
using Werhouse.Data.Repositories;
using Werhouse.Items.Models;

namespace Werhouse.Items.Features.GetNewItem;

public record GetNewItemCommand(Guid input) : ICommand<GenericResult<Guid>>;

public class GetNewItemHandler(IWerhouseRepository repository) : ICommandHandler<GetNewItemCommand, GenericResult<Guid>>
{
    public async Task<GenericResult<Guid>> Handle(GetNewItemCommand request, CancellationToken cancellationToken)
    {
        return new GenericResult<Guid>((await repository.GetNewItem(request.input, cancellationToken)).Id);
    }
}
