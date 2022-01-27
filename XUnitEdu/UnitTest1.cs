using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace XUnitEdu
{
    public class UnitTest1
    {
        [Fact(Skip ="skip test")]
        public void Test1()
        {
            Assert.True(true);
        }

        [Theory]
        [InlineData(4,2)]
        public void Test2(int a, int b)
        {
            Assert.True(a > b);
        }

        [Theory]
        [MemberData(nameof(FakeData))]
        public void Test3(int a, int b)
        {
            Assert.True(a > b);

        }

        [Theory]
        [ClassData(typeof(FakeDataFactory))]
        public void Test4(Ab ab)
        {
            Assert.True(ab.A > ab.B);

        }

        public static IEnumerable<object[]> FakeData()
        {
            yield return new object[] { 23, 18 };
        }

        public class FakeDataFactory : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new Ab {A=23,B=18 } };
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        public class Ab
        {
            public int A { get; set; }
            public int B { get; set; }
        }

    }
}
