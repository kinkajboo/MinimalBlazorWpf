# Minimal .NET 6 Blazor WPF app

Requires Microsoft Edge [WebView2](https://developer.microsoft.com/en-us/microsoft-edge/webview2/)

## Overview
Projects where created using dotnet cli:  
```
dotnet new wpf -o WpfApp
dotnet new razorlib -o BlazorLib
```

Following changes are applied  

### WpfApp
- Added wwwroot folder and content
- Added Pages folder and content, Index.razor references BlazorLib
- Added _Imports.razor
- Added Main.razor
- Updated MainWindow.xaml
```xml
xmlns:blazor="clr-namespace:Microsoft.AspNetCore.Components.WebView.Wpf;assembly=Microsoft.AspNetCore.Components.WebView.Wpf"
..
<blazor:BlazorWebView HostPage="wwwroot\index.html" Services="{StaticResource services}" x:Name="blazorWebView1">
    <blazor:BlazorWebView.RootComponents>
        <blazor:RootComponent Selector="#app" ComponentType="{x:Type local:Main}" />
    </blazor:BlazorWebView.RootComponents>
</blazor:BlazorWebView>
```
- Updated MainWindow.xaml.cs
```cs
var services = new ServiceCollection();
services.AddBlazorWebView();
Resources.Add("services", services.BuildServiceProvider());

...
public partial class Main {}
```
- Updated WpfApp.csproj
```xml
<Project Sdk="Microsoft.NET.Sdk.Razor">
  <PropertyGroup>
    ...
    <RuntimeIdentifier>win-x64</RuntimeIdentifier>
    <Nullable>enable</Nullable>
    <UseWPF>true</UseWPF>
    ...
  </PropertyGroup>

  <ItemGroup>
    <Content Update="wwwroot\**">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\BlazorLib\BlazorLib.csproj" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.Components.WebView.Wpf" Version="6.0.101-preview.10.2068" />
  </ItemGroup>
</Project>
```
### BlazorLib
- Split Component1.razor into two files and added simple counter
```html
@namespace BlazorLib
<div class="my-component">
    This component is defined in the <strong>BlazorLib</strong> library. BlazorLib Counter: @Counter
</div>
<button @onclick="Increment">Increment</button>
```
```cs
using Microsoft.AspNetCore.Components;

namespace BlazorLib
{
    public partial class Component1 : ComponentBase
    {
        protected int Counter = 0;
        protected void Increment()
        {
            Counter++;
        }
    }
}
```

## Publish
From within WpfApp folder  
```
dotnet publish -c release --self-contained false
```

## Output
![](/assets/publish-screenshot.PNG?raw=true)  
WpfApp.exe.WebView2 folder will be created on run.