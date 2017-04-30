using Castle.DynamicProxy;
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements;
using TddEbook.TddToolkit.Subgenerators;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.Interceptors
{
  internal class FakeAbstractClass<T> : IResolution<T>
  {
    private readonly CachedReturnValueGeneration _generation;
    private readonly ProxyGenerator _proxyGenerator;

    public FakeAbstractClass(CachedReturnValueGeneration generation, ProxyGenerator proxyGenerator)
    {
      _generation = generation;
      _proxyGenerator = proxyGenerator;
    }

    public bool Applies()
    {
      return typeof (T).IsAbstract;
    }

    public T Apply(IProxyBasedGenerator proxyBasedGenerator)
    {
      var fallbackTypeGenerator = new FallbackTypeGenerator<T>();
      var result = (T)(_proxyGenerator.CreateClassProxy(
        typeof(T),
        fallbackTypeGenerator.GenerateConstructorParameters().ToArray(), 
        new AbstractClassInterceptor(_generation)));
      fallbackTypeGenerator.FillFieldsAndPropertiesOf(result);
      return result;
    }
  }
}