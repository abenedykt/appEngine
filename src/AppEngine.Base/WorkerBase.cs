using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppEngine.Base
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
