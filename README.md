# DhivatarGenerator

Generates avatars from dhivehi names or strings.

## Installation 
This package can be installed directly from NuGet [here](https://www.nuget.org/packages/DhivatarGenerator/1.0.0)

### .NET CLI 
`dotnet add package DhivatarGenerator --version 1.0.0`
### PM 
`NuGet\Install-Package DhivatarGenerator -Version 1.0.0`

## Generating Dhivatars
Once you've installed the Dhivatar Generator package, you can start generating Dhivatars in your code. Here's a basic example of how to generate a Dhivatar:

```csharp
using System.Drawing;
using DhivatarGenerator;

class Program
{
    static void Main(string[] args)
    {
        // Name - REQUIRED
        string name = "މުހައްމަދު އަލީ";

        // Other customizable settings
        int size = 200;
        Color bgColor = Color.LightBlue;
        Color fontColor = Color.DarkSlateGray;
        string fontName = "my-custom-font.ttf";
        string fileType = "JPEG";

        // Generate Image
        byte[] dhivatarImage = Dhivatar.Generate(name, size, bgColor, fontColor, fontName, fileType);
    }
}
```
