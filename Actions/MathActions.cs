﻿namespace it.Actions
{
	using BenchmarkDotNet.Attributes;
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Linq;
	using System.Text.RegularExpressions;
	using System.Windows.Forms;

	public sealed class MathActions : IAction
	{
		public ActionResult TryExecute(string clipboardText)
		{
			ActionResult actionResult = new ActionResult();
			clipboardText = clipboardText.Replace(',', '.');
			string answer = EvalExpression(clipboardText).ToString(CultureInfo.CurrentCulture);
			actionResult.Title = clipboardText;
			actionResult.Description = answer;
			Clipboard.SetText(answer);
			return actionResult;
		}

		public bool Matches(string clipboardText)
		{
			try
			{
				EvalExpression(clipboardText);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public static double EvalExpression(String expr)
		{
			return parseSummands(expr.ToCharArray(), 0);
		}

		private static double parseSummands(char[] expr, int index)
		{
			double x = parseFactors(expr, ref index);
			while (true)
			{
				char op = expr[index];
				if (op != '+' && op != '-')
					return x;
				index++;
				double y = parseFactors(expr, ref index);
				if (op == '+')
					x += y;
				else
					x -= y;
			}
		}

		private static double parseFactors(char[] expr, ref int index)
		{
			double x = GetDouble(expr, ref index);
			while (true)
			{
				char op = expr[index];
				if (op != ':' && op != '*' && op != 'x' && op != '%')
					return x;
				index++;
				double y = GetDouble(expr, ref index);
				if (op == ':')
					x /= y;
				else if (op == '%')
					x %= y;
				else
					x *= y;
			}
		}

		private static double GetDouble(char[] expr, ref int index)
		{
			bool isNegative = expr[index] == '-';
			if (isNegative) index++;
			string dbl = string.Empty;
			while ((int)expr[index] >= 48 && (int)expr[index] <= 57 || expr[index] == 46 || (int)expr[index] == 32)
			{
				dbl = dbl + expr[index];
				index++;
				if (index == expr.Length)
				{
					index--;
					break;
				}
			}
			var result = double.Parse(dbl, CultureInfo.InvariantCulture);
			if (isNegative) result = -result; return result;
			return double.Parse(dbl, CultureInfo.InvariantCulture);
		}
	}
}