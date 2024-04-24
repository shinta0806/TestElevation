using System.Diagnostics;
using System.Security.Principal;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;

namespace TestElevation.ViewModels;

public partial class MainViewModel : ObservableRecipient
{
	public MainViewModel()
	{
		if (IsAdminRole())
		{
			ButtonElevateVisibility = Visibility.Collapsed;
			TextBlockElevatedVisibility = Visibility.Visible;
		}
		else
		{
			ButtonElevateVisibility = Visibility.Visible;
			TextBlockElevatedVisibility = Visibility.Collapsed;
		}
		ButtonElevateClickedCommand = new RelayCommand(ButtonElevateClicked);
	}

	private Visibility _ButtonElevateVisibility;
	public Visibility ButtonElevateVisibility
	{
		get => _ButtonElevateVisibility;
		set => SetProperty(ref _ButtonElevateVisibility, value);
	}

	private Visibility _textBlockElevatedVisibility;
	public Visibility TextBlockElevatedVisibility
	{
		get => _textBlockElevatedVisibility;
		set => SetProperty(ref _textBlockElevatedVisibility, value);
	}

	#region ButtonElevate
	public RelayCommand ButtonElevateClickedCommand
	{
		get;
	}

	private void ButtonElevateClicked()
	{
		try
		{
			ProcessStartInfo startInfo = new()
			{
				UseShellExecute = true,
				FileName = Path.ChangeExtension(Environment.GetCommandLineArgs()[0], ".exe"),
				Verb = "runas",
			};
			Process.Start(startInfo);
		}
		catch (Exception ex)
		{
			Debug.WriteLine("err: " + ex.Message);
		}
	}
	#endregion

	private static Boolean IsAdminRole()
	{
		WindowsPrincipal windowsPrincipal = new(WindowsIdentity.GetCurrent());
		return windowsPrincipal.IsInRole(WindowsBuiltInRole.Administrator);
	}

}
