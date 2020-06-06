using Domain.Enums;
using Infrastructure;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using System;
using static Infrastructure.Helpers.EnumHelper;

namespace Test.Infrastructure.Unit.Helpers.EnumHelpers
{
    [TestFixture]
    public static class TestGetSubtitleFormat
    {
        private static IFormFile GetMockFileWithExtension(string extension)
        {
            var file = new Mock<IFormFile>();
            file.Setup(x => x.FileName).Returns($"filename{extension}");

            return file.Object;
        }

        [TestCase(".ass", ESubtitleFormat.ASS)]
        [TestCase(".srt", ESubtitleFormat.SRT)]
        public static void FileWithSubtitleExtension_ShouldReturnESubtitleFormat(string extension, ESubtitleFormat expectedFormat)
        {
            // Arrange

            // Act
            var format = GetMockFileWithExtension(extension).GetSubtitleFormat();

            // Assert
            Assert.That(format, Is.EqualTo(expectedFormat));
        }

        [Test]
        public static void FileWithOtherExtension_ShouldThrowException([Values(".txt", ".png", ".png")]string extension)
        {
            // Arrange
            var file = GetMockFileWithExtension(extension);

            // Act

            // Assert
            Assert.That(
                () => file.GetSubtitleFormat(),
                Throws.TypeOf<ArgumentException>().With.Property("Message").EqualTo(ExceptionCode.ESubtitleFormatOutOfRange)
            );
        }
    }
}
