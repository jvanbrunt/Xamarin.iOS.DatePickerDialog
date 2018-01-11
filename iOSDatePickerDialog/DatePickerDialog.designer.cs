// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace iOSDatePickerDialog
{
	[Register ("DatePickerDialog")]
	partial class DatePickerDialog
	{
		[Outlet]
		UIKit.UIView ContainerView { get; set; }

		[Outlet]
		UIKit.UIDatePicker DatePicker { get; set; }

		[Action ("Cancel:")]
		partial void Cancel (Foundation.NSObject sender);

		[Action ("Done:")]
		partial void Done (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (DatePicker != null) {
				DatePicker.Dispose ();
				DatePicker = null;
			}

			if (ContainerView != null) {
				ContainerView.Dispose ();
				ContainerView = null;
			}
		}
	}
}
