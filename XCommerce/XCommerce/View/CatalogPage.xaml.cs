using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XCommerce
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CatalogPage : ContentPage
    {
        private CatalogViewModel vm = new CatalogViewModel();
        public CatalogPage()
        {
            InitializeComponent();
            BindingContext = vm;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await vm.Load();
        }

        private void OnChartClicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new UploadPage());
        }

        private void OnChatClicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new ChatPage());
        }
    }
}