# `<Table />`

### Requirements

To use this `<Table />` component you need the following data points:

- A generic list of objects (This is the main list of data that will be displayed)

## Code Snippet

```razor
<Table @bind-Data=@falobjData>
    <ActionColumn Action=@((link) => mobjNavigationManager.NavigateTo("custom/link/to/navigate/to")) DisplayName="Edit">Edit Row</ActionColumn>
    <Column Field=@((obj) => obj.mstrString) DisplayName="Plain string value" />
    <Column Field=@((obj) => obj.mdteDateTimeValue) DisplayName="Date Time value with custom display">@context.mdteDateTimeValue.ToString("MM/dd/yyyy hh:mm tt")</Column>
</Table>
```

## Component details

### <Table />

- `Data`/`DataChanged`/`TData` (`@bind-Data` will bind the list and populate all 3 of these parameters) - These parameters are used to pass in the generic list of data used to iterate over
- `ShowSelectOption` - Boolean and can be written as `<Table ShowSelectOption />` (leaving off the `=@(true)` doesn't effect it). This will show a checkbox next to each row
- `Value`/`ValueChanged` (`@bind-Value` will bind a list of `TData` objects to these 2 parameters) - These parameters are used as somewhat of an "*input*" when the option for `ShowSelectOption` is checked. If a list is bound to this it will populate with all rows whose checkboxes are selected
- `EmptyValuePlaceholder` - This is defaultly set to `-`. If a value in one of the columns is `null` it will display `-` instead of being blank

### <Column />

- `Field` - This should be set to a Linq expression with the parameter for the row being passed in (i.e. `<Column Field=@((objRow) => objRow.mobjField) />`). It denotes which field of each object will be displayed.
- `DisplayName` - A basic string to set the name of each column's header
- `ChildContent`/`Context` - Child content can be passed into the <Column>...</Column>. The data from each row can be accessed like `<Column>@context.mobjField.toString()</Column>`. Similarly, the `Context` parameter can be a string to denote the name of the `@context` object (i.e. `<Column Context="objCustomContextName">@objCustomContextName.mobjField</Column>`). (*Note:* This is generally used if the value is an object. Things like DateTime can be made to custom display values like `@context.mdteField.ToString("MM/dd/yyyy")`)
- `Hidden` - Boolean and can be written as `<Column Hidden />` (leaving off the `=@(true)` doesn't effect it). This will hide the column and it's corresponding values
- `DefaultWidth` - The default width of a column. Columns are automatically sized upon rendering (and are resizable upon dragging the right handle of the header)
- `TextAlign` - Enum (`Column<T>.enumTextAlign`) to denote which alignment to use (Left, center or right)
- ~~`TData` (*Auto populated by `<Table />`*) - Data type that's used in `<Table @bind-Data... />` parameter~~

### <ActionColumn />

- `Action` - A function (System.Action) to be executed upon the contents of the column being clicked. It includes a parameter for the row's object and can be used like `<ActionColumn Action=@((objRow) => ...) />`.
- `DisplayName` - A basic string to set the name of each column's header
- `ChildContent` - Any value to be repeated for each row. Plain text will be made clickable if `Action` is passed as a parameter, you can also use a custom `<Button />` to handle clicks
- `Context` - The `Context` parameter can be a string to denote the name of the `@context` object (i.e. `<Column Context="objCustomContextName">@objCustomContextName.mobjField</Column>`). (*Note:* This is generally used if the value is an object. Things like DateTime can be made to custom display values like `@context.mdteField.ToString("MM/dd/yyyy")`)
- ~~`TData` (*Auto populated by `<Table />`*) - Data type that's used in `<Table @bind-Data... />` parameter~~

### <ResizableColumn />

! IMPORTANT ! - This component is for internal use only. The `<Table />` component uses it when rendering it's contents