using System;
using CoreAnimation;
using CoreGraphics;
using CustomControls;
using CustomControls.iOS.Renderers.Entries;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace CustomControls.iOS.Renderers.Entries
{
    public class CustomEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                //Control.BorderStyle = UITextBorderStyle.None;

                var view = (Element as CustomEntry);

                if (view.CustomBorderType == CustomEntry.BorderTypes.Rounded)
                {
                    Control.BorderStyle = UITextBorderStyle.RoundedRect;
                    //Control.TintColor = view.CustomBorderColor.ToUIColor();
                    //view.BorderColor = 
                    Control.Layer.BorderColor = view.CustomBorderColor.ToCGColor();
                    Control.Layer.BorderWidth = 2.0f;
                }
                else
                {
                    Control.BorderStyle = UITextBorderStyle.None;
                    //changing disable textcolor to active textcolor 
                    if (Control.Enabled == false)
                    {
                        Control.AttributedText = new NSAttributedString(Control.Text, new UIStringAttributes { ForegroundColor = view.TextColor.ToUIColor() });
                    }
                    view.SizeChanged += (obj, args) =>
                    {
                        var xamEl = obj as Entry;
                        if (xamEl == null)
                            return;

                        // get native control (UITextField)
                        var entry = this.Control;

                        // Create borders (bottom only)
                        CALayer border = new CALayer();
                        float width = 1.0f;
                        border.BorderColor = view.CustomBorderColor.ToCGColor(); //UIColor.White.CGColor;  // white border color
                        border.Frame = new CGRect(x: 0, y: xamEl.Height - width, width: xamEl.Width, height: 1.0f);
                        border.BorderWidth = width;

                        entry.Layer.AddSublayer(border);

                        entry.Layer.MasksToBounds = true;
                        //entry.BorderStyle = UITextBorderStyle.None;
                        //entry.BackgroundColor = new UIColor(1, 1, 1, 1); // white

                        //increasing margin to fill border space
                        //xamEl.Margin = new Thickness(0, 10, 0, 10);
                    };
                }
            }
        }
    }
}
