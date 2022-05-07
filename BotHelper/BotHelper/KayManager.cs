using GregsStack.InputSimulatorStandard;
using GregsStack.InputSimulatorStandard.Native;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BotHelper
{
    class KayManager
    {
        private InputSimulator _inputSimulator;
        public KayManager()
        {
            _inputSimulator = new InputSimulator();
        }

        // zamiast dict zrobić enum
        public static Dictionary<string, VirtualKeyCode> KaysDict = new Dictionary<string, VirtualKeyCode> {
        // F
        { "F1" , VirtualKeyCode.F1},
        { "F2" , VirtualKeyCode.F2},
        { "F3" , VirtualKeyCode.F3},
        { "F4" , VirtualKeyCode.F4},
        { "F5" , VirtualKeyCode.F5},
        { "F6" , VirtualKeyCode.F6},
        { "F7" , VirtualKeyCode.F7},
        { "F8" , VirtualKeyCode.F8},
        { "F9" , VirtualKeyCode.F9},
        { "F10" , VirtualKeyCode.F10},
        { "F11" , VirtualKeyCode.F11},
        { "F12" , VirtualKeyCode.F12},
        // NUMS
        { "NUM_0" , VirtualKeyCode.NUMPAD0},
        { "NUM_1" , VirtualKeyCode.NUMPAD1},
        { "NUM_2" , VirtualKeyCode.NUMPAD2},
        { "NUM_3" , VirtualKeyCode.NUMPAD3},
        { "NUM_4" , VirtualKeyCode.NUMPAD4},
        { "NUM_5" , VirtualKeyCode.NUMPAD5},
        { "NUM_6" , VirtualKeyCode.NUMPAD6},
        { "NUM_7" , VirtualKeyCode.NUMPAD7},
        { "NUM_8" , VirtualKeyCode.NUMPAD8},
        { "NUM_9" , VirtualKeyCode.NUMPAD9},
        // Action Kays
        { "Backspace" , VirtualKeyCode.BACK},
        { "Space" , VirtualKeyCode.SPACE},
        { "R_Shift" , VirtualKeyCode.RSHIFT},
        { "L_Shift" , VirtualKeyCode.LSHIFT},
        { "R_Ctrl" , VirtualKeyCode.RCONTROL},
        { "L_Ctrl" , VirtualKeyCode.LCONTROL},
        { "Enter" , VirtualKeyCode.ACCEPT}, // to sprawdzić
        // Letters
        { "Q" , VirtualKeyCode.VK_Q},
            };

        public void SimulateKeyDown(string key)
        {

            if (!KaysDict.TryGetValue(key, out var keyCode))
            {
                throw new Exception("niepoprawny typ przycisku");
            }

            _inputSimulator.Keyboard.KeyDown(keyCode);
        }

        public void SimulateKeyUp(string key)
        {

            if (!KaysDict.TryGetValue(key, out var keyCode))
            {
                throw new Exception("niepoprawny typ przycisku");
            }

            _inputSimulator.Keyboard.KeyUp(keyCode);
        }

        public void SimulateKeyClick(string key)
        {
            if (!KaysDict.TryGetValue(key, out var keyCode))
            {
                throw new Exception("niepoprawny typ przycisku");
            }

            _inputSimulator.Keyboard.KeyDown(keyCode);
            Thread.Sleep(10);
            _inputSimulator.Keyboard.KeyUp(keyCode);
        }

        public void SimulateKeyText(string textInput)
        {
            _inputSimulator.Keyboard.TextEntry(textInput);
        }

        // dodać async
    }
}
