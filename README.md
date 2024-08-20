[![NuGet Package](https://img.shields.io/nuget/v/Validity.DataAnnotation)](https://www.nuget.org/packages/Validity.DataAnnotation/)


### Table of content
- [Installing](#Installation)
- [Usage](#Usage)
- [Result Message](#Result-Message)
- [Result Errors](#Result-Errors)

### Installation
  ```bash
  dotnet add package Resulver
  ```

### Usage
   ```csharp
    public Result<int> Sum(int a, int b)
    {
        var sum = a + b;

        return new Result<int>(sum);
    }

    public void Writer()
    {
        var result = Sum(3, 5);

        Console.WriteLine(result.Content);
    } 
   ```

### Result Message
```csharp
public Result<User> AddUser(User user)
{
    // implementation

    return new Result<User>(user, message: "User Created");
}

public void Writer()
{
    var user = new User();

    var result = AddUser(user);

    Console.WriteLine(result.Message);
}
```
### Result Errors
```csharp
public class UserNotFoundError() : ResultError(message: "User not found");
public class UserIdIsNotValidError() : ResultError(message:   "User ID is not valid");

public Result RemoveUser(int userId)
{
    //Implementation



    return new UserNotFoundError();

    //or for multiple errors
    return new Result(new UserNotFoundError(), new UserIdIsNotValidError());
}

public void Writer()
{
    var result = RemoveUser(1);

    if (result.IsFailure)
    {
        foreach (var error in result.Errors)
        {
            Console.WriteLine(error.Message);
        }
    }
}
```

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

