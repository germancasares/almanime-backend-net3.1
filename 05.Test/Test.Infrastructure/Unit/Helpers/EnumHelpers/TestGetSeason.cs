using Domain.Enums;
using Infrastructure;
using NUnit.Framework;
using System;
using static Infrastructure.Helpers.EnumHelper;

namespace Test.Infrastructure.Unit.Helpers.EnumHelper
{
    [TestFixture]
    public static class TestGetSeason
    {
        [TestCase(1, ExpectedResult = ESeason.Winter)]
        [TestCase(2, ExpectedResult = ESeason.Winter)]
        [TestCase(3, ExpectedResult = ESeason.Spring)]
        [TestCase(4, ExpectedResult = ESeason.Spring)]
        [TestCase(5, ExpectedResult = ESeason.Spring)]
        [TestCase(6, ExpectedResult = ESeason.Summer)]
        [TestCase(7, ExpectedResult = ESeason.Summer)]
        [TestCase(8, ExpectedResult = ESeason.Summer)]
        [TestCase(9, ExpectedResult = ESeason.Fall)]
        [TestCase(10, ExpectedResult = ESeason.Fall)]
        [TestCase(11, ExpectedResult = ESeason.Fall)]
        [TestCase(12, ExpectedResult = ESeason.Winter)]
        public static ESeason IntegerInRange_ShouldReturnESeason(int month) => GetSeason(month);

        [TestCase("2020-01-01", ExpectedResult = ESeason.Winter)]
        [TestCase("2020-02-01", ExpectedResult = ESeason.Winter)]
        [TestCase("2020-03-01", ExpectedResult = ESeason.Spring)]
        [TestCase("2020-04-01", ExpectedResult = ESeason.Spring)]
        [TestCase("2020-05-01", ExpectedResult = ESeason.Spring)]
        [TestCase("2020-06-01", ExpectedResult = ESeason.Summer)]
        [TestCase("2020-07-01", ExpectedResult = ESeason.Summer)]
        [TestCase("2020-08-01", ExpectedResult = ESeason.Summer)]
        [TestCase("2020-09-01", ExpectedResult = ESeason.Fall)]
        [TestCase("2020-10-01", ExpectedResult = ESeason.Fall)]
        [TestCase("2020-11-01", ExpectedResult = ESeason.Fall)]
        [TestCase("2020-12-01", ExpectedResult = ESeason.Winter)]
        public static ESeason DateTimeInRange_ShouldReturnESeason(DateTime date) => GetSeason(date);

        [Test]
        public static void IntegerOutOfRange_ShouldThrowException([Values(-1, 13, 9999)] int month) =>
            Assert.That(
                () => GetSeason(month),
                Throws.TypeOf<ArgumentException>().With.Property("Message").EqualTo(ExceptionCode.ESeasonOutOfRange)
            );
    }
}
