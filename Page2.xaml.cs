using PopupV2NavigationIssueRepo.DataServices;

namespace PopupV2NavigationIssueRepo
{
    public partial class Page2 : ContentPage
    {
        int count = 0;
        private readonly IModalPopupService _modalPopupService;

        public Page2(IModalPopupService modalPopupService)
        {
            InitializeComponent();
            _modalPopupService = modalPopupService;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();


            if (_modalPopupService.CurrentPopup is not null)
                return;

            await _modalPopupService.ShowAsync<PopUpPage>(() => new PopUpPage());

            await Task.Delay(500);

            await _modalPopupService.CloseAsync<PopUpPage>();
        }

        private async void OnCounterClicked(object? sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"///{nameof(MainPage)}");
        }

        private async void OnCounterClicked3(object? sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"///{nameof(Page3)}");
        }
        private async void OnCounterClicked4(object? sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"/{nameof(Page4)}");
        }
    }
}
