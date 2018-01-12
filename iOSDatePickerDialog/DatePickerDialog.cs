using System;
using System.Linq;
using System.Threading.Tasks;
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
                TitleLabel.Text = "Selected Date";
        }

        public void Show()
        {
            var root = UIApplication.SharedApplication.Windows.First().RootViewController;

            root.AddChildViewController(this);

            View.TranslatesAutoresizingMaskIntoConstraints = false;
            View.Alpha = 0;
            root.View.AddSubview(View);

            DidMoveToParentViewController(root);

            View.LeadingAnchor.ConstraintEqualTo(root.View.LeadingAnchor).Active = true;
            View.TopAnchor.ConstraintEqualTo(root.View.TopAnchor).Active = true;
            View.TrailingAnchor.ConstraintEqualTo(root.View.TrailingAnchor).Active = true;
            View.BottomAnchor.ConstraintEqualTo(root.View.BottomAnchor).Active = true;

            UIView.Animate(0.2, 0, UIViewAnimationOptions.CurveEaseInOut, () => {
                View.Alpha = 1;
            }, null);
        }

        public void Close()
        {
            var root = UIApplication.SharedApplication.Windows.First().RootViewController;

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

