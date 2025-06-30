namespace PopupV2NavigationIssueRepo
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute($"{nameof(Page4)}", typeof(Page4));


        }
    }
}
