Curacao.Mvvm
============

[![NuGet version](https://badge.fury.io/nu/curacao.mvvm.svg)](http://badge.fury.io/nu/curacao.mvvm)

[![Build status](https://ci.appveyor.com/api/projects/status/mro8s8ongggue0p7?svg=true)](https://ci.appveyor.com/project/asizikov/curacao-mvvm)

Yet another simple MVVM library for Windows Phone.

Was started as an experiment back in time when there was only [one way to perform navigation][OldNavigation] in WP:

```chsarp
NavigationService.Navigate(new Uri("/SecondPage.xaml", UriKind.Relative));
```

Obviously this is horrible: it's all `string` based, no types and compile time checks. We also had to manually construct query string.

Nowadays things has changed and we can navigate to strongly typed `Page`: 

```chsharp
Frame.Navigate(typeof(SecondPage), param);
``` 

Much better now, isn't it? 
* Compile time checks
* Parameters as `objects`, not as `strings`

Still feels not very clean for me. Typically navigation is initialized from `ViewMovel`, so you have to bring the knowlege about UI into your VM.

This library is an attempt to invert this dependency.

 You can annotate your view with `[DependsOnViewModel]` attribute, so that `NavigationService` can discover and bind all `Views` to `ViewModels`.
 
 ```csharp
 NavigationService.NavigateTo<Page1ViewModel, Data>(new Data{Id = 1, Value = "value"});
 ```
 
 That gives you better separation. View models can 'live' in separate assembly and have no references to views at all.
 

[OldNavigation]: (https://msdn.microsoft.com/en-us/library/windows/apps/ff626521(v=vs.105).aspx)