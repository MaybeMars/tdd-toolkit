using TddEbook.TddToolkit.Subgenerators;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements
{
  internal class FakeConcreteClassWithNonConcreteConstructor<T> : IResolution<T>
  {
    readonly FallbackTypeGenerator<T> _fallbackTypeGenerator = new FallbackTypeGenerator<T>();

    public bool Applies()
    {
      return _fallbackTypeGenerator.ConstructorIsInternalOrHasAtLeastOneNonConcreteArgumentType();
    }

    public T Apply(IProxyBasedGenerator proxyBasedGenerator)
    {
      return _fallbackTypeGenerator.GenerateInstance(proxyBasedGenerator);
    }
  }
}