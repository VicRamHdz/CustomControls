using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using FFImageLoading.Forms;
using FFImageLoading.Svg.Forms;
using Xamarin.Forms;

namespace CustomControls
{
    public class CustomTabPage : Grid
    {
        private Grid _contentPage = new Grid
        {
            VerticalOptions = LayoutOptions.FillAndExpand,
            BackgroundColor = Color.White
        };

        public static readonly BindableProperty PagesProperty = BindableProperty.Create(nameof(Pages), typeof(IList<TabItemPage>), typeof(CustomTabPage), null, BindingMode.OneWay);

        public IList<TabItemPage> Pages
        {
            get { return (IList<TabItemPage>)GetValue(PagesProperty); }
            set { SetValue(PagesProperty, value); }
        }

        public static readonly BindableProperty StartBackgroundColorProperty = BindableProperty.Create(nameof(StartBackgroundColor), typeof(Color), typeof(CustomTabPage), Color.Red, BindingMode.OneWay);

        public Color StartBackgroundColor
        {
            get { return (Color)GetValue(StartBackgroundColorProperty); }
            set { SetValue(StartBackgroundColorProperty, value); }
        }

        public static readonly BindableProperty EndBackgroundColorProperty = BindableProperty.Create(nameof(EndBackgroundColor), typeof(Color), typeof(CustomTabPage), Color.Yellow, BindingMode.OneWay);

        public Color EndBackgroundColor
        {
            get { return (Color)GetValue(EndBackgroundColorProperty); }
            set { SetValue(EndBackgroundColorProperty, value); }
        }

        public static readonly BindableProperty StartActiveBackgroundColorProperty = BindableProperty.Create(nameof(StartActiveBackgroundColor), typeof(Color), typeof(CustomTabPage), Color.Green, BindingMode.OneWay);

        public Color StartActiveBackgroundColor
        {
            get { return (Color)GetValue(StartActiveBackgroundColorProperty); }
            set { SetValue(StartActiveBackgroundColorProperty, value); }
        }

        public static readonly BindableProperty EndActiveBackgroundColorProperty = BindableProperty.Create(nameof(EndActiveBackgroundColor), typeof(Color), typeof(CustomTabPage), Color.White, BindingMode.OneWay);

        public Color EndActiveBackgroundColor
        {
            get { return (Color)GetValue(EndActiveBackgroundColorProperty); }
            set { SetValue(EndActiveBackgroundColorProperty, value); }
        }

        public static readonly BindableProperty FullUnderlineColorProperty = BindableProperty.Create(nameof(FullUnderlineColor), typeof(Color), typeof(CustomTabPage), Color.FromHex("#DBDBDB"), BindingMode.OneWay);

        public Color FullUnderlineColor
        {
            get { return (Color)GetValue(FullUnderlineColorProperty); }
            set { SetValue(FullUnderlineColorProperty, value); }
        }

        public static readonly BindableProperty HasUnderlineProperty = BindableProperty.Create(nameof(HasUnderline), typeof(bool), typeof(CustomTabPage), true, BindingMode.OneWay);

        public bool HasUnderline
        {
            get { return (bool)GetValue(HasUnderlineProperty); }
            set { SetValue(HasUnderlineProperty, value); }
        }

        public static readonly BindableProperty TitleFontSizeProperty = BindableProperty.Create(nameof(TitleFontSize), typeof(double), typeof(CustomTabPage), 16d, BindingMode.OneWay);

        public double TitleFontSize
        {
            get { return (double)GetValue(TitleFontSizeProperty); }
            set { SetValue(TitleFontSizeProperty, value); }
        }

        public static readonly BindableProperty TitleFontAttributesProperty = BindableProperty.Create(nameof(TitleFontAttributes), typeof(FontAttributes), typeof(CustomTabPage), FontAttributes.None, BindingMode.OneWay);

        public FontAttributes TitleFontAttributes
        {
            get { return (FontAttributes)GetValue(TitleFontAttributesProperty); }
            set { SetValue(TitleFontAttributesProperty, value); }
        }

        public static readonly BindableProperty TitleTextColorProperty = BindableProperty.Create(nameof(TitleTextColor), typeof(Color), typeof(CustomTabPage), Color.Gray, BindingMode.OneWay);

        public Color TitleTextColor
        {
            get { return (Color)GetValue(TitleTextColorProperty); }
            set { SetValue(TitleTextColorProperty, value); }
        }

        public static readonly BindableProperty TitleActiveTextColorProperty = BindableProperty.Create(nameof(TitleActiveTextColor), typeof(Color), typeof(CustomTabPage), Color.Green, BindingMode.OneWay);

        public Color TitleActiveTextColor
        {
            get { return (Color)GetValue(TitleActiveTextColorProperty); }
            set { SetValue(TitleActiveTextColorProperty, value); }
        }

        public static readonly BindableProperty TitleHeightProperty = BindableProperty.Create(nameof(TitleHeight), typeof(GridLength), typeof(CustomTabPage), new GridLength(10, GridUnitType.Star), BindingMode.OneWay);

        public GridLength TitleHeight
        {
            get { return (GridLength)GetValue(TitleHeightProperty); }
            set { SetValue(TitleHeightProperty, value); }
        }

        public static readonly BindableProperty ButtonsPositionProperty = BindableProperty.Create(nameof(ButtonsPosition), typeof(ButtonsPosition), typeof(CustomTabPage), null, BindingMode.OneWay);

        public ButtonsPosition ButtonsPosition
        {
            get { return (ButtonsPosition)GetValue(ButtonsPositionProperty); }
            set { SetValue(ButtonsPositionProperty, value); }
        }

        public static readonly BindableProperty HasBorderButtonProperty = BindableProperty.Create(nameof(HasBorderButton), typeof(bool), typeof(CustomTabPage), false, BindingMode.OneWay);

        public bool HasBorderButton
        {
            get { return (bool)GetValue(HasBorderButtonProperty); }
            set { SetValue(HasBorderButtonProperty, value); }
        }

        public static readonly BindableProperty IconPositionProperty = BindableProperty.Create(nameof(IconPosition), typeof(IconPosition), typeof(CustomTabPage), IconPosition.Left, BindingMode.OneWay);

        public IconPosition IconPosition
        {
            get { return (IconPosition)GetValue(IconPositionProperty); }
            set { SetValue(IconPositionProperty, value); }
        }

        public ICommand OnAppearingCommand { get; set; }
        public ICommand SelectedPageChanged { get; set; }
        public ICommand ChangeIndexPage { get; set; }

        public CustomTabPage()
        {
            ColumnSpacing = 0;
            RowSpacing = 0;
            ChangeIndexPage = new Command(async (index) => { await OnChangeIndexPage(int.Parse(index.ToString())); });
        }

        private int buttonPosition = 0;
        private int linePosition = 1;
        private int tabsPosition = 2;
        private readonly FFImageLoading.Svg.Forms.SvgImageSourceConverter _imageSourceConverter = new SvgImageSourceConverter();

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == ButtonsPositionProperty.PropertyName)
            {
                if (ButtonsPosition == ButtonsPosition.Up)
                {
                    buttonPosition = 0;
                    linePosition = 1;
                    tabsPosition = 2;
                    this.Margin = new Thickness(0, 30, 0, 0);
                    RowDefinitions.Add(new RowDefinition() { Height = 60 });
                    RowDefinitions.Add(new RowDefinition() { Height = 1 });
                    RowDefinitions.Add(new RowDefinition() { Height = new GridLength(90, GridUnitType.Star) });
                }
                else if (ButtonsPosition == ButtonsPosition.Down)
                {
                    buttonPosition = 2;
                    linePosition = 1;
                    tabsPosition = 0;
                    RowDefinitions.Add(new RowDefinition() { Height = new GridLength(90, GridUnitType.Star) });
                    RowDefinitions.Add(new RowDefinition() { Height = 1 });
                    RowDefinitions.Add(new RowDefinition() { Height = 60 });
                }
                return;
            }

            if (propertyName == PagesProperty.PropertyName)
            {
                AddPages();
                return;
            }

            if (propertyName == TitleFontSizeProperty.PropertyName)
            {
                var tabs = this.Children.Where(tb => tb.StyleId != null && tb.StyleId.Contains("tabbutton")).ToList();
                foreach (StackLayout tb in tabs)
                {
                    var title = tb.Children.FirstOrDefault(lbl => lbl is Label) as Label;
                    title.FontSize = TitleFontSize;
                }
                return;
            }

            if (propertyName == TitleHeightProperty.PropertyName)
            {
                this.RowDefinitions.FirstOrDefault().Height = TitleHeight;
                return;
            }
        }

        private async Task OnChangeIndexPage(int index)
        {
            if (index > 0 && Pages != null)
            {
                //Find a page from a given index
                var page = Pages.FirstOrDefault(i => i.Index == index);
                if (page != null)
                    await OnTappedTabButton(page);
            }
        }

        private async Task OnTappedTabButton(TabItemPage page)
        {
            if (page.Content == null)
            {
                page.Clicked?.Execute(null);
            }
            else
            {
                await ShowPage(page.Index);
                SelectedPageChanged?.Execute(page);
                await TabAnimation(page);
            }
        }

        private bool wasBuild = false;

        private async void AddPages()
        {
            if (Pages == null || wasBuild)
                return;

            wasBuild = true;

            int WidthTabs = 0;
            int Increment = 0;
            bool IsOdd = Pages.Count % 2 > 0;
            if (IsOdd)
            {
                WidthTabs = Convert.ToInt32(Math.Floor(decimal.Parse((100 / Pages.Count).ToString())));
                Increment = (100 - (WidthTabs * Pages.Count)) / 2;
            }
            else
            {
                WidthTabs = 100 / Pages.Count;
            }
            int index = 0;
            foreach (var tabs in Pages)
            {
                tabs.Index = index;
                if (index == 0 || index == Pages.Count - 1)
                {
                    ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(WidthTabs + Increment, GridUnitType.Star) });
                }
                else
                {
                    ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(WidthTabs, GridUnitType.Star) });
                }

                Label tab = new Label()
                {
                    Text = tabs.Title,
                    TextColor = TitleTextColor,
                    FontSize = TitleFontSize,
                    FontAttributes = TitleFontAttributes,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand
                };
                CustomFrame tabButton = new CustomFrame() { HasShadow = false, StartColor = StartBackgroundColor, EndColor = EndBackgroundColor, Padding = 2, MinimumHeightRequest = 20, HeightRequest = 20 };

                StackLayout buttonArea = new StackLayout() { Spacing = 1 };
                buttonArea.Orientation = IconPosition == IconPosition.Up ? StackOrientation.Vertical : StackOrientation.Horizontal;

                if (!string.IsNullOrEmpty(tabs.IconName))
                {
                    var iconImage = default(View);
                    if (tabs.IconName.Contains("svg"))
                    {
                        var xfSource = _imageSourceConverter.ConvertFromInvariantString(tabs.IconName) as ImageSource;
                        iconImage = new SvgCachedImage()
                        {
                            WidthRequest = 26,
                            HeightRequest = 26,
                            HorizontalOptions = LayoutOptions.CenterAndExpand,
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            Source = new SvgImageSource(xfSource, 0, 0, true)
                        };
                    }
                    else
                    {
                        iconImage = new CachedImage()
                        {
                            WidthRequest = 40,
                            HeightRequest = 40,
                            HorizontalOptions = LayoutOptions.CenterAndExpand,
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            Source = ImageSource.FromFile(tabs.IconName)
                        };
                    }
                    buttonArea.Children.Add(iconImage);
                }

                buttonArea.Children.Add(tab);

                tabButton.StyleId = $"tabbutton{tabs.Index}";
                tabButton.GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(async () => { await OnTappedTabButton(tabs); }) });
                tabButton.Content = buttonArea;
                //tabButton.Children.Add(tab);
                this.Children.Add(tabButton, index, buttonPosition);

                if (tabs.Content != null)
                    tabs.Content.IsVisible = false;

                //adding content to content page
                if (tabs.Content != null)
                {
                    _contentPage.Children.Add(tabs.Content);
                }
                else
                {
                    _contentPage.Children.Add(new StackLayout());
                }
                index++;
            }

            //Adding full underline
            BoxView underline = new BoxView()
            {
                HeightRequest = 1,
                Color = FullUnderlineColor,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                //VerticalOptions = LayoutOptions.FillAndExpand 
            };
            this.Children.Add(underline, 0, linePosition);
            Grid.SetColumnSpan(underline, Pages.Count);

            //Adding content pages
            this.Children.Add(_contentPage, 0, tabsPosition);
            Grid.SetColumnSpan(_contentPage, Pages.Count);

            //Select first tab by default
            await OnTappedTabButton(Pages.FirstOrDefault());
        }

        private async Task TabAnimation(TabItemPage page)
        {
            if (HasUnderline)
            {
                var prevunderline = this.Children.FirstOrDefault(x => x.AutomationId == "underlineitem");

                //Adding underline on item
                BoxView underline = new BoxView()
                {
                    AutomationId = "underlineitem",
                    HeightRequest = 1,
                    StyleId = page.Index.ToString(),
                    Color = TitleActiveTextColor,
                    WidthRequest = 70,
                    Opacity = 0,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.EndAndExpand
                };

                //remove previous underline and animate it
                async Task<bool> AnimatePrev()
                {
                    if (prevunderline != null)
                    {
                        int spaces = (int.Parse(underline.StyleId) - int.Parse(prevunderline.StyleId)) * 100;
                        await prevunderline.TranslateTo(spaces, 0, 200, Easing.SinOut);
                        return this.Children.Remove(prevunderline);
                    }
                    else
                    {
                        return await Task.FromResult(true);
                    }
                };

                //add a new underline and animate it
                async Task<bool> AnimateAddUnderline()
                {
                    this.Children.Add(underline, page.Index, 1);
                    return await underline.FadeTo(100, 1000, Easing.SinIn);
                };

                //runs the two actions at the same time
                await Task.WhenAny<bool>
                (
                      AnimatePrev(),
                      AnimateAddUnderline()
                );
            }

            //highligth tab button title
            var tabs = this.Children.Where(tb => tb.StyleId != null && tb.StyleId.Contains("tabbutton")).ToList();
            //foreach (StackLayout tb in tabs)
            foreach (CustomFrame tb in tabs)
            {
                var buttonarea = tb.Content as StackLayout;
                var title = buttonarea.Children.FirstOrDefault(lbl => lbl is Label) as Label;

                if (tb.StyleId == $"tabbutton{page.Index}")
                {
                    title.TextColor = TitleActiveTextColor;
                    if (page.IconActiveName.Contains("svg"))
                    {
                        var icon = buttonarea.Children.FirstOrDefault(ico => ico is SvgCachedImage) as SvgCachedImage;
                        var xfSource = _imageSourceConverter.ConvertFromInvariantString(page.IconActiveName) as ImageSource;
                        icon.Source = new SvgImageSource(xfSource, 0, 0, true);
                    }
                    else
                    {
                        var icon = buttonarea.Children.FirstOrDefault(ico => ico is CachedImage) as CachedImage;
                        icon.Source = ImageSource.FromFile(page.IconActiveName);
                    }
                    tb.StartColor = StartActiveBackgroundColor;
                    tb.EndColor = EndActiveBackgroundColor;
                }
                else
                {
                    title.TextColor = TitleTextColor;
                    var iconnormal = Pages.FirstOrDefault(ico => ico.Index == int.Parse(tb.StyleId.Replace("tabbutton", string.Empty)));
                    if (iconnormal.IconName.Contains("svg"))
                    {
                        var icon = buttonarea.Children.FirstOrDefault(ico => ico is SvgCachedImage) as SvgCachedImage;
                        var xfSource = _imageSourceConverter.ConvertFromInvariantString(iconnormal.IconName) as ImageSource;
                        icon.Source = new SvgImageSource(xfSource, 0, 0, true);
                    }
                    else
                    {
                        var icon = buttonarea.Children.FirstOrDefault(ico => ico is CachedImage) as CachedImage;
                        icon.Source = ImageSource.FromFile(iconnormal.IconName);
                    }
                    tb.StartColor = StartBackgroundColor;
                    tb.EndColor = EndBackgroundColor;
                }
            }
        }

        private async Task ShowPage(int Index)
        {
            if (_contentPage == null || _contentPage.Children.Count() == 0) return;

            //hiding all pages
            foreach (var p in _contentPage.Children)
            {
                if (p.IsVisible)
                {
                    await p.FadeTo(0, 200);
                    p.IsVisible = false;
                }
            }

            ContentView selectedPage = (ContentView)_contentPage.Children[Index];
            selectedPage.IsVisible = true;
            await selectedPage.FadeTo(1, 200);
        }
    }

    public enum ButtonsPosition
    {
        Up = 1,
        Down = 2
    }

    public enum IconPosition
    {
        Left = 1,
        Up = 2
    }

    public class TabItemPage
    {
        public TabItemPage()
        {

        }

        public int Index
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public string IconName
        {
            get;
            set;
        }

        public string IconActiveName
        {
            get;
            set;
        }

        public ContentView Content
        {
            get;
            set;
        }

        public ICommand Clicked
        {
            get;
            set;
        }
    }
}
