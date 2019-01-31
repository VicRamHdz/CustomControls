using System;
using System.Collections.Generic;
using CustomControls.ViewModels;
using Xamarin.Forms;

namespace CustomControls.Views
{
    public partial class Option2ContentView : ContentView
    {
        public Option2ContentView()
        {

        }

        public Option2ContentView(Option1ViewModel vm)
        {
            InitializeComponent();
            this.BindingContext = vm;
        }
    }
}
