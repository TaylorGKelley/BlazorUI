The Toast service can be used like so:

```csharp
[Inject] private cToastService mobjToastService { get; set; }

// {... inside method ...}
mobjToastService.fncToast(enmToastType.typ..., "title", "content");
```