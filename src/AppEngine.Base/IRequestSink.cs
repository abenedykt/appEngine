namespace AppEngine.Base
{
    public interface IRequestSink
    {
        void OnIncomingRequest<TRequest,TResponse>(IRequest<TRequest, TResponse> request) where TResponse : new();
    }
}