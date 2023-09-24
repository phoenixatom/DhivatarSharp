using System;
using System.Drawing;
using Xunit;

namespace DhivatarGenerator.Tests
{
    public class DhivatarTests
    {
        [Fact]
        public void GenerateDhivatar_ValidInput_ReturnsImageByteArray()
        {
            // Arrange
            string input = "މުހައްމަދު އަރުހަމް";
            int size = 150;
            Color? bgColor = Color.White;
            Color fontColor = Color.Black;
            string fontName = "mv_eaman_xp.otf";
            string fileType = "PNG";

            // Act
            byte[] avatarImage = Dhivatar.Generate(input, size, bgColor, fontColor, fontName, fileType);

            // Assert - lol if it exists, it's good to go!
            Assert.NotNull(avatarImage);
            Assert.True(avatarImage.Length > 0);
        }
    }
}