﻿using System;
using System.Windows;

namespace it.Actions
{
    internal sealed class tryBinary : IAction
    {
        public bool Matches(string clipboardText)
        {
            return clipboardText.EndsWith(" to binary", StringComparison.Ordinal);
        }

        public ActionResult TryExecute(string clipboardText = null)
        {
            ActionResult actionResult = new ActionResult();
            {
                int toBinaryIndex = clipboardText.IndexOf("to binary", StringComparison.Ordinal);
                clipboardText.Substring(0, toBinaryIndex);
                string binary = Convert.ToString(toBinaryIndex, 2);
                actionResult.Title = "Calculate binary";
                actionResult.Description = $"{clipboardText}, {binary}";
                Clipboard.SetText($"{binary}");
            }
            return actionResult;
        }
    }
}
