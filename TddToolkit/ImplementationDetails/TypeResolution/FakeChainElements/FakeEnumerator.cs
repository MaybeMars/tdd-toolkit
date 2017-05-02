using System.Collections;
using TddEbook.TddToolkit.Subgenerators;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements
{
  public class FakeEnumerator<T> : IResolution<T>
  {
    public FakeEnumerator()
    {
    }

    public bool Applies()
    {
      return TypeOf<T>.Is<IEnumerator>();
    }

    public T Apply(IProxyBasedGenerator proxyBasedGenerator)
    {
      return (T)(proxyBasedGenerator.Instance<object[]>().GetEnumerator());
    }
  }
}