using System;
using System.Globalization;

namespace it.Actions
{
    public sealed class TimespanActions : IAction
    {
        private readonly string[] dateFormats =
        {
            "dd.MM.yyyy",
            "dd-MM-yyyy",

            //american format
            "MM.dd.yyyy",
            "MM-dd-yyyy",
        };

        private bool isUsingTimespan;

        private DateTimeOffset? prevDate;

        public override bool Equals(object obj)
        {
            return Equals(obj as TimespanActions);
        }

        public bool Matches(string clipboardText)
        {
            return DateTimeOffset.TryParseExact(clipboardText, dateFormats, CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal, out _);
        }

        public ActionResult TryExecute(string clipboardText)
        {
            isUsingTimespan = !isUsingTimespan;
            ActionResult actionResult = new ActionResult();
            if (DateTimeOffset.TryParseExact(clipboardText, dateFormats, CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal, out DateTimeOffset newDate))
            {
                if (prevDate.HasValue)
                {
                    TimeSpan? difference = newDate - prevDate;
                    prevDate = null;
                    actionResult.Title = "Days between:";
                    actionResult.Description = difference.Value.Days.ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    prevDate = newDate;
                }
            }

            return actionResult;
        }
    }
}
