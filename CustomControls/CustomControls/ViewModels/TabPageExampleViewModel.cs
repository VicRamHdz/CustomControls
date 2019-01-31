using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomControls.Views;
using Xamarin.Forms;

namespace CustomControls.ViewModels
{
    public class TabPageExampleViewModel
    {
        public List<TabItemPage> Pages { get; set; }

        public Command TestCommand { get; set; }

        public TabPageExampleViewModel()
        {
            TestCommand = new Command(() => { OnTestCommand(); });

            Pages = new List<TabItemPage>()
            {
                new TabItemPage
                {
                    Title = "Option 1",
                    IconName = "option1.svg",
                    IconActiveName = "option1active.svg",
                    Content = Activator.CreateInstance(typeof(Option1ContentView), new Option1ViewModel()) as ContentView,
                },
                new TabItemPage
                {
                    Title = "Option 2",
                    IconName = "option1.svg",
                    IconActiveName = "option1active.svg",
                    Content = Activator.CreateInstance(typeof(Option2ContentView), new Option1ViewModel()) as ContentView,
                },
                new TabItemPage
                {
                    Title = "Option 3",
                    IconName = "option1.svg",
                    IconActiveName = "option1active.svg",
                    Content = null,
                    Clicked = TestCommand
                }
            };
        }

        void OnTestCommand()
        {
            try
            {
                MessagingCenter.Send<string, string>("Test", "DisplayMessage", "Hello world!");
            }
            catch (Exception ex)
            {
                MessagingCenter.Send<string, string>("Test", "DisplayMessage", $"An exception: { ex.Message }");
            }
        }
    }
}
