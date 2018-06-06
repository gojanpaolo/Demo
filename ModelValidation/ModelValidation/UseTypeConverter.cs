using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using UnitsNet;
using UnitsNet.Units;
using Xunit;

namespace UseTypeConverter
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
            /// We need to add LengthTypeConverter to the TypeDescriptor attributes which is used by the RangeAttribute
            /// https://referencesource.microsoft.com/#System.ComponentModel.DataAnnotations/DataAnnotations/RangeAttribute.cs,178
            /// https://stackoverflow.com/questions/606854/inject-custom-type-conversion-to-net-library-classes?utm_medium=organic&utm_source=google_rich_qa&utm_campaign=google_rich_qa
            TypeDescriptor.AddAttributes(typeof(Length), new TypeConverterAttribute(typeof(LengthTypeConverter)));

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
        [Range(typeof(Length), "0 m", "1 m")]
        public Length MyLength { get; set; }
    }

    public class LengthTypeConverter : TypeConverter
    {
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return Length.Parse((string)value);
        }
    }
}
