using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using FluentAssertions;
using ProductFlow.Application.UseCases.Products.Delete;
using ProductFlow.Domain.Entities;
using ProductFlow.Exception;
using ProductFlow.Exception.ExceptionBase;

namespace UseCases.Test.Products.Delete;

public class DeleteProductUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var expense = ProductBuilder.Build();
        
        var useCase = CreateUseCase(expense);

        var act = async () => await useCase.Execute(expense.Id);
        
        await act.Should().NotThrowAsync();
    }

    [Fact]
    private async Task Error_Expense_Not_Found()
    {
        var useCase = CreateUseCase();
        
        var act = async () => await useCase.Execute(id: 1000);

        var result = await act.Should().ThrowAsync<NotFoundException>();

        result.Where(ex =>
            ex.GetErrors().Count() == 1 && ex.GetErrors().Contains(ResourceErrorMessages.PRODUCT_NOT_FOUND));
    }

    
    private DeleteProductUseCase CreateUseCase(Product? product = null)
    {
        var repositoryWriteOnly = ProductsWriteOnlyRepositoryBuilder.Build();
        var repository = new ProductsReadOnlyRepositoryBuilder().GetById(product).Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        
        return new DeleteProductUseCase(repository, repositoryWriteOnly, unitOfWork);
    }
}