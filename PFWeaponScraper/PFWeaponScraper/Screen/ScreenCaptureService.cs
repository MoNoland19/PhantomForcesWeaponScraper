using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using PFWeaponScraper.Config;

namespace PFWeaponScraper.Screen;

public class ScreenCaptureService
{
    private const string GameProcessName = "RobloxPlayerBeta"; 
    // ⬆️ Change this if your game process name is different

    /* ==================== PUBLIC API ==================== */

    public Bitmap CaptureRegion(RelativeRegion region)
    {
        Rectangle windowBounds = GetGameWindowBounds();
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

    public Bitmap CaptureFullWindow()
    {
        Rectangle windowBounds = GetGameWindowBounds();

        var bitmap = new Bitmap(windowBounds.Width, windowBounds.Height);

        using var g = Graphics.FromImage(bitmap);
        g.CopyFromScreen(
            windowBounds.Location,
            Point.Empty,
            windowBounds.Size
        );

        return bitmap;
    }

    /* ==================== INTERNAL LOGIC ==================== */

    private static Rectangle ToPixelRegion(RelativeRegion region, Rectangle window)
    {
        return new Rectangle(
            x: window.Left + (int)(region.X * window.Width),
            y: window.Top + (int)(region.Y * window.Height),
            width: (int)(region.Width * window.Width),
            height: (int)(region.Height * window.Height)
        );
    }

    private static Rectangle GetGameWindowBounds()
    {
        IntPtr hwnd = FindGameWindow(GameProcessName);

        if (!GetWindowRect(hwnd, out RECT rect))
            throw new InvalidOperationException("Failed to get game window bounds.");

        return Rectangle.FromLTRB(
            rect.Left,
            rect.Top,
            rect.Right,
            rect.Bottom
        );
    }

    private static IntPtr FindGameWindow(string processName)
    {
        var processes = Process.GetProcessesByName(processName);

        foreach (var process in processes)
        {
            if (process.MainWindowHandle != IntPtr.Zero)
                return process.MainWindowHandle;
        }

        throw new InvalidOperationException(
            $"Could not find a window for process '{processName}'."
        );
    }

    /* ==================== WIN32 ==================== */

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
