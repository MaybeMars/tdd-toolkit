﻿using NSubstitute;
using NUnit.Framework;
using TddEbook.TddToolkit;
using TddEbook.TddToolkit.ImplementationDetails.Common;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions.CustomCollections;

namespace TddEbook.TddToolkitSpecification
{
  class RecordedAssertionsSpecification
  {
    [Test]
    public void ShouldAddErrorMessageWhenTruthAssertionFails()
    {
      //GIVEN
      var violations = Substitute.For<IConstraintsViolations>();
      var anyMessage = Any.String();

      //WHEN
      RecordedAssertions.True(false, anyMessage, violations);

      //THEN
      violations.Received(1).Add(anyMessage);
    }

    [Test]
    public void ShouldNotAddErrorMessageWhenTruthAssertionPasses()
    {
      //GIVEN
      var violations = Substitute.For<IConstraintsViolations>();
      var anyMessage = Any.String();

      //WHEN
      RecordedAssertions.True(true, anyMessage, violations);

      //THEN
      violations.DidNotReceive().Add(anyMessage);
    }


    [Test]
    public void ShouldFailStaticFieldsAssertionIfAssemblyContainsAtLeastOneStaticField()
    {
      var assembly = typeof (RecordedAssertionsSpecification).Assembly;
      
      var e = Assert.Throws<AssertionException>(() => XAssert.EachTypeHasNoStaticFields(assembly));
      StringAssert.Contains("_lolek", e.Message);
      StringAssert.Contains("_gieniek", e.Message);
      StringAssert.Contains("StaticProperty", e.Message);
    }

    [Test]
    public void ShouldFailReferenceAssertionWhenAssemblyReferencesOtherAssembly()
    {
      var assembly1 = typeof(RecordedAssertionsSpecification).Assembly;
      Assert.Throws<AssertionException>(() => XAssert.IsNotReferencedBy(assembly1, typeof(TestAttribute)));
    }


    [Test] //TODO 1. only single constructor! 2. class inheritance levels
    public void ShouldFailNonPublicEventsAssertionWhenAssemblyContainsAtLeastOneNonPublicEvent()
    {
      var assembly = typeof (RecordedAssertionsSpecification).Assembly;
      Assert.Throws<AssertionException>(() => XAssert.EachTypeDoesNotDeclareNonPublicEvents(assembly));
    }

    [Test] //TODO 1. only single constructor! 2. class inheritance levels, 3. private/protected events
    public void ShouldFailConstructorLimitAssertionWhenAnyClassContainsAtLeastOneConstructor()
    {
      var assembly = typeof (RecordedAssertionsSpecification).Assembly;
      Assert.Throws<AssertionException>(() => XAssert.EachTypeHasSingleConstructor(assembly));
    }



    public class Lol2
    {
      private static int _gieniek = 123;
    }

    protected event AnyEventHandler _changed;


#pragma warning disable 169
    private static int _lolek = 12;
#pragma warning restore 169

    public static int StaticProperty
    {
      get;
      set;
    }

  }

  delegate void AnyEventHandler(object sender, AnyEventHandlerArgs args);

  interface AnyEventHandlerArgs
  {
  }

  public class ObjectWithTwoConstructors
  {
    public ObjectWithTwoConstructors(int x)
    {
      
    }

    public ObjectWithTwoConstructors(string x)
    {

    }
  }
}
