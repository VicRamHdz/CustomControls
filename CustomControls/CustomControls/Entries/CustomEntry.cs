using System;
using Xamarin.Forms;

namespace CustomControls
{
    public class CustomEntry : Entry
    {
        #region Bindable Properties

        public static readonly BindableProperty CustomBorderTypeProperty
        = BindableProperty.Create(nameof(CustomBorderType), typeof(BorderTypes),
            typeof(CustomEntry), BorderTypes.Underline, BindingMode.OneWay);

        public BorderTypes CustomBorderType
        {
            get { return (BorderTypes)GetValue(CustomBorderTypeProperty); }
            set { SetValue(CustomBorderTypeProperty, value); }
        }

        public static readonly BindableProperty CustomBorderColorProperty
        = BindableProperty.Create(nameof(CustomBorderColor), typeof(Color),
            typeof(CustomEntry), Color.Black, BindingMode.OneWay);

        public Color CustomBorderColor
        {
            get { return (Color)GetValue(CustomBorderColorProperty); }
            set { SetValue(CustomBorderColorProperty, value); }
        }

        #endregion

        #region Enums

        public enum BorderTypes
        {
            Underline,
            Rounded
        }

        #endregion

        #region Constructor

        public CustomEntry()
        {
        }

        #endregion
    }
}
