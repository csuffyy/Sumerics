﻿namespace Sumerics.Controls
{
    using FastColoredTextBoxNS;
    using Sumerics.Resources;
    using System;
    using System.Drawing;
    using System.Text;

    public class ConsoleQueryReference
    {
        readonly String _originalQuery;
        readonly OutputRegion _outputRegion;
        readonly FastColoredTextBox _control;

        internal ConsoleQueryReference(String query, OutputRegion outputRegion, FastColoredTextBox control)
        {
            _originalQuery = query;
            _outputRegion = outputRegion;
            _control = control;
        }

        public String OriginalQuery
        {
            get { return _originalQuery; }
        }

        Int32 CurrentChars
        {
            get { return _control.Width / _control.CharWidth; }
        }

        public void SetRunning()
        {
            _outputRegion.Style.ForeBrush = new SolidBrush(Color.SteelBlue);
            _outputRegion.Text = Messages.Evaluating;
        }

        public void SetError(String message)
        {
            _outputRegion.Style.ForeBrush = new SolidBrush(Color.PaleVioletRed);
            _outputRegion.Text = message;
        }

        public void SetVoid()
        {
            _outputRegion.Text = String.Empty;
        }

        public void SetText(String result)
        {
            var lc = result.Split('\n').Length;
            var message = String.Concat("[ ", Messages.LinesOfText, " ]\n");
            _outputRegion.Style.ForeBrush = new SolidBrush(Color.DarkGray);
            _outputRegion.Fold = lc > 2;
            _outputRegion.Text = String.Format(message, lc) + result;
        }

        public void SetMatrix(String[,] values)
        {
            var length = ComputeLargestStringContent(values);
            var cols = values.GetLength(0);
            var rows = values.GetLength(1);
            var requireFormat = TooManyColumns(cols, length);
            var mtxt = requireFormat ? GetFormattedText(values, length) : GetPlainText(values, length);
            var message = String.Concat("[ ", Messages.DimOfMatrix, " ]");
            _outputRegion.Style.ForeBrush = new SolidBrush(Color.DarkGray);
            _outputRegion.Fold = rows > 1 || requireFormat;
            _outputRegion.Text = String.Format(message, rows, cols) + mtxt;
        }

        public void SetResult(String value)
        {
            _outputRegion.Style.ForeBrush = new SolidBrush(Color.DarkGray);
            _outputRegion.Text = value;
        }

        Boolean TooManyColumns(Int32 cols, Int32 length)
        {
            var colLength = length + 3;
            return cols * colLength > CurrentChars;
        }

        String GetFormattedText(String[,] m, Int32 length)
        {
            var sb = new StringBuilder();
            var colLength = length + 1;
            var cols = m.GetLength(0);
            var rows = m.GetLength(1);
            var width = Math.Max(1, CurrentChars / colLength - 1);
            var loops = cols / width;

            if (cols > loops * width)
            {
                loops++;
            }

            for (var k = 0; k < loops; k++)
            {
                var c = k * width + 1;
                var max = c + Math.Min(width - 1, cols - c);
                sb.AppendLine();
                sb.AppendLine();

                if (c != max)
                {
                    sb.AppendFormat(Messages.ColumnsRange, c, max);
                }
                else
                {
                    sb.Append(Messages.Column).Append(' ').Append(c);
                }

                sb.AppendLine();

                for (var i = 0; i < rows; i++)
                {
                    sb.AppendLine();

                    for (var j = c - 1; j < max; j++)
                    {
                        sb.Append(m[j, i].PadLeft(colLength));
                    }
                }
            }

            return sb.ToString();
        }

        String GetPlainText(String[,] m, Int32 length)
        {
            var sb = new StringBuilder();
            var colLength = length + 1;
            var cols = m.GetLength(0);
            var rows = m.GetLength(1);

            for (var i = 0; i < rows; i++)
            {
                sb.AppendLine();

                for (var j = 0; j < cols; j++)
                {
                    sb.Append(m[j, i].PadLeft(colLength));
                }
            }

            return sb.ToString();
        }

        static Int32 ComputeLargestStringContent(String[,] values)
        {
            var cols = values.GetLength(0);
            var rows = values.GetLength(1);
            var maxLength = 0;

            for (var j = 0; j != rows; j++)
            {
                for (var i = 0; i != cols; i++)
                {
                    var colLength = values[i, j].Length;
                    maxLength = Math.Max(colLength, maxLength);
                }

            }

            return maxLength;
        }
    }
}
