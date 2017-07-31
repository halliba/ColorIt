using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace ColorIt
{
    public class StripePattern : IPattern
    {
        private static readonly Color Green = Color.FromRgb(51, 204, 51);
        private static readonly Color White = Color.FromRgb(255, 255, 255);

        public int StripeCount { get; set; }

        public double Factor { get; set; }

        public double Factor2 { get; set; }

        public double Factor3 { get; set; }

        public bool Mirrored { get; set; }

        public bool SkipMiddle { get; set; }

        public bool InvertColors { get; set; }

        public StripePattern()
        {
            Reset();
        }

        Brush IPattern.GetBrush()
        {
            var brush = new LinearGradientBrush { StartPoint = new Point(0, 0), EndPoint = new Point(1, 0) };

            var stripes = GetStripes();
            var position = 0d;
            foreach (var stripe in stripes)
            {
                brush.GradientStops.Add(new GradientStop(stripe.Color, position));
                position += stripe.Width;
                brush.GradientStops.Add(new GradientStop(stripe.Color, position));
            }

            return brush;
        }

        private IEnumerable<Stripe> GetStripes()
        {
            var stripes = new List<Stripe>();

            var values = GetRange().ToArray();
            if (Mirrored && SkipMiddle)
                values[values.Length - 1] = values[values.Length - 1] / 2;
            var proportional = GetProportionalAmount(values).ToArray();

            var colorMode = InvertColors ? ColorMode.Green : ColorMode.White;
            foreach (var t in proportional)
            {
                colorMode = colorMode == ColorMode.Green ? ColorMode.White : ColorMode.Green;
                stripes.Add(new Stripe(Mirrored ? t / 2 : t, GetColor(colorMode)));
            }

            if (!Mirrored) return stripes.ToArray();

            proportional = proportional.Reverse().ToArray();
            colorMode = colorMode == ColorMode.Green ? ColorMode.White : ColorMode.Green;
            foreach (var t in proportional)
            {
                colorMode = colorMode == ColorMode.Green ? ColorMode.White : ColorMode.Green;
                stripes.Add(new Stripe(t / 2, GetColor(colorMode)));
            }

            return stripes.ToArray();
        }

        private static IEnumerable<double> GetProportionalAmount(double[] values)
        {
            var sum = values.Sum();
            return values.Select(i => i / sum);
        }

        private IEnumerable<double> GetRange()
        {
            return Enumerable.Range(1, StripeCount)
                .Select(Calculate).ToArray();

            double Calculate(int x)
            {
                var xVal = x % 2 != 0
                    ? x * Math.Pow(Factor2, StripeCount - x)
                    : x * Factor3;
                return Math.Pow(xVal * (1d / (StripeCount + 1)), Factor);
            }
        }

        string IPattern.GetFile(int width, int height)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"Raumbreite: {(double)width / 1000:F3}m");
            builder.AppendLine($"Raumhöhe: {(double)height / 1000:F3}m");
            builder.AppendLine($"Balken: {StripeCount}");
            builder.AppendLine($"Faktor: {Factor:F}");
            builder.AppendLine($"Faktor 2: {Factor2:F}");
            builder.AppendLine($"Faktor 3: {Factor3:F}");
            builder.AppendLine();
            builder.AppendLine();
            builder.AppendLine("Balken".PadRight(10) + "Farbe".PadRight(10) + "Breite".PadRight(10) + "links".PadLeft(10) + "rechts".PadLeft(10));

            var stripes = GetStripes();
            var position = 0d;
            var i = 1;

            foreach (var stripe in stripes)
            {
                var absoluteWidth = stripe.Width * width;
                var start = position;
                var end = position += absoluteWidth;
                var line = FormatBlockLine(i++, stripe.Color, absoluteWidth, start, end);
                builder.AppendLine(line);
            }

            string FormatBlockLine(int number, Color color, double stripeWidth, double start, double end)
            {
                return number.ToString().PadRight(10)
                       + (color.Equals(Green) ? "Grün" : "").PadRight(10)
                       + ((stripeWidth / 1000).ToString("F3") + "m").PadLeft(10)
                       + ((start / 1000).ToString("F3") + "m").PadLeft(10)
                       + ((end / 1000).ToString("F3") + "m").PadLeft(10);
            }

            return builder.ToString();
        }

        public void Reset()
        {
            StripeCount = 9;
            Factor = 1;
            Factor2 = 1;
            Factor3 = 1;
            Mirrored = true;
            SkipMiddle = true;
            InvertColors = false;
        }

        private Color GetColor(ColorMode mode)
        {
            switch (mode)
            {
                case ColorMode.Green:
                    return Green;
                case ColorMode.White:
                    return White;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }

        private enum ColorMode
        {
            Green,
            White
        }

        private struct Stripe
        {
            public double Width { get; }

            public Color Color { get; }

            public Stripe(double width, Color color)
            {
                Width = width;
                Color = color;
            }
        }
    }
}