# `<Select />`

> Data Type: `System.Collections.Generic.List<TValue>` (`TValue` is a generic parameter and is derived from the value specified in whatever objectis bound to the `@bind-Value` parameter. It can be most data types available in c#)

This is a custom select component that allows for stylized options or for multiple options to be selected

## Usage

Below is a snippet of how to use this custom `<Select />` Component:

```razor
<Select MultiSelect SelectAllOption @bind-Value=@falobjSelectedValues>
	@foreach (cValue objValue in alobjValues) {
		<Option Value=@objValue.Value>@alobjValue.DisplayText</Option>
	}
</Select>
```

Also, multi-select with searchbar to filter options:

```razor
<Select MultiSelect SelectAllOption @bind-Value=@falobjSelectedValues>
	@foreach (cValue objValue in alobjValues) {
		<Option Value=@objValue.Value>@alobjValue.DisplayText</Option>
	}
</Select>
```