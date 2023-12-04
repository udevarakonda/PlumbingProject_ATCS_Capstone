namespace HelloWorld.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
#pragma warning disable CA1822 // Mark members as static
    public string Greeting => "Hello World! This is our Technology Stack for our Capstone Project. We are using the Avalonia Application with .NET to create our desktop application.";
#pragma warning restore CA1822 // Mark members as static
}
