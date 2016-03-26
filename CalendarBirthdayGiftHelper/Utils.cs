﻿using System;
using System.Collections.Generic;
using StardewModdingAPI;

namespace CalendarBirthdayGiftHelper
{
    public class Utils
    {
        public static string ArrayToString<T>(T[] array)
        {
            string s = "";
            int i = 0;
            foreach (T item in array)
            {
                s += item.ToString() + ((++i < array.Length) ? ", " : "");
            }
            return s;
        }

        public static int[] StringToIntArray(string[] array, int defaultVal=0)
        {
            int[] output = new int[array.Length];
            for (int i = 0; i < array.Length; ++i)
            {
                if (array[i] == null || !array[i].IsInt32())
                    continue;

                try
                {
                    output[i] = Int32.Parse(array[i]);
                }
                catch (Exception ex)
                {
                    Log.Error("[CalendarBirthdayGiftHelper] failed to convert {0} to int32: {1}", array[i], ex);
                    output[i] = defaultVal;
                }
            }
            return output;
        }
    }
}
