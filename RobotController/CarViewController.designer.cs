// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace RobotController
{
	[Register ("CarViewController")]
	partial class CarViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel TextTilt { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView ViewForward { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView ViewLeft { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView ViewRight { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (TextTilt != null) {
				TextTilt.Dispose ();
				TextTilt = null;
			}
			if (ViewForward != null) {
				ViewForward.Dispose ();
				ViewForward = null;
			}
			if (ViewLeft != null) {
				ViewLeft.Dispose ();
				ViewLeft = null;
			}
			if (ViewRight != null) {
				ViewRight.Dispose ();
				ViewRight = null;
			}
		}
	}
}
