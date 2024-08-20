### 1. Install package
  ```bash
  dotnet add package AutoValidator.DataAnnotation
  ```


### 2. Create Class
 ```csharp
 public class MyClass
 {
      [Phone]
      public string PhoneNumber { get; set; }
      public NestedClass Nested { get; set; }
 }

 public class NestedClass
 {
      [MaxLength(5)]
      public string Title { get; set; }
 }
 ```

### 3. Add validator to service container
in Program.cs
```csharp
 builder.Services.AddObjectValidator();
```

### 4. Inject validator to yout controller
```csharp
[ApiController]
[Route("api/[controller]")]
public class MyController : ControllerBase
{
    readonly IObjectValidator _validator;

    public MyController(IObjectValidator validator)
    {
        _validator = validator;
    }
}
```

### 5. Use validator
```csharp
[HttpPost]
public IActionResult AddUser(MyClass myClass)
{
    var validationErrors = _validator.Validate(myClass);

    if (validationErrors.Count != 0)
    {
        //DO SOMETHING
    }
}
```