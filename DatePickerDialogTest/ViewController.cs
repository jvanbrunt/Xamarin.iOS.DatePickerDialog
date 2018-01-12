using System;

using UIKit;
using iOSDatePickerDialog;
using System.Diagnostics;

namespace DatePickerDialogTest
{
    public partial class ViewController : UIViewController
    {
        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        partial void Show(Foundation.NSObject sender)
        {
            new DatePickerDialog.Builder()
                                     .SetTitle("My Date")
                               .SetMode(UIDatePickerMode.Date)
                               .SetDefaultDate(DateTime.Now)
                               .SetDateSelected((obj) => Debug.WriteLine(obj.ToString("D")))
                               .Build()
                               .Show();
        }
    }
}
