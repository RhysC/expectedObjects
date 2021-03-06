﻿using System.Collections.Generic;
using ExpectedObjects.Specs.TestTypes;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace ExpectedObjects.Specs
{
	public class when_comparing_with_custom_comparison
	{
		static ComplexType _actual;
		static object _expected;
		static bool _called;

		static bool _result;
		static Mock<IComparison> _comparisionSpy;

		Establish context = () =>
			{
				_comparisionSpy = new Mock<IComparison>();
				_comparisionSpy.Setup(x => x.AreEqual(Moq.It.IsAny<object>()))
				               .Callback<object>(x => _called = true);


				_expected = new
					{
						StringProperty = _comparisionSpy.Object
					}.ToExpectedObject().IgnoreTypes();

				_actual = new ComplexType
					{
						StringProperty = "test string",
						DecimalProperty = 10.10m,
						IndexType = new IndexType<int>(new List<int> {1, 2, 3, 4, 5})
					};
			};

		Because of = () => _result = _expected.Equals(_actual);

		It should_use_custom_comparison = () => _called.ShouldBeTrue();
	}
}