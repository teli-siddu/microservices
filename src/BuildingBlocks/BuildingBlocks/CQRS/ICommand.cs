using MediatR;
namespace BuildingBlocks.CQRS
{
    //Unit represents void return type
    public interface ICommand : ICommand<Unit>
    {

    }
    public interface ICommand<out TResponse> :IRequest<TResponse>
    {
    }
}
