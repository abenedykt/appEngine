namespace AppEngine.Abstract
{
	public abstract class WorkerBase<TRequest, TResponse> : IHandle<TRequest, TResponse>
	{
		public TResponse Process(IRequest<TRequest, TResponse> request)
		{
			return Execute((TRequest)request);
		}

		protected abstract TResponse Execute(TRequest request);
	}
}
