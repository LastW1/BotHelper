using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace BotHelper
{
    public static class MouseManager
    {
        [Flags]
        public enum MouseEventFlags
        {
            LeftDown = 0x00000002,
            LeftUp = 0x00000004,
            MiddleDown = 0x00000020,
            MiddleUp = 0x00000040,
            Move = 0x00000001,
            Absolute = 0x00008000,
            RightDown = 0x00000008,
            RightUp = 0x00000010
        }

        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetCursorPos(out MousePoint lpMousePoint);

        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        public static void SetCursorPosition(int x, int y)
        {
            SetCursorPos(x, y);
        }

        public static void SetCursorPosition(MousePoint point)
        {
            SetCursorPos(point.X, point.Y);
        }

        public static MousePoint GetCursorPosition()
        {
            MousePoint currentMousePoint;
            var gotPoint = GetCursorPos(out currentMousePoint);
            if (!gotPoint) { currentMousePoint = new MousePoint(0, 0); }
            return currentMousePoint;
        }

        public static void MouseEvent(MouseEventFlags value)
        {
            MousePoint position = GetCursorPosition();

            mouse_event
                ((int)value,
                 position.X,
                 position.Y,
                 0,
                 0)
                ;
        }

        public static void MouseEventAtPosition(MouseEventFlags value, int x, int y)
        {
            mouse_event
                ((int)value,
                 x,
                 y,
                 0,
                 0)
                ;
        }

        public static void MouseRightClickAtPosition(int x, int y)
        {
            SetCursorPosition(x, y);
            Thread.Sleep(20);
            MouseEvent(MouseEventFlags.RightDown);
            Thread.Sleep(20);
            MouseEvent(MouseEventFlags.RightUp);
        }

        public static void MouseRightClick()
        {
            MouseEvent(MouseEventFlags.RightDown);
            Thread.Sleep(20);
            MouseEvent(MouseEventFlags.RightUp);
        }

        public static void MouseLeftClick()
        {
            MouseEvent(MouseEventFlags.LeftDown);
            Thread.Sleep(20);
            MouseEvent(MouseEventFlags.LeftUp);
        }

        public static void MouseDoubleLeftClick()
        {
            MouseLeftClick();
            Thread.Sleep(20);
            MouseLeftClick();
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MousePoint
        {
            public int X;
            public int Y;

            public MousePoint(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        //dodać klasy async
    }
}
