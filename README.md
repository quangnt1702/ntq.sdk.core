# This is my personal nuget package

## NTQ.Sdk.Core [![NuGet Version](https://img.shields.io/nuget/v/NTQ.Sdk.Core)](https://www.nuget.org/packages/NTQ.Sdk.Core/)

`NTQ.Sdk.Core` is a support library that provides methods, utils, extensions for coding more effectively.

<img src="./NTQ.Sdk.Core/NTQ-Logo.png" style=" width:150px ; height:150px "  >

## Features

- Dynamic filter
- Dynamic sort
- Dynamic paging
- Swagger config
- Global Error Filter Config
- Utilities
- Code Generation with T4 Text Templates

## Get started

### Dynamic filter

`Dynamic filter` helps you filter a `Queryable` in generic using custom attributes.

Example:

```csharp
 public class CategoryViewModel
    {
        [Int] public int? Id { get; set; }
        [String] public string Name { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        [Boolean] public bool? Active { get; set; }
    }
```

```csharp
  CategoryViewModel filter=...;
  var result = _categoryRepository.Get(x => x.Active)
                .ProjectTo<CategoryViewModel>(_mapper.ConfigurationProvider)
                .DynamicFilter(filter);
```

### Dynamic sort

`Dynamic sort` helps you sort a `Queryable` in generic using custom attributes.

Example:

```csharp
 public class CategoryViewModel : SortModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? Active { get; set; }
    }
```

```csharp
 CategoryViewModel filter = ...;
 filter.SortDirection = ...; // asc or desc
 filter.SortProperty = ...; // name of a property you want to sort
 var result = _categoryRepository.Get(x => x.Active)
                .ProjectTo<CategoryViewModel>(_mapper.ConfigurationProvider)
                .DynamicSort(filter);
```

Copyright &copy; 2022 QuangNT
