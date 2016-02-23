using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using SerialPortUtility.Helpers;

namespace SerialPortUtility.Behaviors
{
    // From StackOverflow: http://stackoverflow.com/questions/1268552/how-do-i-get-a-textbox-to-only-accept-numeric-input-in-wpf

    /// <summary>
    ///     Regular expression for Textbox with properties:
    ///     <see cref="RegularExpression" />,
    ///     <see cref="MaxLength" />,
    ///     <see cref="EmptyValue" />.
    /// </summary>
    public class TextBoxInputRegExBehaviour : Behavior<ComboBox>
    {
        #region DependencyProperties

        public static readonly DependencyProperty RegularExpressionProperty =
            DependencyProperty.Register("TextBoxInputRegExBehaviour", typeof (string),
                typeof (TextBoxInputRegExBehaviour), null);

        public static readonly DependencyProperty MaxLengthProperty =
            DependencyProperty.Register("MaxLength", typeof (int), typeof (TextBoxInputRegExBehaviour),
                new FrameworkPropertyMetadata(int.MinValue));

        public static readonly DependencyProperty EmptyValueProperty =
            DependencyProperty.Register("EmptyValue", typeof (string), typeof (TextBoxInputRegExBehaviour), null);

        public string RegularExpression
        {
            get { return (string) GetValue(RegularExpressionProperty); }
            set { SetValue(RegularExpressionProperty, value); }
        }

        public int MaxLength
        {
            get { return (int) GetValue(MaxLengthProperty); }
            set { SetValue(MaxLengthProperty, value); }
        }

        public string EmptyValue
        {
            get { return (string) GetValue(EmptyValueProperty); }
            set { SetValue(EmptyValueProperty, value); }
        }

        #endregion

        /// <summary>
        ///     Attach our behaviour. Add event handlers
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.PreviewTextInput += PreviewTextInputHandler;
            AssociatedObject.PreviewKeyDown += PreviewKeyDownHandler;
            DataObject.AddPastingHandler(AssociatedObject, PastingHandler);
        }

        /// <summary>
        ///     Deattach our behaviour. remove event handlers
        /// </summary>
        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.PreviewTextInput -= PreviewTextInputHandler;
            AssociatedObject.PreviewKeyDown -= PreviewKeyDownHandler;
            DataObject.RemovePastingHandler(AssociatedObject, PastingHandler);
        }

        #region Event handlers [PRIVATE] --------------------------------------

        private void PreviewTextInputHandler(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = AssociatedObject.GetTextBox();

            string text = textBox.Text.Insert(textBox.CaretIndex, e.Text);

            e.Handled = !ValidateText(text);
        }

        /// <summary>
        ///     PreviewKeyDown event handler
        /// </summary>
        private void PreviewKeyDownHandler(object sender, KeyEventArgs e)
        {
            TextBox textBox = AssociatedObject.GetTextBox();

            if (string.IsNullOrEmpty(EmptyValue))
                return;

            string text = null;

            // Handle the Backspace key
            if (e.Key == Key.Back)
            {
                if (!TreatSelectedText(out text))
                {
                    if (textBox.SelectionStart > 0)
                        text = textBox.Text.Remove(textBox.SelectionStart - 1, 1);
                }
            }
                // Handle the Delete key
            else if (e.Key == Key.Delete)
            {
                // If text was selected, delete it
                if (!TreatSelectedText(out text))
                {
                    // Otherwise delete next symbol
                    text = textBox.Text.Remove(textBox.SelectionStart, 1);
                }
            }

            if (text == string.Empty)
            {
                textBox.Text = EmptyValue;
                if (e.Key == Key.Back)
                    textBox.SelectionStart++;
                e.Handled = true;
            }
        }

        private void PastingHandler(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(DataFormats.Text))
            {
                string text = Convert.ToString(e.DataObject.GetData(DataFormats.Text));

                if (!ValidateText(text))
                    e.CancelCommand();
            }
            else
                e.CancelCommand();
        }

        #endregion Event handlers [PRIVATE] -----------------------------------

        #region Auxiliary methods [PRIVATE] -----------------------------------

        /// <summary>
        ///     Validate certain text by our regular expression and text length conditions
        /// </summary>
        /// <param name="text"> Text for validation </param>
        /// <returns> True - valid, False - invalid </returns>
        private bool ValidateText(string text)
        {
            return (new Regex(RegularExpression, RegexOptions.IgnoreCase)).IsMatch(text) &&
                   (MaxLength == 0 || text.Length <= MaxLength);
        }

        /// <summary>
        ///     Handle text selection
        /// </summary>
        /// <returns>true if the character was successfully removed; otherwise, false. </returns>
        private bool TreatSelectedText(out string text)
        {
            text = null;
            if (AssociatedObject.GetTextBox().SelectionLength > 0)
            {
                text = AssociatedObject.Text.Remove(AssociatedObject.GetTextBox().SelectionStart,
                    AssociatedObject.GetTextBox().SelectionLength);
                return true;
            }
            return false;
        }

        #endregion Auxiliary methods [PRIVATE] --------------------------------
    }
}