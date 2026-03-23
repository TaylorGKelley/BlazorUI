# `ComponentMain`

A base class that provides basic functions and injects useful services for use in *Components* or *Pages*.

## Usage

Add import statements at the top of your razor page. If using a code-behind page, be sure to inherit from `ComponentMain` for the class as well

```razor
@inherits ComponentMain
```

> [!Important]
> When overriding `OnAfterRenderAsync` be sure to insert the following line of code at the beginning of the funciton:
> `base.OnAfterRenderAsync(firstRender)`

> [!Note]
> If using a razor page, specify this class to inherit from

```csharp
partial class NewComponent : ComponentMain
{
	...
}
```

## Provided functions

Below is an explination for the functions provided in this class

- `subToast` - Show a Toast notification when an action occurs. Specify the type of notification (i.e. `enumToastType.typSuccess` when an action was completed successfully or `typError` for errors (Handled automatically with subSetLastError as well)
- `subSetLastError` - Sets the error and handles displaying it in the front-end
- `fncKeyBind`/`subKeyPressed` - If you have a code action that you want ran upon a key shortcut being pressed (i.e. `Ctrl + K` for focusing on a searchbar), you can register it in `OnAfterRenderAsync` (See callout in usage section when overriding this function) using the `fncKeyBind` function. This function will then call the `subKeyPressed` (Which should be overridden in your code if `fncKeyBind` is used). *Note:* See the below section for a sample implementation of this function.
- Cookies: `subSetCookieAsync`/`subGetCookieAsync`/`subDeleteCookieAsync` - These functions should be mostly self explanitory. They execute js on the client-side to get, set, and delete cookies. This can be used to store user specific settings/preferences without having to use a Database table.
- `subGenerateRandomHex` - This is mostly used for derivitives of the `ComponentMain` class like `InputMain<T>`. It's a replacemnet for GUID that is shorter and just easier to read/manage, but is used in the same way a GUID could be used with generating a random string to uniquely identify something (`InputMain<T>` uses it for the input's `Id` to keep inputs separate when using JS with them).
