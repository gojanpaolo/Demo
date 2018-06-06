using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UnitsNet;
using UnitsNet.Units;
using Xunit;

namespace UseQuantityRangeAttribute
{
    //https://github.com/angularsen/UnitsNet/issues/442
    public class MyObjectTests
    {
        [Theory]
        [InlineData(-1, false)]
        [InlineData(0, true)]
        [InlineData(0.5, true)]
        [InlineData(1, true)]
        [InlineData(1.1, false)]
        public void MyLength_Range(double myLength, bool expectedIsValid)
        {
            var myObject = new MyObject();
            myObject.MyLength = new Length(myLength, LengthUnit.Meter);

            var context = new ValidationContext(myObject);
            var results = new List<ValidationResult>();
            bool actualIsValid = Validator.TryValidateObject(myObject, context, results, true);

            Assert.Equal(expectedIsValid, actualIsValid);
        }
    }

    public class MyObject
    {
        [QuantityRange(0, 1, LengthUnit.Meter)]
        public Length MyLength { get; set; }
    }

    public class QuantityRangeAttribute : RangeAttribute
    {
        private readonly double _minimum;
        private readonly double _maximum;
        private readonly int _unitEnumValue;
        private readonly Func<object, double> _convertToDouble;

        public QuantityRangeAttribute(double minimum, double maximum, LengthUnit unit) : this(minimum, maximum, (int)unit, o => ((Length)o).As(unit)) { }

        public QuantityRangeAttribute(double minimum, double maximum, RatioUnit unit) : this(minimum, maximum, (int)unit, o => ((Ratio)o).As(unit)) { }

        public QuantityRangeAttribute(double minimum, double maximum, AngleUnit unit) : this(minimum, maximum, (int)unit, o => ((Angle)o).As(unit)) { }

        //TODO: Add other Quantity types

        private QuantityRangeAttribute(double minimum, double maximum, int unitEnumValue, Func<object, double> convertToDouble) : base(minimum, maximum)
        {
            _minimum = minimum;
            _maximum = maximum;
            _unitEnumValue = unitEnumValue;
            _convertToDouble = convertToDouble;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            double doubleValue = _convertToDouble(value);
            return base.IsValid(doubleValue, validationContext);
        }
    }
}