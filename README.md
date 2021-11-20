# CodeGuard

[![NuGet Status](https://img.shields.io/nuget/v/CodeGuard.Fody.svg)](https://www.nuget.org/packages/CodeGuard.Fody/)
[![GitHub release](https://img.shields.io/github/release/raltinsoy/CodeGuard.svg?logo=GitHub)](https://github.com/raltinsoy/CodeGuard/releases)

Hide the actual code.

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
