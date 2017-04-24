﻿// Author: Daniele Giardini - http://www.demigiant.com
// Created: 2017/04/24 12:40
// License Copyright (c) Daniele Giardini

using UnityEngine;

namespace DG.DemiEditor.DeGUINodeSystem
{
    /// <summary>
    /// Returns key modifiers currently pressed
    /// </summary>
    public static class KeyModifier
    {
        public static bool ctrl { get { return Event.current.control || Event.current.command; }}
        public static bool softCtrl { get {
            return (Event.current.control || Event.current.command || Time.realtimeSinceStartup - _timeAtControlKeyRelease < 0.2f);
        }}
        public static bool shift { get { return Event.current.shift; }}
        public static bool alt { get { return Event.current.alt; }}
        public static bool none { get { return !ctrl && !shift && !alt; } }

        public static bool ctrlShiftAlt { get { return ctrl && shift && alt; }}
        public static bool ctrlShift { get { return ctrl && shift; }}
        public static bool ctrlAlt { get { return ctrl && alt; }}
        public static bool shiftAlt { get { return shift && alt; }}

        static float _timeAtControlKeyRelease;

        #region Internal Methods

        /// <summary>
        /// Call this method to update data required by softCtrl calculations.
        /// Automatically called from within a <see cref="NodeProcessScope{T}"/>.
        /// </summary>
        public static void Update()
        {
            // Evaluate softControl and space keys
            if (Event.current.type == EventType.KeyDown) {
                if (Event.current.keyCode == KeyCode.Space) Extra.space = true;
            } else if (Event.current.rawType == EventType.KeyUp) {
                bool isCtrl = Event.current.keyCode == KeyCode.LeftControl || Event.current.keyCode == KeyCode.RightControl
                              || Event.current.keyCode == KeyCode.LeftCommand || Event.current.keyCode == KeyCode.RightCommand;
                if (isCtrl) _timeAtControlKeyRelease = Time.realtimeSinceStartup;
                else if (Event.current.keyCode == KeyCode.Space) Extra.space = false;
            }
        }

        /// <summary>
        /// Returns the given <see cref="KeyCode"/> as an int, or -1 if it's not a number
        /// </summary>
        public static int ToInt(KeyCode keycode)
        {
            switch (keycode) {
            case KeyCode.Keypad0:
            case KeyCode.Alpha0:
                return 0;
            case KeyCode.Keypad1:
            case KeyCode.Alpha1:
                return 1;
            case KeyCode.Keypad2:
            case KeyCode.Alpha2:
                return 2;
            case KeyCode.Keypad3:
            case KeyCode.Alpha3:
                return 3;
            case KeyCode.Keypad4:
            case KeyCode.Alpha4:
                return 4;
            case KeyCode.Keypad5:
            case KeyCode.Alpha5:
                return 5;
            case KeyCode.Keypad6:
            case KeyCode.Alpha6:
                return 6;
            case KeyCode.Keypad7:
            case KeyCode.Alpha7:
                return 7;
            case KeyCode.Keypad8:
            case KeyCode.Alpha8:
                return 8;
            case KeyCode.Keypad9:
            case KeyCode.Alpha9:
                return 9;
            default:
                return -1;
            }
        }

        #endregion

        // █████████████████████████████████████████████████████████████████████████████████████████████████████████████████████
        // ███ INTERNAL CLASSES ████████████████████████████████████████████████████████████████████████████████████████████████
        // █████████████████████████████████████████████████████████████████████████████████████████████████████████████████████

        public static class Extra
        {
            public static bool space { get; internal set; }
        }

        public static class Exclusive
        {
            public static bool ctrl { get { return KeyModifier.ctrl && !KeyModifier.shift && !KeyModifier.alt; }}
            public static bool softCtrl { get { return KeyModifier.softCtrl && !KeyModifier.shift && !KeyModifier.alt; }}
            public static bool shift { get { return KeyModifier.shift && !KeyModifier.ctrl && !KeyModifier.alt; }}
            public static bool alt { get { return KeyModifier.alt && !KeyModifier.ctrl && !KeyModifier.shift; }}

            public static bool ctrlShiftAlt { get { return KeyModifier.ctrl && KeyModifier.shift && KeyModifier.alt; }}
            public static bool ctrlShift { get { return KeyModifier.ctrl && KeyModifier.shift && !KeyModifier.alt; }}
            public static bool ctrlAlt { get { return KeyModifier.ctrl && KeyModifier.alt && !KeyModifier.shift; }}
            public static bool shiftAlt { get { return KeyModifier.shift && KeyModifier.alt && !KeyModifier.ctrl; }}
        }
    }
}