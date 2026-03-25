# BlazorUI

A component library for Blazor .NET 8 with Server-Side Rendering. Provides a collection of reusable UI components, form inputs, data tables, and utility services to accelerate Blazor application development.

---

## Requirements

- .NET 8 (Server-Side Rendering)
- ASP.NET Core

---

## Getting Started

### 1. Register Services

Add the following to your `Program.cs` **before** `var app = builder.Build()`:

```csharp
builder.Services.AddScoped<cToastService>();
builder.Services.AddScoped<cApplicationState>();
```

### 2. Add Imports

Add the following to your `_Imports.razor` file for convenient access to all components:

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

### 3. Wrap Your App

Wrap your root layout content in `<ToastProvider>` and `<ErrorContext>` to enable toast notifications and error boundaries globally:

```razor
<ToastProvider>
    <ErrorContext>
        @Body
    </ErrorContext>
</ToastProvider>
```

### 4. `@rendermode` for pages

Add the `@rendermode RenderMode.InteractiveServer` to the top of each page that uses a component if using .Net 8 Web app

---

## Components

### Base Classes

#### `ComponentMain`

The base class for all pages and components. Inherit from it to gain access to injected services and utility functions.

```razor
@inherits ComponentMain
```

```csharp
public partial class MyComponent : ComponentMain { }
```

Provides:

- `subToast(enumToastType, title, content)` — show a toast notification
- `subSetLastError(methodName, message)` — log and display errors
- `fncKeyBind(key, modifier, identifier)` / `subKeyPressed(identifier)` — register keyboard shortcuts
- `subSetCookieAsync` / `fncGetCookieAsync<T>` / `subDeleteCookieAsync` — cookie management
- `fncStateItemExists` / `fncAddStateItem<T>` / `fncGetStateItem<T>` / `fncRemoveStateItem` — global application state

> **Note:** When overriding `OnAfterRenderAsync`, call `await base.OnAfterRenderAsync(firstRender)` at the start.

---

### Inputs

All inputs share common parameters inherited from `InputMain`:

| Parameter | Description |
|---|---|
| `Label` | Label text displayed above the input |
| `Placeholder` | Placeholder text |
| `Required` | Appends `*` to label and validates on form submit |
| `Optional` | Appends `(optional)` to label |
| `Disabled` | Disables the input |
| `ErrorMessage` | Custom error message override |
| `WarningMessage` | Advisory warning displayed on the input |

#### `<TextInput />`

```razor
<TextInput Label="Name" Placeholder="Enter name" Required @bind-Value=@fstrName />
```

Additional parameters: `MaxLength`, `Pattern` (regex)

#### `<TextAreaInput />`

```razor
<TextAreaInput Label="Bio" AutoSizing MaxLength="500" @bind-Value=@fstrBio />
```

Additional parameters: `MaxLength`, `AutoSizing`, `MinHeight`, `MaxHeight`

#### `<NumberInput />`

```razor
<NumberInput Label="Quantity" Min="1" Max="100" @bind-Value=@fintQty />
```

Additional parameters: `Min`, `Max`, `HideButtons`

#### `<DateInput />` / `<TimeInput />` / `<DateTimeInput />`

```razor
<DateInput Label="Start Date" Required @bind-Value=@fdteStart />
<TimeInput Label="Start Time" @bind-Value=@fdteStart />
```

#### `<PhoneNumberInput />`

Automatically formats input as `(000) 000-0000`.

```razor
<PhoneNumberInput Label="Phone" @bind-Value=@flngPhone />
```

#### `<ZipCodeInput />`

```razor
<ZipCodeInput Label="Zip Code" @bind-Value=@fintZip />
```

#### `<CheckboxInput />`

```razor
<CheckboxInput Label="Agree to terms" @bind-Value=@fblnAgreed />
```

#### `<SelectInput />`

Supports single and multi-select with optional search and "Select All".

```razor
<SelectInput Label="Role" Required @bind-Value=@falstrSelected>
    <Option Value="admin">Admin</Option>
    <Option Value="user">User</Option>
</SelectInput>

<SelectInput Label="Tags" MultiSelect SelectAllOption Searchable @bind-Value=@falstrTags>
    <Option Value="blazor">Blazor</Option>
    <Option Value="dotnet">.NET</Option>
</SelectInput>
```

Additional parameters: `MultiSelect`, `SelectAllOption`, `Searchable`, `PreventDeselect`, `ListStyle`

#### `<FileInput />`

```razor
<FileInput Label="Attachments" @bind-Value=@falobjFiles OnFileDrop=@subHandleFileDrop MaxFileSizeMB="25" />
```

#### `<RichTextInput />`

Full-featured rich text editor supporting bold, italic, underline, headings, bullet lists, block quotes, links, and images.

```razor
<RichTextInput Label="Description" @bind-Value=@falobjContent />
```

Render saved content:

```razor
@RichTextInput.fncRenderContent(falobjContent)
```

---

### Form

Wraps inputs and handles validation and submission.

```razor
<Form OnValidate=@subValidate OnSubmit=@subSubmit>
    <TextInput Label="Email" Required @bind-Value=@fstrEmail />
    <SubmitButton>Save</SubmitButton>
</Form>
```

| Parameter | Description |
|---|---|
| `OnSubmit` | Called after all validation passes |
| `OnValidate` | Synchronous custom validation; return `false` to block submission |
| `OnValidateAsync` | Async custom validation; return `false` to block submission |

---

### Table

Displays a list of objects with sortable, resizable columns and optional row selection.

```razor
<Table @bind-Data=@falobjItems>
    <ActionColumn DisplayName="Edit" Action=@((row) => subEdit(row))>Edit</ActionColumn>
    <Column Field=@((row) => row.mstrName) DisplayName="Name" />
    <Column Field=@((row) => row.mdteCreated) DisplayName="Created">
        @context.mdteCreated.ToString("MM/dd/yyyy")
    </Column>
</Table>
```

| Parameter | Description |
|---|---|
| `Data` / `@bind-Data` | The list of objects to display |
| `ShowSelectOption` | Adds checkboxes for row selection |
| `@bind-Value` | Bound list of selected rows (requires `ShowSelectOption`) |
| `EmptyValuePlaceholder` | Text shown for null values (default: `-`) |

---

### Modal

```razor
<Modal @ref=@mobjModal ModalType=Modal.enumModalType.typLarge OnClose=@subHandleClose>
    <Title>Confirm Action</Title>
    <ChildContent>
        Are you sure you want to proceed?
    </ChildContent>
</Modal>
```

```csharp
Modal.enumCloseAction result = await mobjModal.fncShowModal();

if (result == Modal.enumCloseAction.typConfirm) { ... }
```

Close actions: `typConfirm`, `typCancel`, `typDisolve`

---

### Popover

```razor
<Popover Anchor=@Popover.enumPopoverPosition.typTopCenter>
    <AnchorElement>
        <Button @onclick=@context.fncToggle>Open</Button>
    </AnchorElement>
    <Content>
        <div style="padding: 1rem;">Popover content here</div>
    </Content>
</Popover>
```

---

### TabView

```razor
<TabView @ref=@fobjTabs OnFocusedTabChange=@StateHasChanged>
    <Tab Id="details" Default>
        <p>Details content</p>
    </Tab>
    <Tab Id="history">
        <p>History content</p>
    </Tab>
</TabView>

<Button @onclick=@(() => fobjTabs?.fncNavigateTo("history"))>View History</Button>
```

Tab state is synced to the URL via the `?tabId=` query parameter.

---

### SidePanel

```razor
<SidePanel @ref=@mobjPanel OnClose=@subHandleClose>
    <p>Side panel content</p>
</SidePanel>

<Button @onclick=@mobjPanel.subOpen>Open Panel</Button>
```

---

### Toast Notifications

From any component inheriting `ComponentMain`:

```csharp
subToast(enumToastType.typSuccess, "Saved!", "Your changes have been saved.");
subToast(enumToastType.typError, "Error", "Something went wrong.");
subToast(enumToastType.typInfo, "Note", "FYI.", pobjToastAction: new cToastAction("Undo", () => subUndo()));
```

Toast types: `typSuccess`, `typError`, `typInfo`, `typWarning`

---

### SectionProgressBar

```razor
<SectionProgressBar Sections=@(new List<cSectionProgress> {
    new cSectionProgress { mstrTitle = "Step 1", mintProgressPercentage = 100 },
    new cSectionProgress { mstrTitle = "Step 2", mintProgressPercentage = 60 },
    new cSectionProgress { mstrTitle = "Step 3", mintProgressPercentage = 0 },
}) />
```

---

### ProgressBar

```razor
<ProgressBar Value=@fintProgress MaxValue=@100 Status=@ProgressBar<int>.enumStatus.typInProgress>
    <DefaultText>Uploading @context.mintValue%</DefaultText>
    <SuccessText>Done!</SuccessText>
    <FailedText>Failed</FailedText>
</ProgressBar>
```

---

## Services

### Application State

A scoped key-value store for sharing state across components without cascading parameters or re-renders.

```csharp
mobjApplicationState.fncAddStateItem("userId", 42);
int? userId = mobjApplicationState.fncGetStateItem<int>("userId");
mobjApplicationState.fncRemoveStateItem("userId");
```

---

## Utilities

### `cNavigationManagerExtension`

Extension methods for reading and writing URL query parameters:

```csharp
int? id = mobjNavigationManager.GetQueryParameterValue<int>("id");
mobjNavigationManager.AddOrUpdateQueryParameter("tab", "details");
```

### `ValueDisplayConverter`

```csharp
ValueDisplayConverter.FormatBytes(1048576); // "1 MB"
```

### `cObjectCopy`

```csharp
var copy = cObjectCopy.fncDeepCopy(myObject);
```

---

## Icons

A set of SVG icon components is available under `BlazorUI.Components.UI.Icons`:

`CheckIcon` `XIcon` `EditIcon` `PlusIcon` `MinusIcon` `SearchIcon` `ChevronIcon` `CalendarIcon` `ImageIcon` `UploadIcon` `LoadingSpinner` `CheckboxIcon` `InfoIcon` `HelpIcon` `WarningIcon` `ErrorIcon` `DashboardIcon` `SettingsIcon` `LogoutIcon` `MapIcon` `MailIcon` `PhoneIcon` `MessageIcon` `NotificationIcon` `DocumentsIcon` `LibraryIcon` `LinksIcon` `CommunitiesIcon` `SlidersIcon` `SideMenuIcon` `TeamsLogo`

Usage:

```razor
<SearchIcon />
<ChevronIcon Direction=@ChevronIcon.enumDirection.typDown />
<NotificationIcon HasDot=true DotColor="#E22828" />
```

---

## Test Pages

A set of reference pages is included under `Pages/Test Pages/` demonstrating component usage. These may not reflect the latest API but serve as a useful starting point.

| Page | Route |
|---|---|
| `TestComponents.razor` | `/TestComponents` |
| `TestInputs.razor` | `/TestInputs` |
| `RichTextTestPage.razor` | `/testRichTextEditor` |
