using System;
using Castle.DynamicProxy;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution
{
  internal class CachedGeneration
  {
    private readonly ReturnValueCache _cache;

    public CachedGeneration(ReturnValueCache cache)
    {
      _cache = cache;
    }

    public object GetReturnTypeFor(IInvocation invocation)
    {
      var cacheKey = ReturnValueCacheKey.For(invocation);
      if (!_cache.AlreadyContainsValueFor(cacheKey))
      {
        var returnType = invocation.Method.ReturnType;
        var returnValue = AnyReturnValue.Of(returnType).Generate();
        _cache.Add(cacheKey, returnValue);
      }

      return _cache.ValueFor(cacheKey);
    }

    public bool AppliesTo(Type returnType)
    {
      return returnType != typeof (void);
    }
  }
}