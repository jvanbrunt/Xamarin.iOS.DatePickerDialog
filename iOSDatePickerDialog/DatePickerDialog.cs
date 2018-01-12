using System;
using Foundation;
using UIKit;

namespace iOSDatePickerDialog
{
    public partial class DatePickerDialog : UIViewController
    {
        private UIDatePickerMode Mode;

        private DateTime? DefaultDate;

        private DateTime? MaxiumDate;

        private DateTime? MinimumDate;

        private Action<DateTime> DateSelected;

        private String DialogTitle;

        private UIViewController RootViewController;

        private DatePickerDialog() : base("DatePickerDialog", null)
        {
            ModalPresentationStyle = UIModalPresentationStyle.OverFullScreen;
            ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            ContainerView.Layer.CornerRadius = 5F;

            DatePicker.Mode = Mode;

            if (DefaultDate.HasValue)
                DatePicker.Date = (NSDate)DefaultDate;
            else
                DatePicker.Date = (NSDate)DateTime.Now;

            if (MaxiumDate.HasValue)
                DatePicker.MaximumDate = (NSDate)MaxiumDate;

            if (MinimumDate.HasValue)
                DatePicker.MinimumDate = (NSDate)MinimumDate;

            if (!string.IsNullOrEmpty(DialogTitle))
                TitleLabel.Text = DialogTitle;
            else
                TitleLabel.Text = "Select Date";
        }

        public void Show()
        {
            RootViewController = ViewControllerHelper.TopViewController();
            RootViewController.AddChildViewController(this);

            View.TranslatesAutoresizingMaskIntoConstraints = false;
            View.Alpha = 0;
            RootViewController.View.AddSubview(View);

            DidMoveToParentViewController(RootViewController);

            View.LeadingAnchor.ConstraintEqualTo(RootViewController.View.LeadingAnchor).Active = true;
            View.TopAnchor.ConstraintEqualTo(RootViewController.View.TopAnchor).Active = true;
            View.TrailingAnchor.ConstraintEqualTo(RootViewController.View.TrailingAnchor).Active = true;
            View.BottomAnchor.ConstraintEqualTo(RootViewController.View.BottomAnchor).Active = true;

            UIView.Animate(0.2, 0, UIViewAnimationOptions.CurveEaseInOut, () => {
                View.Alpha = 1;
            }, null);
        }

        public void Close()
        {
            WillMoveToParentViewController(null);

            UIView.Animate(0.2, 0, UIViewAnimationOptions.CurveEaseInOut, () => {
                View.Alpha = 0;
            }, null);

            View.RemoveFromSuperview();
            RemoveFromParentViewController();
        }

        partial void Cancel(Foundation.NSObject sender)
        {
            Close();
        }

        partial void Done(Foundation.NSObject sender)
        {
            DateSelected?.Invoke((DateTime)DatePicker.Date);
            Close();
        }



        public class Builder 
        {
            private DatePickerDialog DatePickerDialog;
            
            public Builder() {
                DatePickerDialog = new DatePickerDialog();
            }

            public Builder SetTitle(String title) {
                DatePickerDialog.DialogTitle = title;
                return this;
            }

            public Builder SetMode(UIDatePickerMode mode) 
            {
                DatePickerDialog.Mode = mode;
                return this;
            }

            public Builder SetDefaultDate(DateTime date)
            {
                DatePickerDialog.DefaultDate = date;
                return this;
            }

            public Builder SetMaximumDate(DateTime date)
            {
                DatePickerDialog.MaxiumDate = date;
                return this;
            }

            public Builder SetMinimumDate(DateTime date)
            {
                DatePickerDialog.MinimumDate = date;
                return this;
            }

            public Builder SetDateSelected(Action<DateTime> dateSelected) {
                DatePickerDialog.DateSelected = dateSelected;
                return this;
            }

            public DatePickerDialog Build() {
                return DatePickerDialog;
            }
        }
    }
}

