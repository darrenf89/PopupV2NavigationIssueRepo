using PopupV2NavigationIssueRepo.DataServices;

namespace PopupV2NavigationIssueRepo
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        private readonly IModalPopupService _modalPopupService;
        public MainPage(IModalPopupService modalPopupService)
        {
            InitializeComponent();
            this._modalPopupService = modalPopupService;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();


            if (_modalPopupService.CurrentPopup is not null)
                return;

            await Task.Delay(1000);
            var LoadingPage = new LoadingPageTwo();
            await _modalPopupService.ShowModalAsync<PopUpPage>(() => new PopUpPage(), LoadingPage);

            await Task.Delay(2000);

            await _modalPopupService.CloseModalAsync();
        }


        private async void OnCounterClicked(object? sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"///{nameof(Page2)}");
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
