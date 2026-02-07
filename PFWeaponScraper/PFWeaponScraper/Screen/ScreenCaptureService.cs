using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using PFWeaponScraper.Config;

namespace PFWeaponScraper.Screen;

public class ScreenCaptureService
{
    public Bitmap CaptureRegion(RelativeRegion region)
    {
        Rectangle windowBounds = GetActiveWindowBounds();
        Rectangle pixelRegion = ToPixelRegion(region, windowBounds);

        var bitmap = new Bitmap(pixelRegion.Width, pixelRegion.Height);
        using var g = Graphics.FromImage(bitmap);

        g.CopyFromScreen(
            pixelRegion.Location,
            Point.Empty,
            pixelRegion.Size
        );

        return bitmap;
    }

    /* -------------------- Helpers -------------------- */

    private static Rectangle ToPixelRegion(RelativeRegion region, Rectangle window)
    {
        return new Rectangle(
            x: window.Left + (int)(region.X * window.Width),
            y: window.Top + (int)(region.Y * window.Height),
            width: (int)(region.Width * window.Width),
            height: (int)(region.Height * window.Height)
        );
    }

    private static Rectangle GetActiveWindowBounds()
    {
        IntPtr hwnd = GetForegroundWindow();
        GetWindowRect(hwnd, out RECT rect);

        return Rectangle.FromLTRB(
            rect.Left,
            rect.Top,
            rect.Right,
            rect.Bottom
        );
    }

    /* -------------------- Win32 -------------------- */

    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

    private struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }
}