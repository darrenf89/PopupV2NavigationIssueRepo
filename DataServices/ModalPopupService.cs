using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Maui.Views;

namespace PopupV2NavigationIssueRepo.DataServices;

public class ModalPopupService : IModalPopupService
{
    public Popup? CurrentPopup { get; private set; }
    public Type? CurrentPopupType => CurrentPopup?.GetType();

    private readonly IPopupService _ps;
    public IPopupService PopupService => _ps;

    private CancellationTokenSource? _popupTimeoutCts;
    private TimeSpan _popupTimeout;

    public ModalPopupService(IPopupService ps)
    {
        _ps = ps;
    }

    public bool IsPopupShown<TPopup>() where TPopup : Popup =>
        CurrentPopupType == (typeof(TPopup));

    public async Task ShowAsync<TPopup>(Func<TPopup> popupFactory, PopupOptions? options = null, TimeSpan? popupTimeout = null) where TPopup : Popup
    {
        if (CurrentPopupType == typeof(TPopup))
        {
            return;
        }

        if(CurrentPopup is not null)
        {
            await CurrentPopup.CloseAsync();
            await Task.Yield();
        }

        var popup = popupFactory();

        options ??= new PopupOptions
        {
            CanBeDismissedByTappingOutsideOfPopup = false,
            PageOverlayColor = Color.FromArgb("#80000000")
        };

#if ANDROID
        var navColor = Platform.CurrentActivity?.Window?.NavigationBarColor;
#endif

        _ = Shell.Current.ShowPopupAsync(popup, options);
        await Task.Delay(50);

#if ANDROID
        var navColor2 = Platform.CurrentActivity?.Window?.NavigationBarColor;
#endif

        CurrentPopup = popup;
        return;
    }

    public async Task ShowModalAsync<TPopup>(Func<TPopup> popupFactory, LoadingPageTwo LoadingPage, PopupOptions? options = null, TimeSpan? popupTimeout = null) where TPopup : Popup
    {
        if (CurrentPopupType == typeof(TPopup))
        {
            return;
        }

        var popup = popupFactory();

        options ??= new PopupOptions
        {
            CanBeDismissedByTappingOutsideOfPopup = false,
            PageOverlayColor = Color.FromArgb("#80000000")
        };

        CurrentPopup = popup;

#if ANDROID
        var navColor = Platform.CurrentActivity?.Window?.NavigationBarColor;
#endif
        await Application.Current.Windows[0].Navigation.PushModalAsync(LoadingPage, true);
        await Task.Delay(500);

#if ANDROID
        var navColor2 = Platform.CurrentActivity?.Window?.NavigationBarColor;
#endif
        
        return;
    }

    public async Task CloseModalAsync()
    {        
        if(Application.Current.Windows[0].Navigation.ModalStack.Count > 0)
        {
            await Application.Current.Windows[0].Navigation.PopModalAsync();            
        }
        CurrentPopup = null;
        return;
    }

    public async Task CloseAsync<TPopup>() where TPopup : Popup
    {
        if (CurrentPopupType != typeof(TPopup))
            return;

        _popupTimeoutCts?.Cancel();
        _popupTimeoutCts = null;

        await CurrentPopup.CloseAsync();

        await Task.Yield();
        CurrentPopup = null;

        return;
    }

    public void Clear()
    {
        _popupTimeoutCts?.Cancel();
        _popupTimeoutCts = null;
        CurrentPopup = null;

    }

}