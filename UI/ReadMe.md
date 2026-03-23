# BlazorUI

A guide to using this package originally made for Blazor `.Net 6 Core` with *Server-Side rendering*.

> [!NOTE]
> Viewing these markdown files in a program like [Obsidian](https://obsidian.md/) is the preferred way to view this documentation as it will format the code blocks and other textual modifications.

## Initialization

This package takes advantage of a few services that should be registered in your `Program.cs` file.

- **Toast Service** | This is used when any error occurs in a component. To use it in your own code, inherit from `ComponentMain` and call `subToast` (See [/Components/BaseComponents/ComponentMain/ReadMe.md])
- **Application State Service** | This is used to keep track of global application state. At the root it is just a dictionary that stores JSON Serialized objects. It is automatically injected into `ComponentMain` and can be used with the helper functions `fncStateItemExists()`, `fncAddStateItem<T>()`, `fncGetStateItem<T>()`, and `fncRemoveStateItem()`.

Register these services to `Program.cs` with the following code (Be sure to include it **before** the `var app = builder.Build()` line):

```csharp
builder.Services.AddScoped<cToastService>();
builder.Services.AddScoped<cApplicationState>();
```

### `_Imports.razor`

You can also add imports to the `_Imports.razor` file to make life easier while developing. Below is a code snippet that will encompass most of the components.

```razor
@using BlazorUI.Components
@using BlazorUI.Components.BaseComponents
@using BlazorUI.Components.BaseComponents.ComponentMain
@using BlazorUI.Components.BaseComponents.InputMain

@using BlazorUI.Services
@using BlazorUI.Services.Toast

@using BlazorUI.Contexts

@using BlazorUI.Utils
@using BlazorUI.Utils.Constants
@using BlazorUI.Utils.Class_Extensions
@using BlazorUI.Utils.Classes

@using BlazorUI.Components.Inputs
@using BlazorUI.Components.Inputs.Select
@using BlazorUI.Components.Inputs.RichTextInput
@using BlazorUI.Components.Inputs.Form
@using BlazorUI.Components.Inputs.Form.Classes
@using BlazorUI.Components.Table
@using BlazorUI.Components.TabView
@using BlazorUI.Components.UI
@using BlazorUI.Components.UI.SectionProgressBar
@using BlazorUI.Components.UI.SectionProgressBar.Classes
@using BlazorUI.Components.UI.Icons
```
