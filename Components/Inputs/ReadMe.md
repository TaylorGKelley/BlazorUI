# Inputs

Most inputs are similar too eachother in their use with minor differences in the Parameters that are available to pass in.

## Usage

Below is a sample snippet of what a generic input will look like:

```razor
<TextInput Label="Test" Placeholder="Testing" Required @bind-Value=@fstrValue />
```

Some standard parameters are listed below, and can be referenced in the `InputMain` or `<InputContainer />` components to see their use:

- `Id` - A custom Id for each field. This is automatically populated and is primarily used inside the component for linking it to the `Label` and other identification purposes.
- `Required`/`Optional` - By default (if both parameters are left off) the label for the input is as specified. If `Required` is added/set to true a `*` is appended to the end of the label to denote it being a required field, and it will automatically be validated to be sure it isn't empty. The `Optional` parameter parameter is purely for display purposes, as if it is added/set to true it will append "*(optional)*" to the end of the label (this is purely for the user to know it is optional, but the field will also defaultly be optional without this property).
- `Placeholder` - Placeholder value for the input
- `Disabled` - Whether the input is disabled or not
- `Error Message` - This can be used alongside custom validator functions to set a custom error message if a field fails validation. This message will override any internally set messages
- `Warning Message` - An optional warning message to display incase the action may be *dangerous* or *important* in some way, and let the user know. (**Unsure if this is fully implemented on each component, if you find this is broken, feel free to update the code with any bugs found**)

## Input specifig parameters

### `<TextAreaInput />`

- `MaxLength` - Maximum length allowed for the input. It will automatically display a counter of $"{Length}/{Max}" so the user knows how many characters are available
- `AutoSizing` - This uses JavaScript to automatically scale the text input if the users types enough to go over the min-height. This will stop scaling once it reaches the max-height. (*Note:* This will effect the placement of content on the page and is not a smooth animation, but is somewhat nice to have)
- `MinHeight` - Minimum height for the text area (Default is set to `100`*px*)
- `MaxHeight` - Maximum height for the text area (Default is set to `500`*px*)

### `<NumberInput />`

- `Min` - Minimum amount for number to be
- `Max` - Maximum amount for number to be
- `HideButtons` - Boolean for hiding the up and down arrow buttons from the ui

### `<ImageInput />`

- `DefaultValue` - Set this to the base64 of the image to load a default value for it, since the `@bind-Value` is based on the `IBrowserFile` class and can't be loaded from the database

### `<FileInput />`

- `Type` - Type of file input, either `enumType.typMultipleFiles` or `typSingleFiles` or `typImage`. Not all of these types are implemented properly and it is recommended to leave it at the default value. *Feel free to implement the other types*
- `OnFileDrop` - Function to be used when a file is uploaded to handle processing it
- `MaxFileSizeMB` - Maximum file size that's allowed for uploading in `megabytes`, any file over this limit will throw an error on upload
- `AcceptedFileTypes` - Acceptable file types via `enum` values, defaultly set to `enumFileType.typAll`
