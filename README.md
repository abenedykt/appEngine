appEngine [![Build status](https://ci.appveyor.com/api/projects/status/jjwqq5d936xnt9rf)](https://ci.appveyor.com/project/abenedykt/appengine)
=========

appEngine - simple dispatcher for request

initialize appengine and register workers
```
var app = new ApplicationEngine();
app.RegisterWorker(workerThatWillHandleTheRequest);
```

and you're ready to go
```
//and now you can just

var response = app.Execute(new Request{
  arg1 = "Hello",
  arg2 = "World"
  };
  
  
// and hey, your response have an intellisense
```

---------
appEngine will send request to the Worker that knows how to handle the request and will return the response.

All the magic is created with just 2 interfaces:

1) Mark Interface for Request
  
```  
public interface IRequest<TRequest, TResponse>{} 
```

that tells the engine the type of request and corresponding response
  
2) Worker

```
public interface IHandle<TRequest,TResponse>
{
        TResponse Process(IRequest<TRequest, TResponse> request);
}
```

Worker that can process the given request to given response.

Now with that, you can separate UI layer from the real application without any unnececary references and keep things separated
