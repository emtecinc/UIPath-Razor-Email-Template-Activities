//Copyright(c) 2019 Emtec Inc

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

namespace Emtec.EmailTemplate.Design
{
    using System.Activities.Presentation.Metadata;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Controls;

    public partial class RazorMailSenderDesinger
    {
        public RazorMailSenderDesinger()
        {
            InitializeComponent();
        }

        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            builder.AddCustomAttributes(
                typeof(EmailTemplateActivity),
                new DesignerAttribute(typeof(RazorMailSenderDesinger)),
                new DescriptionAttribute("Razor Engine Template"),
                new ToolboxBitmapAttribute(typeof(EmailTemplateActivity), "Emteclogo.png"));
        }

        private void btnAdd_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Razor files (*.cshtml)|*.CSHTML";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    ModelItem.Properties["FilePathOrContent"].SetValue(new System.Activities.InArgument<string>(openFileDialog.FileName));
                }
                catch (System.Exception ex)
                {
                    throw new System.Exception(ex.Message);
                }
            }
        }

        private void CheckBox_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            CheckBox ch = (CheckBox)sender;
            if (ch.IsChecked.Value == true)
            {
                ModelItem.Properties["isFileSelection"].SetValue(new System.Activities.InArgument<bool>(true));
              
            }
            else
            {
                ModelItem.Properties["isFileSelection"].SetValue(new System.Activities.InArgument<bool>(false));
            }
            ModelItem.Properties["FilePathOrContent"].SetValue(new System.Activities.InArgument<string>());
        }
    }
}
