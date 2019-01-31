using System;
using System.Collections.Generic;
using CustomControls.ViewModels;
using Xamarin.Forms;

namespace CustomControls.Views
{
    public partial class TabPageExample : ContentPage
    {
        public TabPageExample()
        {
            InitializeComponent();
            this.BindingContext = new TabPageExampleViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<string, string>(this, "DisplayMessage", (title, message) =>
            {
                DisplayAlert(title, message, "OK");
            });
        }
    }
}
