﻿
namespace HelloWorld.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
#pragma warning disable CA1822 // Mark members as static
public string Greeting => "Hello World!";
#pragma warning restore CA1822 // Mark members as static
}
