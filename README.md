## Getting started
### 1. Install package
  ```bash
  dotnet add package AutoValidator.DataAnnotation
  ```


### 2. Create your class
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

  ### 3. Validate your object
   ```csharp
   var myObject = new MyClass
   {
      PhoneNumber = "invalid phone number for test",
      Nested = new NestedClass { Title = "invalid title for test" }
   };

   var validationResult = NestedObjectValidator.Validate(myObject);

  // Show validation error messages
  foreach (var result in validationResult)
  {
      Console.WriteLine(result.ErrorMessage);
  }

  //Result:
  //The PhoneNumber field is not a valid phone number.
  //The field Title must be a string or array type with a maximum length of '5'.
   ```
