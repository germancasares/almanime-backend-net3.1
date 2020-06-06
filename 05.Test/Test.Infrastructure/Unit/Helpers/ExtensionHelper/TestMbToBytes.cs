using NUnit.Framework;
using static Infrastructure.Helpers.ExtensionHelper;

namespace Test.Infrastructure.Unit.Helpers.ExtensionHelper
{
    [TestFixture]
    public static class TestMbToBytes
    {
        [TestCase(-1, ExpectedResult = -1048576)]
        [TestCase(0, ExpectedResult = 0)]
        [TestCase(1, ExpectedResult = 1048576)]
        [TestCase(1024, ExpectedResult = 1073741824)]
        public static long Integer_ShouldReturnConversion(int mb) => mb.MbToBytes();
    }
}
