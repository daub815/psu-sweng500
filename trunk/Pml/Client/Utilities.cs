namespace Sweng500.Pml.Client
{
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// Various utilities needed for the client
    /// </summary>
    public static class Utilities
    {
        /// <summary>
        /// A method that will recursively look up the visual stree of the provided child
        /// </summary>
        /// <typeparam name="T">The type of control to find</typeparam>
        /// <param name="child">The child to find the parent of</param>
        /// <returns>The first visual parent in the visual tree</returns>
        public static T FindVisualParent<T>(this DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            if (null == parentObject)
            {
                return null;
            }

            T parent = parentObject as T;
            if (parent != null)
            {
                return parent;
            }
            else
            {
                return FindVisualParent<T>(parentObject);
            }
        }
    }
}
