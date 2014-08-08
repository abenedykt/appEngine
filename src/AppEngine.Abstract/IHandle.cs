namespace AB.AppEngine.Abstract
{
    public interface IHandle<TRequest,TResponse>
    {
        TResponse Process(IRequest<TRequest, TResponse> request);
    }
}