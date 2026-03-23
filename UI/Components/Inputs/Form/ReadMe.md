# `<Form />`

This form component will wrap inputs and handle validation and submittion logic. Global form validation can be performed, along with input specific (see each input component). Each input element has default validation that runs as well, and if any of these fail the form will not submit.

## Usage

An example usage of the form is below:

```razor
<Form OnValidate=@... OnValidateAsync=@... OnSubmit=@...>
	<TextInput ... @bind-Value=@fobj />
	<SubmitButton>Submit</SubmitButton>
</Form>
```

### Parameters

- `OnSubmit` - Standard or async function to be executed upon form submission. This function will only run if internal and OnValidate/Async each return `true`
- `OnValidate`/`OnValidateAsync` - Custom validation functions to be ran upon hitting submit on the form. `OnValidate` is used for basic text validation while `OnValidateAsync` can be used to perform async operations like an API call or Database query to validate the data.
- `pdictAttributes` - You may see this in the parameter list. It is there as a catch-all and passes all extra attributes to the `<form>` (html element) incase you want to specify a `class` or `style`.