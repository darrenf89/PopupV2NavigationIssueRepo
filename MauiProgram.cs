using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using PopupV2NavigationIssueRepo.DataServices;

namespace PopupV2NavigationIssueRepo
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            
            builder.Services.AddSingleton<IModalPopupService, ModalPopupService>();
            builder.Services.AddSingletonPopup<PopUpPage>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<Page2>();
            builder.Services.AddTransient<Page3>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
