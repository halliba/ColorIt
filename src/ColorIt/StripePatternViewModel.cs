namespace ColorIt
{
    internal class StripePatternViewModel : PatternViewModel
    {
        protected sealed override IPattern Pattern { get; set; }

        public int StripeCount
        {
            get => ((StripePattern)Pattern).StripeCount;
            set
            {
                if (value == ((StripePattern)Pattern).StripeCount) return;
                ((StripePattern)Pattern).StripeCount = value;
                OnPropertyChanged();
                Reload();
            }
        }

        public double Factor
        {
            get => ((StripePattern) Pattern).Factor;
            set
            {
                if (value.Equals(((StripePattern)Pattern).Factor)) return;
                ((StripePattern)Pattern).Factor = value;
                OnPropertyChanged();
                Reload();
            }
        }

        public double Factor2
        {
            get => ((StripePattern)Pattern).Factor2;
            set
            {
                if (value.Equals(((StripePattern)Pattern).Factor2)) return;
                ((StripePattern)Pattern).Factor2 = value;
                OnPropertyChanged();
                Reload();
            }
        }

        public double Factor3
        {
            get => ((StripePattern)Pattern).Factor3;
            set
            {
                if (value.Equals(((StripePattern)Pattern).Factor3)) return;
                ((StripePattern)Pattern).Factor3 = value;
                OnPropertyChanged();
                Reload();
            }
        }

        public bool Mirrored
        {
            get => ((StripePattern)Pattern).Mirrored;
            set
            {
                if (value == ((StripePattern)Pattern).Mirrored) return;
                ((StripePattern)Pattern).Mirrored = value;
                OnPropertyChanged();
                Reload();
            }
        }

        public bool SkipMiddle
        {
            get => ((StripePattern)Pattern).SkipMiddle;
            set
            {
                if (value == ((StripePattern)Pattern).SkipMiddle) return;
                ((StripePattern)Pattern).SkipMiddle = value;
                OnPropertyChanged();
                Reload();
            }
        }

        public bool InvertColors
        {
            get => ((StripePattern)Pattern).InvertColors;
            set
            {
                if (value == ((StripePattern)Pattern).InvertColors) return;
                ((StripePattern)Pattern).InvertColors = value;
                OnPropertyChanged();
                Reload();
            }
        }

        public StripePatternViewModel()
        {
            Pattern = new StripePattern();
        }

        public override void Reset()
        {
            PreventReload = true;
            Pattern.Reset();
            OnPropertyChanged(nameof(StripeCount));
            OnPropertyChanged(nameof(Factor));
            OnPropertyChanged(nameof(Factor2));
            OnPropertyChanged(nameof(Factor3));
            OnPropertyChanged(nameof(Mirrored));
            OnPropertyChanged(nameof(SkipMiddle));
            OnPropertyChanged(nameof(InvertColors));
            PreventReload = false;
            Reload();
        }
    }
}