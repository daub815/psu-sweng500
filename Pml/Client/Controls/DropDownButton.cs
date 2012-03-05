namespace Sweng500.Pml.Client.Controls
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;

    /// <summary>
    /// Provides a drop down for the button when clicked
    /// </summary>
    public class DropDownButton : ToggleButton
    {
        /// <summary>
        /// Property name for the DropDown Dependency Property
        /// </summary>
        public const string DropDownPropertyName = "DropDown";

        /// <summary>
        /// DependencyProperty for the ContextMenu
        /// </summary>
        public static readonly DependencyProperty DropDownProperty = 
            DependencyProperty.Register(DropDownPropertyName, typeof(ContextMenu), typeof(DropDownButton));

        /// <summary>
        /// Initializes a new instance of the DropDownButton class
        /// </summary>
        public DropDownButton()
            : base()
        {
            var binding = new Binding(DropDownPropertyName + "." + ContextMenu.IsOpenProperty.Name);
            binding.Source = this;
            binding.Mode = BindingMode.OneWay;
            this.SetBinding(IsCheckedProperty, binding);
        }

        /// <summary>
        /// Gets or sets the ContextMenu used when the button is clicked
        /// </summary>
        public ContextMenu DropDown
        {
            get
            {
                return (ContextMenu)this.GetValue(DropDownProperty);
            }

            set
            {
                this.SetValue(DropDownProperty, value);
            }
        }

        /// <summary>
        /// Displays the drop down when the button is clicked
        /// </summary>
        protected override void OnClick()
        {
            if (null != this.DropDown)
            {
                this.DropDown.PlacementTarget = this;
                this.DropDown.Placement = PlacementMode.Bottom;
                this.DropDown.IsOpen = true;
            }

            base.OnClick();
        }
    }
}
