﻿using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions;

namespace TddEbook.TddToolkit.Helpers.Constraints.EqualityOperator
{
  public class StateBasedEqualityMustBeImplementedInTermsOfInequalityOperator<T> : IConstraint where T : class
  {
    private readonly ValueObjectActivator<T> _activator;

    public StateBasedEqualityMustBeImplementedInTermsOfInequalityOperator(ValueObjectActivator<T> activator)
    {
      _activator = activator;
    }

    public void CheckAndRecord(ConstraintsViolations violations)
    {
      var instance1 = _activator.CreateInstanceAsValueObjectWithFreshParameters();
      var instance2 = _activator.CreateInstanceAsValueObjectWithPreviousParameters();

      RecordedAssertions.False(Are.NotEqualInTermsOfInEqualityOperator(instance1, instance2), 
        "a != b should return false if both are created with the same arguments", violations);
      RecordedAssertions.False(Are.NotEqualInTermsOfInEqualityOperator(instance2, instance1), 
        "b != a should return false if both are created with the same arguments", violations);
    }
  }
}