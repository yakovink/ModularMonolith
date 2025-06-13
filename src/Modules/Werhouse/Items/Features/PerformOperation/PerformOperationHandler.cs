using System;
using Werhouse.Data.Repositories;
using Werhouse.Items.Dtos;
using Werhouse.Items.Models;

namespace Werhouse.Items.Features.PerformOperation;

public record PerformOperationCommand(WerhouseItemHistoryDto ItemHistoryDto) : ICommand<GenericResult<Guid>>;

public class PerformOperationHandler(IWerhouseRepository repository) : ICommandHandler<PerformOperationCommand, GenericResult<Guid>>
{
    public async Task<GenericResult<Guid>> Handle(PerformOperationCommand request, CancellationToken cancellationToken)
    {
        if (request.ItemHistoryDto == null)
        {
            throw new ArgumentNullException(nameof(request.ItemHistoryDto), "Item history cannot be null");
        }

        WerhouseItem item = (await repository.GetItemsByCondition(false, i => i.Id == request.ItemHistoryDto.WerhouseItemId, cancellationToken)).SingleOrDefault() ?? throw new NotFoundException($"Item with id {request.ItemHistoryDto.WerhouseItemId} not found");
        WerhouseItemHistory itemHistory = WerhouseItemHistory.Create(
            request.ItemHistoryDto.In,
            request.ItemHistoryDto.Out,
            request.ItemHistoryDto.Operation,
            request.ItemHistoryDto.description,
            item
        );
        Guid itemHistoryId = await repository.PerformItemOperation(itemHistory, cancellationToken);
        return new GenericResult<Guid>(itemHistoryId);
    }
}
