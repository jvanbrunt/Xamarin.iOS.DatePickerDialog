using System;
using System.Linq;
using UIKit;

namespace iOSDatePickerDialog
{
    public class ViewControllerHelper
    {
        public static UIViewController TopViewController() {
            var rootViewController = UIApplication.SharedApplication.KeyWindow.RootViewController;
            return TopViewController(rootViewController);
        }

        private static UIViewController TopViewController(UIViewController rootViewContoller) {

            if (rootViewContoller.PresentedViewController == null)
                return rootViewContoller;

            var navigationController = rootViewContoller.PresentedViewController as UINavigationController;
            if (navigationController != null) {
                return navigationController;
            }

            var presentedViewController = rootViewContoller.PresentedViewController;
            return TopViewController(presentedViewController);
        }
    }
}
