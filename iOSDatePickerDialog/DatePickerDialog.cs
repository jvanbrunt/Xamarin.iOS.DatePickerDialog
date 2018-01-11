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
        }

        public async Task Show()
        {
            var root = UIApplication.SharedApplication.Windows.First().RootViewController;
            await root.PresentViewControllerAsync(this, true);
        }

        public async Task Close()
        {
            var root = UIApplication.SharedApplication.Windows.First().RootViewController;
            await root.DismissViewControllerAsync(true);
        }

        async partial void Cancel(Foundation.NSObject sender)
        {
            await Close();
        }

        async partial void Done(Foundation.NSObject sender)
        {
            DateSelected?.Invoke((DateTime)DatePicker.Date);
            await Close();
        }

        public class Builder 
        {
            private DatePickerDialog DatePickerDialog;
            
            public Builder() {
                DatePickerDialog = new DatePickerDialog();
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

