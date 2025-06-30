using CommunityToolkit.Maui.Views;

namespace PopupV2NavigationIssueRepo;

public partial class PopUpPage : Popup
{    
	public PopUpPage()
	{
		InitializeComponent();
		MainGrid.HeightRequest = DeviceDisplay.Current.MainDisplayInfo.Height / DeviceDisplay.Current.MainDisplayInfo.Density;
		MainGrid.WidthRequest = DeviceDisplay.Current.MainDisplayInfo.Width / DeviceDisplay.Current.MainDisplayInfo.Density;
        border.WidthRequest = (DeviceDisplay.Current.MainDisplayInfo.Width / DeviceDisplay.Current.MainDisplayInfo.Density) - 80;
    }

}