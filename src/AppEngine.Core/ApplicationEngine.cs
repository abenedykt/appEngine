﻿using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using AppEngine.Abstract;
using AppEngine.Core.Exceptions;

namespace AppEngine.Core
{
    public class ApplicationEngine : IApplicationEngine
    {
        private readonly IList<IRequestSink> _requestSinks = new List<IRequestSink>();
        private readonly ConcurrentBag<object> _workers = new ConcurrentBag<object>();

        public TResponse Execute<TRequest, TResponse>(IRequest<TRequest, TResponse> request)
        {
            NotifyRequestSinks(request);

            return ExecuteRequest(request);

        }

        private TResponse ExecuteRequest<TRequest, TResponse>(IRequest<TRequest, TResponse> request)
        {
            var worker = _workers.OfType<IHandle<TRequest, TResponse>>().FirstOrDefault();

            if (worker != null)
            {
                return worker.Process(request);
            }
            return default(TResponse);
        }

        private void NotifyRequestSinks<TRequest, TResponse>(IRequest<TRequest, TResponse> request)
        {
            foreach (var sink in _requestSinks)
            {
                sink.OnIncomingRequest(request);
            }
        }

        public void RegisterRequestSink(IRequestSink sink)
        {
            _requestSinks.Add(sink);
        }

        public void RegisterWorker<TRequest, TResponse>(IHandle<TRequest, TResponse> worker)
        {
            if (_workers.OfType<IHandle<TRequest, TResponse>>().Any())
                throw new WorkerForGivenPairAlreadyExist();

            _workers.Add(worker);
        }
    }
}
