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

namespace Emtec.EmailTemplate
{
    using System.Activities;
    using System.ComponentModel;
    using System.IO;
    using Newtonsoft.Json;
    using RazorEngine.Templating;
    public class EmailTemplateActivity : CodeActivity
    {
        [Category("CSHTMl Template")]
        [DisplayName("File Path Or File Content")]
        public InArgument<string> FilePathOrContent
        {
            get; set;
        }

        [Category("Input Object")]
        [DisplayName("CsHTML Model Object")]
        public InArgument<object> CsHtmlModel
        {
            get; set;
        }

        [Category("Output")]
        [DisplayName("Result")]
        public OutArgument<string> Result
        {
            get; set;
        }

        [Browsable(false)]
        public InArgument<bool> isFileSelection { get; set; }


        public EmailTemplateActivity()
        {
            isFileSelection = true;
        }

        public dynamic CreateAnonymousObject(object jsonString)
        {
            try
            {
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(jsonString));
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("Exception in EmailTemplate : -\n" + ex.Message + " \n" + ex.StackTrace);
            }
        }
        protected override void Execute(CodeActivityContext context)
        {
            try
            {
                var templateService = new TemplateService();
                dynamic data = CreateAnonymousObject(this.CsHtmlModel.Get(context));
                string Filedata;
                if (this.isFileSelection.Get(context) == true)
                    Filedata = File.ReadAllText(this.FilePathOrContent.Get(context));
                else
                    Filedata = this.FilePathOrContent.Get(context).ToString().Replace("'", "\"");
                Result.Set(context, templateService.Parse(Filedata, data, null, null));
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("Exception in EmailTemplate : -\n" + ex.Message + " \n" + ex.StackTrace);
            }
        }
    }
}