# White Knight

![NuGet Version](https://img.shields.io/nuget/v/White.Knight?label=White.Knight)

![NuGet Version](https://img.shields.io/nuget/v/White.Knight.Injection.Abstractions?label=White.Knight.Injection.Abstractions)

![NuGet Version](https://img.shields.io/nuget/v/White.Knight.Tests.Abstractions?label=White.Knight.Tests.Abstractions)

[![nuget-core](https://github.com/gman-au/white-knight/actions/workflows/nuget-core.yml/badge.svg)](https://github.com/gman-au/white-knight/actions/workflows/nuget-core.yml)
[![nuget-injection-abstractions](https://github.com/gman-au/white-knight/actions/workflows/nuget-injection-abstractions.yml/badge.svg)](https://github.com/gman-au/white-knight/actions/workflows/nuget-injection-abstractions.yml)
[![nuget-tests-abstractions](https://github.com/gman-au/white-knight/actions/workflows/nuget-tests-abstractions.yml/badge.svg)](https://github.com/gman-au/white-knight/actions/workflows/nuget-tests-abstractions.yml)

## Summary
The purpose of this library is to create a distilled abstraction of both the [specification pattern](https://en.wikipedia.org/wiki/Specification_pattern) and the [repository pattern](https://www.geeksforgeeks.org/system-design/repository-design-pattern/) such that there can be a complete decoupling between business logic and a data store.
This infrastructure package encapsulates the data read/write operations to a data store via the `IRepository` interface.

The overall intention is to have repository 'logic' completely decoupled from database 'code' in the sense that the implementing code should be agnostic about the database or even data store mechanism involved. Were the technology to change, for example, in theory another `IRepository` implementation could be written with its own DI module, and literally 'hot-swapped' by a consuming codebase without any change to the code (other than the DI selection).

This package is suited to code-first domain design, as the repository implementations are typed against the domain POCOs.

## Implementations

* [White.Knight.Csv](https://github.com/gman-au/white-knight-csv)
* [White.Knight.Redis](https://github.com/gman-au/white-knight-redis)