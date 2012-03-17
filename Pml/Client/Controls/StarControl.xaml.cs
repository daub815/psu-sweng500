namespace Sweng500.Pml.Client.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    /// <summary>
    /// Interaction logic for StarControl.xaml
    /// </summary>
    /// <remarks>Borrowed from Sacha Barber <see cref="http://www.codeproject.com/Articles/45210/WPF-A-Simple-Yet-Flexible-Rating-Control"/></remarks>
    public partial class StarControl : UserControl
    {
        #region Statics

        /// <summary>
        /// BackgroundColor Dependency Property
        /// </summary>
        public static readonly DependencyProperty BackgroundColorProperty = DependencyProperty.Register(
            "BackgroundColor",
            typeof(SolidColorBrush), 
            typeof(StarControl),
            new FrameworkPropertyMetadata(
                Brushes.Transparent,
                new PropertyChangedCallback(OnBackgroundColorChanged)));

        /// <summary>
        /// StarForegroundColor Dependency Property
        /// </summary>
        public static readonly DependencyProperty StarForegroundColorProperty = DependencyProperty.Register(
            "StarForegroundColor", 
            typeof(SolidColorBrush),
            typeof(StarControl),
            new FrameworkPropertyMetadata(
                Brushes.Transparent,
                new PropertyChangedCallback(OnStarForegroundColorChanged)));

        /// <summary>
        /// StarOutlineColor Dependency Property
        /// </summary>
        public static readonly DependencyProperty StarOutlineColorProperty = DependencyProperty.Register(
            "StarOutlineColor", 
            typeof(SolidColorBrush),
            typeof(StarControl),
            new FrameworkPropertyMetadata(
                Brushes.Transparent,
                new PropertyChangedCallback(OnStarOutlineColorChanged)));

        /// <summary>
        /// Value Dependency Property
        /// </summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value", 
            typeof(decimal),
            typeof(StarControl),
            new FrameworkPropertyMetadata(
                (decimal)0.0,
                new PropertyChangedCallback(OnValueChanged),
                new CoerceValueCallback(CoerceValueValue)));

        /// <summary>
        /// Maximum Dependency Property
        /// </summary>
        public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register(
            "Maximum", 
            typeof(decimal),
            typeof(StarControl),
            new FrameworkPropertyMetadata((decimal)1.0));

        /// <summary>
        /// Minimum Dependency Property
        /// </summary>
        public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register(
            "Minimum",
            typeof(decimal),
            typeof(StarControl),
            new FrameworkPropertyMetadata((decimal)0.0));

        /// <summary>
        /// Size of star
        /// </summary>
        private const int StarSize = 12;

        #endregion Statics

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the StarControl class
        /// </summary>
        public StarControl()
        {
            this.DataContext = this;
            InitializeComponent();

            gdStar.Width = StarSize;
            gdStar.Height = StarSize;
            gdStar.Clip = new RectangleGeometry
            {
                Rect = new Rect(0, 0, StarSize, StarSize)
            };

            mask.Width = StarSize;
            mask.Height = StarSize;
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

        #region DP Value Changed

        /// <summary>
        /// Handles changes to the BackgroundColor property.
        /// </summary>
        /// <param name="d">The owning dependency object</param>
        /// <param name="e">The property changed arguments</param>
        private static void OnBackgroundColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            StarControl control = (StarControl)d;
            control.gdStar.Background = (SolidColorBrush)e.NewValue;
            control.mask.Fill = (SolidColorBrush)e.NewValue;
        }

        /// <summary>
        /// Handles changes to the StarForegroundColor property.
        /// </summary>
        /// <param name="d">The owning dependency object</param>
        /// <param name="e">The property changed arguments</param>
        private static void OnStarForegroundColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            StarControl control = (StarControl)d;
            control.starForeground.Fill = (SolidColorBrush)e.NewValue;
        }

        /// <summary>
        /// Handles changes to the StarOutlineColor property.
        /// </summary>
        /// <param name="d">The owning dependency object</param>
        /// <param name="e">The property changed arguments</param>
        private static void OnStarOutlineColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            StarControl control = (StarControl)d;
            control.starOutline.Stroke = (SolidColorBrush)e.NewValue;
        }

        /// <summary>
        /// Handles changes to the Value property.
        /// </summary>
        /// <param name="d">The owning dependency object</param>
        /// <param name="e">The property changed arguments</param>
        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.CoerceValue(MinimumProperty);
            d.CoerceValue(MaximumProperty);
            StarControl starControl = (StarControl)d;
            if (starControl.Value == 0.0m)
            {
                starControl.starForeground.Fill = Brushes.Gray;
            }
            else
            {
                starControl.starForeground.Fill = starControl.StarForegroundColor;
            }

            int marginLeftOffset = (int)(starControl.Value * (decimal)StarSize);
            starControl.mask.Margin = new Thickness(marginLeftOffset, 0, 0, 0);
            starControl.InvalidateArrange();
            starControl.InvalidateMeasure();
            starControl.InvalidateVisual();
        }

        #endregion DP Value Changed

        #region Coerce DP

        /// <summary>
        /// Coerces the Value value.
        /// </summary>
        /// <param name="d">The owning dependency object</param>
        /// <param name="value">The value to coerce</param>
        /// <returns>The coerced object</returns>
        private static object CoerceValueValue(DependencyObject d, object value)
        {
            StarControl starControl = (StarControl)d;
            decimal current = (decimal)value;
            if (current < starControl.Minimum)
            {
                current = starControl.Minimum;
            }

            if (current > starControl.Maximum)
            {
                current = starControl.Maximum;
            }

            return current;
        }

        #endregion Coerce DP
    }
}
