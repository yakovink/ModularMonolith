using System;
using Shared.Exceptions;

namespace Catalog.Features.UpdateProduct;
public record UpdateProductCommand
    (ProductDto input)
    : ICommand<UpdateProductResult>;


public record UpdateProductResult(bool isUpdated): GenericResult<bool>(isUpdated);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.input)
            .NotNull()
            .WithMessage("Product cannot be null");
        RuleFor(x => x.input.Id)
            .NotEmpty()
            .WithMessage("Id cannot be empty");
        RuleFor(x => x.input)
            .Must(x => x.Name != null || x.Price != null || x.Description != null || x.ImageUrl != null || x.Categories != null)
            .WithMessage("At least one of Name, Price, Description or ImageUrl must be provided");

    }
}

internal class UpdateProductHandler(CatalogDbContext dbContext) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{

    public async Task<UpdateProductResult> Handle(UpdateProductCommand command,
                  CancellationToken cancellationToken)
    {
        //get the product entity
        if (command.input.Id == null)
        {
            throw new InternalServerException("Generic validator cant catch the null product in request");
        }

        Guid id = (Guid)command.input.Id;


        Product product = await dbContext.getProductById(id, cancellationToken, RequestType.Command);

        if (product == null)
        {
            throw new ProductNotFoundException(id);
        }
        //update the product
        UpdateProduct(product, command.input);
        //save to db
        await dbContext.SaveChangesAsync(cancellationToken);
        //validate
        bool success = await validate(product, cancellationToken);
        //return the result
        return new UpdateProductResult(success);
    }

    private void UpdateProduct(Product product, ProductDto productDto)
    {


        //update the product entity
        product.Update(
            productDto.Name == null ? product.Name : productDto.Name,
            productDto.Categories == null ? product.Categories : productDto.Categories.Select(s => (ProductCategory)Enum.Parse(typeof(ProductCategory), s)).ToList(),
            productDto.Price == null ? product.Price : (decimal)productDto.Price,
            productDto.Description == null ? product.Description : productDto.Description,
            productDto.ImageUrl == null ? product.ImageFile : productDto.ImageUrl
        );
    }

    private async Task<bool> validate(Product product, CancellationToken cancellationToken)
    {
        Product UpdatedProduct = await dbContext.getProductById(product.Id, cancellationToken, RequestType.Command);
        return product.Compare(UpdatedProduct);
    }
}
