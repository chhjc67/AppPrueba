using System.Windows.Controls;
using System.Windows.Interactivity;

namespace WpfApplication1
{
    class TargetedTriggerActionCheckBox : TargetedTriggerAction<TextBlock>
    {
        protected override void Invoke(object parameter)
        {
            this.Target.Text = (this.AssociatedObject as CheckBox).IsChecked != null ? 
                (this.AssociatedObject as CheckBox).IsChecked.ToString() : "Null";
        }
    }
}
