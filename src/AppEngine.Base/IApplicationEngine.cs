namespace AppEngine.Base
{
    public interface IApplicationEngine
    {
        void RegisterWorker<TRequest, TResponse>(IHandle<TRequest, TResponse> worker) where TResponse : new();
        TResponse Execute<TRequest, TResponse>(IRequest<TRequest,TResponse> request) where TResponse : new();
        void RegisterRequestSink(IRequestSink sink);
    }
}