using System;
using System.Threading.Tasks;
using KCinema.model;
using KCinema.service;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KCinema.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContentEntityPage
    {
        private new Content Content { get; }

        public ContentEntityPage(Content content)
        {
            BindingContext = this;
            InitializeComponent();
            Content = content;
        }

        private async void OnContentDelete(object sender, EventArgs e)
        {
            var token = DeleteContent();
            if (token.ToString().Equals("")) return;

            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
            await Navigation.PushAsync(new ContentCatalogPage());
        }

        private async Task DeleteContent()
        {
            await new ContentRepo().DeleteContent(Content);
        }

        private async void OnEditContent(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ContentRedactor(false), true);
        }
    }
}