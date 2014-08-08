namespace AB.AppEngine.Abstract
{
    public interface IApplicationEngine
    {
        void RegisterWorker<TRequest, TResponse>(IHandle<TRequest, TResponse> worker);
        TResponse Execute<TRequest, TResponse>(IRequest<TRequest,TResponse> request);
        void RegisterRequestSink(IRequestSink sink);
    }
}