1. Create MyWpfApp.csproj
2. Add Prism.Unity via NuGet
	- This will also install dependencies such as Prism.Wpf and Prism.Core
3. Remove `StartupUri="MainWindow.xaml"` in App.xaml
4. Add Bootstrapper.cs
5. Override `OnStartup` in App.xaml.cs
6. Add MainWindowViewModel.cs
7. Edit Maindow.xaml
	- Add to attributes:
		- `xmlns:prism="http://prismlibrary.com/"`
		- `prism:ViewModelLocator.AutoWireViewModel="True"`
	- Edit content to display MainWindowViewModel.cs