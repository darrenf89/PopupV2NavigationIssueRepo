using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Views;

namespace PopupV2NavigationIssueRepo.DataServices
{
    public interface IModalPopupService
    {
        Popup? CurrentPopup { get; }
        Type? CurrentPopupType { get; }
        IPopupService PopupService { get; }

        bool IsPopupShown<TPopup>() where TPopup : Popup;
        Task ShowAsync<TPopup>(Func<TPopup> popupFactory, PopupOptions? options = null, TimeSpan? popupTimeout = null) where TPopup : Popup;
        Task ShowModalAsync<TPopup>(Func<TPopup> popupFactory, Page2 LoadingPage, PopupOptions? options = null, TimeSpan? popupTimeout = null) where TPopup : Popup;
        Task CloseAsync<TPopup>() where TPopup : Popup;
        Task CloseModalAsync();
        void Clear();
    }
}