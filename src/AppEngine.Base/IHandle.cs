namespace AppEngine.Base
{
    public interface IHandle<TRequest,TResponse> where TResponse : new()
    {
        TResponse Process(IRequest<TRequest, TResponse> request);
    }
}