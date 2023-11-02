using API.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Tests.Helpers
{
    public class EnumHelperTest
    {
        [Fact]
        public void GetEnumValues_CorrectRange_ReturnsTrue()
        {
            var bottom = 0;
            var top = Enum.GetNames(typeof(PropertyEnum)).Length;

            var result = EnumHelper.GetRandomEnumValue<PropertyEnum>();

            Assert.True(result > bottom && result < top);
        }
        [Fact]
        public void GetRandomEnumValue_ReturnsValidEnumValue()
        {
            var observedValues = new HashSet<int>();

            for (int i = 0; i < 1000; i++)
            {
                int value = EnumHelper.GetRandomEnumValue<PropertyEnum>();
                observedValues.Add(value);

                Assert.True(Enum.IsDefined(typeof(PropertyEnum), value));
            }

            var allEnumValues = Enum.GetValues(typeof(PropertyEnum)).Cast<int>().ToHashSet();
            Assert.True(observedValues.IsSupersetOf(allEnumValues));
        }
    }
}
