using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UnitsNet;
using UnitsNet.Units;
using Xunit;

namespace UseSeparateRangeAttribute
{

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
        [LengthRange(0, 1, LengthUnit.Meter)]
        public Length MyLength { get; set; }
    }

    public sealed class LengthRangeAttribute : ValidationAttribute
    {
        private readonly Length _minimum;
        private readonly Length _maximum;
        private readonly LengthUnit _unit;

        public LengthRangeAttribute(double mininum, double maximum, LengthUnit unit)
        {
            _minimum = new Length(mininum, unit);
            _maximum = new Length(maximum, unit);
            _unit = unit;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Length quantity = (Length)value;

            if (quantity < _minimum || quantity > _maximum)
            {
                string unitAbbrevation = UnitSystem.GetDefaultAbbreviation(_unit, null);
                return new ValidationResult($"The field {validationContext.DisplayName} must be between {_minimum.Value} to {_maximum.Value} {unitAbbrevation}.");
            }

            return ValidationResult.Success;
        }
    }
}