using System.Windows;
using System.Windows.Controls;

namespace WpfApplication1
{
    public class ComboBoxItemTemplateChooser : DataTemplateSelector
    {
        #region SelectedTemplate
        public static DependencyProperty SelectedTemplateProperty = DependencyProperty.RegisterAttached("SelectedTemplate",
                                                typeof(DataTemplate), typeof(ComboBoxItemTemplateChooser), new UIPropertyMetadata(null));

        [AttachedPropertyBrowsableForType(typeof(ComboBox))]
        public static DataTemplate GetSelectedTemplate(ComboBox obj)
        {
            return (DataTemplate)obj.GetValue(SelectedTemplateProperty);
        }

        public static void SetSelectedTemplate(ComboBox obj, DataTemplate value)
        {
            obj.SetValue(SelectedTemplateProperty, value);
        }

        #endregion // SelectedTemplate

        #region DropDownTemplate
        public static DependencyProperty DropDownTemplateProperty = DependencyProperty.RegisterAttached("DropDownTemplate",
                                                typeof(DataTemplate), typeof(ComboBoxItemTemplateChooser), new UIPropertyMetadata(null));

        [AttachedPropertyBrowsableForType(typeof(ComboBox))]
        public static DataTemplate GetDropDownTemplate(ComboBox obj)
        {
            return (DataTemplate)obj.GetValue(DropDownTemplateProperty);
        }

        public static void SetDropDownTemplate(ComboBox obj, DataTemplate value)
        {
            obj.SetValue(DropDownTemplateProperty, value);
        }

        #endregion // DropDownTemplate

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            ComboBox parentComboBox = null;
            ComboBoxItem comboBoxItem = container.GetVisualParent<ComboBoxItem>();
            if (comboBoxItem == null)
            {
                parentComboBox = container.GetVisualParent<ComboBox>();
                return ComboBoxItemTemplateChooser.GetSelectedTemplate(parentComboBox);
            }
            parentComboBox = ComboBox.ItemsControlFromItemContainer(comboBoxItem) as ComboBox;
            return ComboBoxItemTemplateChooser.GetDropDownTemplate(parentComboBox);
        }
    }
}
