# Code Guard

[![NuGet Status](https://img.shields.io/nuget/v/CodeGuard.Fody.svg)](https://www.nuget.org/packages/CodeGuard.Fody/)
[![GitHub release](https://img.shields.io/github/release/raltinsoy/CodeGuard.svg?logo=GitHub)](https://github.com/raltinsoy/CodeGuard/releases)

Hide the actual code. In other words, SDK generator. This removes unavailable codes and removes their contents. So, your code can be referenced as dependency on another project, but it prevents your code from leaking.

This is an add-in for [Fody](https://github.com/Fody/Fody/).

## Installation

- Install the NuGet packages [`Fody`](https://www.nuget.org/packages/Fody) and [`CodeGuard.Fody`](https://www.nuget.org/packages/CodeGuard.Fody).

  ```powershell
  PM> Install-Package Fody
  PM> Install-Package CodeGuard.Fody
  ```

- If you already have a `FodyWeavers.xml` file in the root directory of your project, add the `<CodeGuard />` tag there. This file will be created on the first build if it doesn't exist:

  ```XML
  <?xml version="1.0" encoding="utf-8" ?>
  <Weavers>
    <CodeGuard />
  </Weavers>
  ```

See [Fody usage](https://github.com/Fody/Home/blob/master/pages/usage.md) for general guidelines, and [Fody Configuration](https://github.com/Fody/Home/blob/master/pages/configuration.md) for additional options.

## Your Code
```c#
public class Person
{
    private readonly int _id;

    public string Name { get; set; }

    public Person(int id)
    {
        _id = id;
        Name = "Default";
    }

    public string GetKey()
    {
        return Name + " #" + _id;
    }

    private void CalculateSecretCode()
    {
        int hash = 123;
        hash += _id;
    }
}
```

## What gets compiled

- Removes unusable properties, fields and functions.
- Removes the content of functions.

```c#
public class Person
{
    public string Name
    {
        [CompilerGenerated]
        get
        {
            throw new SDKExportException();
        }
        [CompilerGenerated]
        set
        {
            throw new SDKExportException();
        }
    }
    
    public Person(int id)
    {
        throw new SDKExportException();
    }
    
    public string GetKey()
    {
        throw new SDKExportException();
    }
}
```

## Configurability
### General Configs in FodyWeavers.xml

- `AddExceptionToCtor` if true, inserts an exception to constructor, otherwise just removes the content.
- `CleanResources` if true, removes resources like MainWindow.baml

### Specific Attributes

- `[MakeVisible]` if true, avoids removing the unusable objects.
- `[DoNotClearBody]` if true, avoids removing the content.
- `[DoNotThrowException]` if true, avoids inserting an exception.
