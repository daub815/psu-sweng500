namespace Sweng500.Pml.Client.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    /// <summary>
    /// Interaction logic for RatingsControl.xaml
    /// </summary>
    /// <remarks>Borrowed from Sacha Barber <see cref="http://www.codeproject.com/Articles/45210/WPF-A-Simple-Yet-Flexible-Rating-Control"/></remarks>
    public partial class RatingsControl : UserControl
    {
        #region Statics

        /// <summary>
        /// BackgroundColor Dependency Property
        /// </summary>
        public static readonly DependencyProperty BackgroundColorProperty = DependencyProperty.Register(
            "BackgroundColor",
            typeof(SolidColorBrush),
            typeof(RatingsControl),
            new FrameworkPropertyMetadata(
                Brushes.Transparent,
                new PropertyChangedCallback(OnBackgroundColorChanged)));

        /// <summary>
        /// StarForegroundColor Dependency Property
        /// </summary>
        public static readonly DependencyProperty StarForegroundColorProperty = DependencyProperty.Register(
            "StarForegroundColor",
            typeof(SolidColorBrush),
            typeof(RatingsControl),
            new FrameworkPropertyMetadata(
                Brushes.Transparent,
                new PropertyChangedCallback(OnStarForegroundColorChanged)));

        /// <summary>
        /// StarOutlineColor Dependency Property
        /// </summary>
        public static readonly DependencyProperty StarOutlineColorProperty = DependencyProperty.Register(
            "StarOutlineColor", 
            typeof(SolidColorBrush),
            typeof(RatingsControl),
            new FrameworkPropertyMetadata(
                Brushes.Transparent,
                new PropertyChangedCallback(OnStarOutlineColorChanged)));

        /// <summary>
        /// Value Dependency Property
        /// </summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value", 
            typeof(decimal),
            typeof(RatingsControl),
            new FrameworkPropertyMetadata(
                (decimal)0.0,
                new PropertyChangedCallback(OnValueChanged),
                new CoerceValueCallback(CoerceValueValue)));

        /// <summary>
        /// NumberOfStars Dependency Property
        /// </summary>
        public static readonly DependencyProperty NumberOfStarsProperty = DependencyProperty.Register(
            "NumberOfStars", 
            typeof(decimal?), 
            typeof(RatingsControl),
            new FrameworkPropertyMetadata(
                (decimal?)0,
                new PropertyChangedCallback(OnNumberOfStarsChanged),
                new CoerceValueCallback(CoerceNumberOfStarsValue)));

        /// <summary>
        /// Maximum Dependency Property
        /// </summary>
        public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register(
            "Maximum", 
            typeof(decimal), 
            typeof(RatingsControl),
                new FrameworkPropertyMetadata((decimal)5));

        /// <summary>
        /// Minimum Dependency Property
        /// </summary>
        public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register(
            "Minimum", 
            typeof(decimal), 
            typeof(RatingsControl),
                new FrameworkPropertyMetadata((decimal)0));

        #endregion Statics

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the RatingsControl class
        /// </summary>
        public RatingsControl()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the BackgroundColor property.  
        /// </summary>
        public SolidColorBrush BackgroundColor
        {
            get { return (SolidColorBrush)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the StarForegroundColor property.  
        /// </summary>
        public SolidColorBrush StarForegroundColor
        {
            get { return (SolidColorBrush)GetValue(StarForegroundColorProperty); }
            set { SetValue(StarForegroundColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the StarOutlineColor property.  
        /// </summary>
        public SolidColorBrush StarOutlineColor
        {
            get { return (SolidColorBrush)GetValue(StarOutlineColorProperty); }
            set { SetValue(StarOutlineColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Value property.  
        /// </summary>
        public decimal Value
        {
            get { return (decimal)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        /// <summary>
        /// Gets or sets the NumberOfStars property.  
        /// </summary>
        public decimal? NumberOfStars
        {
            get { return (decimal?)GetValue(NumberOfStarsProperty); }
            set { SetValue(NumberOfStarsProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Maximum property.  
        /// </summary>
        public decimal Maximum
        {
            get { return (decimal)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Minimum property.  
        /// </summary>
        public decimal Minimum
        {
            get { return (decimal)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        #endregion Properties

        #region On DP Changed

        /// <summary>
        /// Handles changes to the BackgroundColor property.
        /// </summary>
        /// <param name="d">The owning dependency object</param>
        /// <param name="e">The dependency arguments of the change</param>
        private static void OnBackgroundColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RatingsControl control = (RatingsControl)d;
            foreach (StarControl star in control.spStars.Children)
            {
                star.BackgroundColor = (SolidColorBrush)e.NewValue;
            }
        }

        /// <summary>
        /// Handles changes to the StarForegroundColor property.
        /// </summary>
        /// <param name="d">The owning dependency object</param>
        /// <param name="e">The dependency arguments of the change</param>
        private static void OnStarForegroundColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RatingsControl control = (RatingsControl)d;
            foreach (StarControl star in control.spStars.Children)
            {
                star.StarForegroundColor = (SolidColorBrush)e.NewValue;
            }
        }

        /// <summary>
        /// Handles changes to the StarOutlineColor property.
        /// </summary>
        /// <param name="d">The owning dependency object</param>
        /// <param name="e">The dependency arguments of the change</param>
        private static void OnStarOutlineColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RatingsControl control = (RatingsControl)d;
            foreach (StarControl star in control.spStars.Children)
            {
                star.StarOutlineColor = (SolidColorBrush)e.NewValue;
            }
        }

        /// <summary>
        /// Handles changes to the Value property.
        /// </summary>
        /// <param name="d">The owning dependency object</param>
        /// <param name="e">The dependency arguments of the change</param>
        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.CoerceValue(MinimumProperty);
            d.CoerceValue(MaximumProperty);
            RatingsControl ratingsControl = (RatingsControl)d;
            SetupStars(ratingsControl);
        }

        /// <summary>
        /// Handles changes to the NumberOfStars property.
        /// </summary>
        /// <param name="d">The owning dependency object</param>
        /// <param name="e">The dependency arguments of the change</param>
        private static void OnNumberOfStarsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.CoerceValue(MinimumProperty);
            d.CoerceValue(MaximumProperty);
            RatingsControl ratingsControl = (RatingsControl)d;
            SetupStars(ratingsControl);
        }

#endregion On DP Changed

        #region Coerce DP

        /// <summary>
        /// Coerces the Value value.
        /// </summary>
        /// <param name="d">The owning dependency object</param>
        /// <param name="value">The value to coerces</param>
        /// <returns>The coerced value</returns>
        private static object CoerceValueValue(DependencyObject d, object value)
        {
            RatingsControl ratingsControl = (RatingsControl)d;
            decimal current = (decimal)value;

            if (current < ratingsControl.Minimum)
            {
                current = ratingsControl.Minimum;
            }

            if (current > ratingsControl.Maximum)
            {
                current = ratingsControl.Maximum;
            }

            return current;
        }

        /// <summary>
        /// Coerces the NumberOfStars value.
        /// </summary>
        /// <param name="d">The owning dependency object</param>
        /// <param name="value">The value to coerces</param>
        /// <returns>The coerced value</returns>
        private static object CoerceNumberOfStarsValue(DependencyObject d, object value)
        {
            RatingsControl ratingsControl = (RatingsControl)d;
            decimal current = ((decimal?)value).GetValueOrDefault((decimal)0.0);
            
            if (current < ratingsControl.Minimum)
            {
                current = ratingsControl.Minimum;
            }

            if (current > ratingsControl.Maximum)
            {
                current = ratingsControl.Maximum;
            }

            return current;
        }

        #endregion Coerce DP

        /// <summary>
        /// Sets up stars when Value or NumberOfStars properties change
        /// Will only show up to the number of stars requested (up to Maximum)
        /// so if Value > NumberOfStars * 1, then Value is clipped to maximum
        /// number of full stars
        /// </summary>
        /// <param name="ratingsControl">The owning ratings control</param>
        private static void SetupStars(RatingsControl ratingsControl)
        {
            decimal localValue = ratingsControl.Value;

            ratingsControl.spStars.Children.Clear();
            for (int i = 0; i < ratingsControl.NumberOfStars; i++)
            {
                StarControl star = new StarControl();
                star.BackgroundColor = ratingsControl.BackgroundColor;
                star.StarForegroundColor = ratingsControl.StarForegroundColor;
                star.StarOutlineColor = ratingsControl.StarOutlineColor;
                if (localValue > 1)
                {
                    star.Value = 1.0m;
                }
                else if (localValue > 0)
                {
                    star.Value = localValue;
                }
                else
                {
                    star.Value = 0.0m;
                }

                localValue -= 1.0m;
                ratingsControl.spStars.Children.Insert(i, star);
            }
        }
    }
}
