using Moq;
using NUnit.Framework;
using System;
using System.Security.Claims;
using static Infrastructure.Helpers.ExtensionHelper;

namespace Test.Infrastructure.Unit.Helpers.EnumHelpers
{
    [TestFixture]
    public static class TestGetIdentityID
    {
        [Test]
        public static void ValidNameIdentifier_ShouldReturnGuid([Values("00000000-0000-0000-0000-000000000000", "B70DDB8F-892B-4E56-886F-A7F253F8A56B")]string nameIdentifier)
        {
            // Arrange
            var user = new Mock<ClaimsPrincipal>();
            user.Setup(u => u.Claims).Returns(new Claim[] { new Claim(ClaimTypes.NameIdentifier, nameIdentifier) });

            // Act
            var ID = user.Object.GetIdentityID();

            // Assert
            Assert.That(ID, Is.EqualTo(new Guid(nameIdentifier)));
        }

        [Test]
        public static void InvalidNameIdentifier_ShouldReturnGuidEmpty([Values("", "random", "3213215324314213", "-1", "@@@@")]string nameIdentifier) {
            // Arrange
            var user = new Mock<ClaimsPrincipal>();
            user.Setup(u => u.Claims).Returns(new Claim[] { new Claim(ClaimTypes.NameIdentifier, nameIdentifier) });

            // Act
            var ID = user.Object.GetIdentityID();

            // Assert
            Assert.That(ID, Is.EqualTo(Guid.Empty));
        }
    }
}
