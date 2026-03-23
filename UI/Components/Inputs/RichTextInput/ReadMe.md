# `<RichTextInput />`

> Data Type: `System.Collections.Generic.List<Classes.cRichTextBlock>`

This input allows for rich textual formatting, including **bold**, *italics*, underline along with headings 1-3, bullet point lists, and links along with a few other features.

> [!Note]
> Data for this input is stored as an object and can be serialized to a json string for storing in a database

## Usage

When using this component, it is recommended to wrap it inside of a `<Form />` tag to handle submission and such of data, though it can be used by itself as well. Below is a simple code snippet on how to use it:

```razor
<RichTextInput @bind-Value=@fobjValue />
```

When the value to the `<RichTextInput />` is set outside of the page load functions (i.e. `OnParametersSetAsync` or others) you may need to reference the input with `@ref=@fobjRichTextInputRef`. Below is a sample code snippet of refreshing the contents manually.

```csharp
private RichTextInput? fobjRichTextInputRef = null;

private async void subHandleSubmit()
{
    ...

    await fobjRichTextInputRef?.subRefreshContent();

    ...
}
```

## Rendering data

To render the data from the `<RichTextInput />`, you can use the helper function to either build it as a `RenderTree` (The object blazor will render) or a raw `HTML` string.

- `RichTextInput.fncRenderContent` - This can be used in your `.razor` page by simply being called as `@RichTextInput.fncRenderContent(fobjRichTextValue)` and it will manage rendering out the data similar to how a component would.
- `RichTextInput.fncRenderContentAsHTML` - If this is used in the same way as `fncRenderContent()` is in the razor page, it will simply render out as plain text and the user will see the `HTML` without any proper styling. This function is only to be used in certain cases when a raw `HTML` string is needed (possibly useful when displaying rich text in ActiveReports, though this functionality has not been tested). By default, this `HTML` string will not contain any sort of vulnerabilities to `XSS`, as any extra attributes will not be saved to the object (`JSON`)