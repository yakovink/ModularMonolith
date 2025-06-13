using System;
using Werhouse.Data.Repositories;
using Werhouse.Items.Dtos;
using Werhouse.Items.Models;

namespace Werhouse.Items.Features.SendItem;

public record SendItemCommand(WerhouseItemDto input) : GenericCommandRequest<WerhouseItemDto, GenericResult<bool>>(input);


public class SendItemHandler(IWerhouseRepository repository) : ICommandHandler<SendItemCommand, GenericResult<bool>>
{
    public async Task<GenericResult<bool>> Handle(SendItemCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request.input.id);
        ArgumentNullException.ThrowIfNull(request.input.InvoiceId);
        return new GenericResult<bool>(await repository.SendItem((Guid)request.input.id,(Guid)request.input.InvoiceId, cancellationToken));
    }
}
