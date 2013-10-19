namespace AppEngine.Base
{
    public interface IHandle<TRequest,TResponse>
    {
        TResponse Process(IRequest<TRequest, TResponse> request);
    }
}