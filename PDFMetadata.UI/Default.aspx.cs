using Codeuctivity;
using PDFMetadata.UI.Logger;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web.UI;

namespace PDFMetadata.UI
{
    public partial class _Default : Page
    {
        /// <summary>
        /// Folder in root of the application for for temp pdfa files
        /// </summary>
        const string PATH_FOR_UPLOAD = "~/TempFiles/";

        /// <summary>
        /// 
        /// </summary>
        const string SUPPORTED_CONTENT_TYPE = "application/pdf";
        
        /// <summary>
        /// Folder in root of the application for logging exceptions
        /// </summary>
        const string PATH_FOR_LOGS = "~/Logs/";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void btnUploadFile_Click(object sender, EventArgs e)
        {
            try
            {
                await PdfaFileValidation();
            }
            catch (Exception ex)
            {
                FileLogger.Log(ex, Server.MapPath(PATH_FOR_LOGS));
                lblMessage.Text = "Uncaught exception, please contact support.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        /// <summary>
        /// Validation of the pdf file and customization UI according to the result.
        /// </summary>
        /// <returns></returns>
        private async Task PdfaFileValidation()
        {
            if (fileUpload.HasFile && fileUpload.PostedFile.ContentType == SUPPORTED_CONTENT_TYPE)
            {
                GetFilePath(out string newFilePath);

                fileUpload.SaveAs(newFilePath);

                var profileName = await GetValidationProfileName(newFilePath);

                File.Delete(newFilePath);

                lblMessage.Text = $"File is valid ({profileName}).";
                lblMessage.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblMessage.Text = "Please select a file [.pdf] to upload.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void GetFilePath(out string newFilePath)
        {
            if (!Directory.Exists(Server.MapPath(PATH_FOR_UPLOAD)))
                Directory.CreateDirectory(Server.MapPath(PATH_FOR_UPLOAD));

            newFilePath = Server.MapPath(PATH_FOR_UPLOAD + fileUpload.FileName);
        }

        private async Task<string> GetValidationProfileName(string newFilePath)
        {
            using (var pdfAValidator = new PdfAValidator())
            {
                var result = await pdfAValidator.ValidateWithDetailedReportAsync(newFilePath);
                return result.Jobs.Job.ValidationReport.ProfileName;
            }
        }
    }
}