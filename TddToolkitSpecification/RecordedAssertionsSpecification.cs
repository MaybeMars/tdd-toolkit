﻿using NSubstitute;
using NUnit.Framework;
using TddEbook.TddToolkit;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions;

namespace TddToolkitSpecification
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


  }


}
