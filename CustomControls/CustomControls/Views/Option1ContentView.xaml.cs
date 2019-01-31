using System;
using System.Collections.Generic;
using CustomControls.ViewModels;
using Xamarin.Forms;

namespace CustomControls.Views
{
    public partial class Option1ContentView : ContentView
    {
        public Option1ContentView()
        {

        }

        public Option1ContentView(Option1ViewModel vm)
        {
            InitializeComponent();
            this.BindingContext = vm;
        }
    }
}
