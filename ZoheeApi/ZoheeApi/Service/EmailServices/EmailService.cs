using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

namespace FIN.Service.EmailServices
{
    public class EmailService : IEmailService
    {
        private readonly string smtpServer = "smtp.gmail.com";
        private readonly int smtpPort = 587;
        private readonly string smtpUser = "testmaila67@gmail.com";
        private readonly string smtpPass = "ccyd lrnw gofv qprg";

        public async Task EMailAsync(string toEmail, string subject, string htmlMessage)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("testmaila67@gmail.com", smtpUser));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;

            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = htmlMessage
            };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(smtpServer, smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(smtpUser, smtpPass);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }


        /*
         * HELPER METHOD -> sends an OTP verification email
         */
        public async Task SendSignatureRequestEmailAsync(string email, string documentName, int documentId, int userId)
        {
            // Construct the link based on your requirements
            // Format: http://localhost:4200/#/pdf-signer/email/documentId/pdfName
            string signatureLink = $"http://localhost:4200/#/pdf-signer/{email}/{documentId}/{userId}/{documentName}";

            string htmlMessage = $@"
    <body style=""margin:0; padding:0; background-color:#f8fafc; font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;"">
      <table width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""background-color:#f8fafc; padding:40px 0;"">
        <tr>
          <td align=""center"">

            <table width=""600"" cellpadding=""0"" cellspacing=""0"" style=""max-width:600px; background:#ffffff; border:1px solid #e2e8f0; border-collapse:collapse;"">

              <tr>
                <td style=""background:#0f172a; padding:30px; text-align:left;"">
                  <table width=""100%"">
                    <tr>
                        <td style=""width:32px; background:#2563eb; padding:8px;"">
                             </td>
                        <td style=""padding-left:15px;"">
                            <h2 style=""margin:0; color:#ffffff; font-size:18px; text-transform:uppercase; letter-spacing:2px; font-weight:900;"">
                                DocuVault
                            </h2>
                        </td>
                    </tr>
                  </table>
                </td>
              </tr>

              <tr>
                <td style=""padding:50px 40px;"">
                  <p style=""margin:0 0 10px 0; font-size:10px; font-weight:900; color:#64748b; text-transform:uppercase; letter-spacing:2px;"">
                    Signature Required
                  </p>
                  <h1 style=""margin:0 0 25px 0; font-size:24px; font-weight:900; color:#0f172a; text-transform:uppercase; letter-spacing:-1px;"">
                    Review and Sign Document
                  </h1>
                  
                  <p style=""font-size:15px; line-height:1.6; color:#475569; margin-bottom:30px;"">
                    You have been requested to review and electronically sign the following document: <br/>
                    <strong style=""color:#0f172a;"">{documentName}</strong>
                  </p>

                  <table border=""0"" cellpadding=""0"" cellspacing=""0"" style=""margin: 30px 0;"">
                    <tr>
                      <td align=""center"" bgcolor=""#2563eb"" style=""border-radius:0px;"">
                        <a href=""{signatureLink}"" target=""_blank"" style=""padding: 16px 32px; font-size: 12px; font-weight: 900; color: #ffffff; text-decoration: none; text-transform: uppercase; letter-spacing: 2px; display: inline-block;"">
                          Review & Sign Document
                        </a>
                      </td>
                    </tr>
                  </table>

                  <p style=""font-size:12px; color:#94a3b8; line-height:1.5;"">
                    Note: This link is unique to your email address. Do not share this link with others.
                  </p>
                </td>
              </tr>

              <tr>
                <td style=""padding:30px 40px; background:#f1f5f9; border-top:1px solid #e2e8f0;"">
                  <p style=""font-size:10px; font-weight:bold; color:#64748b; text-transform:uppercase; letter-spacing:1px; margin:0;"">
                    © {DateTime.UtcNow.Year} DocuVault Enterprise. All rights reserved.
                  </p>
                  <p style=""font-size:10px; color:#94a3b8; margin-top:5px;"">
                    Electronic Signatures are legally binding under the ESIGN Act.
                  </p>
                </td>
              </tr>

            </table>

          </td>
        </tr>
      </table>
    </body>";

            await EMailAsync(email, "Signature Requested: " + documentName, htmlMessage);
        }


        /*
 * HELPER METHOD -> sends a password reset email (link-only)
 * Must NOT reveal whether the email exists
 */
        public async Task SendDocumentSignedNotificationAsync(string adminEmail, string signeeName, string documentName)
        {
            string htmlMessage = $@"
    <body style=""margin:0; padding:0; background-color:#f8fafc; font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;"">
      <table width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""background-color:#f8fafc; padding:40px 0;"">
        <tr>
          <td align=""center"">

            <table width=""600"" cellpadding=""0"" cellspacing=""0"" style=""max-width:600px; background:#ffffff; border:1px solid #e2e8f0; border-collapse:collapse;"">

              <tr>
                <td style=""background:#0f172a; padding:30px; text-align:left;"">
                  <table width=""100%"">
                    <tr>
                        <td style=""width:32px; background:#2563eb; padding:8px;"">
                             </td>
                        <td style=""padding-left:15px;"">
                            <h2 style=""margin:0; color:#ffffff; font-size:18px; text-transform:uppercase; letter-spacing:2px; font-weight:900;"">
                                DocuVault
                            </h2>
                        </td>
                    </tr>
                  </table>
                </td>
              </tr>

              <tr>
                <td style=""padding:50px 40px;"">
                  <p style=""margin:0 0 10px 0; font-size:10px; font-weight:900; color:#2563eb; text-transform:uppercase; letter-spacing:2px;"">
                    Status Update
                  </p>
                  <h1 style=""margin:0 0 25px 0; font-size:24px; font-weight:900; color:#0f172a; text-transform:uppercase; letter-spacing:-1px;"">
                    Document Signed
                  </h1>
                  
                  <p style=""font-size:15px; line-height:1.6; color:#475569; margin-bottom:20px;"">
                    This is an automated notification to confirm that a document has been successfully signed.
                  </p>

                  <table width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""background:#f1f5f9; border-left:4px solid #0f172a; margin-bottom:30px;"">
                    <tr>
                        <td style=""padding:20px;"">
                            <p style=""margin:0; font-size:12px; color:#64748b; text-transform:uppercase; font-weight:bold;"">Signee</p>
                            <p style=""margin:5px 0 15px 0; font-size:16px; color:#0f172a; font-weight:bold;"">{signeeName}</p>
                            
                            <p style=""margin:0; font-size:12px; color:#64748b; text-transform:uppercase; font-weight:bold;"">Document</p>
                            <p style=""margin:5px 0 0 0; font-size:16px; color:#0f172a; font-weight:bold;"">{documentName}</p>
                        </td>
                    </tr>
                  </table>

                  <p style=""font-size:12px; color:#94a3b8; line-height:1.5;"">
                    No further action is required from your side. The document status has been updated in your dashboard.
                  </p>
                </td>
              </tr>

              <tr>
                <td style=""padding:30px 40px; background:#f1f5f9; border-top:1px solid #e2e8f0;"">
                  <p style=""font-size:10px; font-weight:bold; color:#64748b; text-transform:uppercase; letter-spacing:1px; margin:0;"">
                    © {DateTime.UtcNow.Year} DocuVault Enterprise. All rights reserved.
                  </p>
                </td>
              </tr>

            </table>

          </td>
        </tr>
      </table>
    </body>";

            await EMailAsync(adminEmail, "Signed: " + documentName, htmlMessage);
        }

    }
}
