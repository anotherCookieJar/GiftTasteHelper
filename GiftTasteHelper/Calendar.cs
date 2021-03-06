﻿using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using StardewValley.Menus;
using StardewValley;

namespace GiftTasteHelper
{
    public class Calendar
    {
        public const int NUM_DAYS = 28;
        private const string calendarDaysFieldName = "calendarDays";

        public struct BirthdayEventInfo
        {
            public int day;
            public string npcName;
        }

        private Billboard billboard;
        private List<ClickableTextureComponent> calendarDays;
        private bool isOpen = false;
        private Rectangle bounds;

        public bool IsOpen
        {
            get { return isOpen; }
            set { isOpen = value; }
        }

        public bool IsInitialized
        {
            get; private set;
        }

        public Rectangle Bounds
        {
            get { return bounds; }
        }

        public void Init(Billboard baseClass)
        {
            Clear();

            billboard = baseClass;
            bounds = new Rectangle(billboard.xPositionOnScreen, billboard.yPositionOnScreen, billboard.width, billboard.height);
            calendarDays = Utils.GetNativeField<List<ClickableTextureComponent>, Billboard>(billboard, calendarDaysFieldName);
            IsInitialized = true;
        }

        public void OnResize(Billboard baseClass)
        {
            if (IsInitialized)
            {
                // We seem to lose our billboard ref on re-size, so get it back
                billboard = baseClass;
                bounds = new Rectangle(billboard.xPositionOnScreen, billboard.yPositionOnScreen, billboard.width, billboard.height);
                calendarDays = Utils.GetNativeField<List<ClickableTextureComponent>, Billboard>(billboard, calendarDaysFieldName);
            }
            else
            {
                Init(baseClass);
            }
        }

        public void Clear()
        {
            billboard = null;
            if (calendarDays != null)
            {
                calendarDays.Clear();
                calendarDays = null;
            }
            isOpen = false;
            IsInitialized = false;
        }

        public Rectangle GetDayBounds(int dayNumber)
        {
            Debug.Assert(dayNumber > 0 && dayNumber <= NUM_DAYS, "Day number out of range");
            return calendarDays[dayNumber - 1].bounds;
        }

        public string GetCurrentHoverText()
        {
            return Utils.GetNativeField<string, Billboard>(billboard, "hoverText");
        }

        public string GetHoveredBirthdayNPCName(SVector2 mouse)
        {
            string name = string.Empty;
            if (!bounds.Contains(mouse.ToPoint()))
            {
                return name;
            }

            foreach (ClickableTextureComponent day in calendarDays)
            {
                if (day.bounds.Contains(mouse.ToPoint()))
                {
                    if (day.hoverText.Length > 0 && day.hoverText.Contains("Birthday"))
                    {
                        name = day.hoverText;
                        break;
                    }
                }
            }
            return name;
        }
    }
}
