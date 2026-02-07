var capture = new ScreenCaptureService();
var img = capture.CaptureRegion(ScreenRegions.GunName);
img.Save("gun.png");