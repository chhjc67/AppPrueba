using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace WpfApplication1
{
    class CheckBoxBehavior : Behavior<CheckBox>
    {
        protected override void OnAttached()
        {
            this.AssociatedObject.Click += AssociatedObject_Click;
        }

        void AssociatedObject_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (AssociatedObject.IsChecked == true)
                this.AssociatedObject.Foreground = Brushes.Green;
            else if (this.AssociatedObject.IsChecked == false)
                this.AssociatedObject.Foreground = Brushes.Red;
            else
                this.AssociatedObject.Foreground = Brushes.Black;
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.Click -= AssociatedObject_Click;
        }
    }
}
