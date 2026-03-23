# `<TabView />`

This component is a method of displaying multiple different sections in a single viewport (See `HALO/Profile` or `HALO/Community` pages for an example use-case)

## Usage

The `<TabView />` component looks at the query parameters in the URL for its page. If the parameter for `?tab=...` is present it will auto-navigate to the specified tab whose `Id` matches the query parameters value. Below is a sample code snippet:

```razor
...

<Button @onclick=@(() => fobjTabViewRef?.fncNavigateTo("FirstTab"))>
	First Tab
</Button>

...

<TabView OnFocusedTabChange=@StateHasChanged @ref=@fobjTabViewRef>
	<Tab Id="FirstTab" Default>
		...
	</Tab>
	<Tab Id="SecondTab">
		...
	</Tab>
</TabView>
```

The parameters for the `<TabView />` component are as follows:

### `<TabView />` Parameters

- `OnFocusedTabChange` - Set this to `OnFocusedTabChange=@StateHasChanged` to be sure it renders properly when a tab is switched
- `@ref` - Use a `ref` to this component to programatically switch tabs using the `ref?.fncNavigateTo()` function

### `<Tab />` Parameters

- `Id` - A unique id to identify the tab
- `Default` - Boolean and can be written as `<Table ShowSelectOption />` (leaving off the `=@(true)` doesn't effect it). This parameter should only be used once per `<TabView />` and will make it be the default one in focus