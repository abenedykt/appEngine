namespace AB.AppEngine.Abstract
{
    public interface IRequestSink
    {
        void OnIncomingRequest<TRequest,TResponse>(IRequest<TRequest, TResponse> request);
    }
}