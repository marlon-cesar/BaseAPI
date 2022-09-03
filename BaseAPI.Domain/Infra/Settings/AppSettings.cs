using System;
using System.Collections.Generic;
using System.Text;

namespace BaseAPI.Domain.Infra.Settings
{
    public class AppSettings
    {
        public TokenSettings? TokenSettings { get; set; }
        public ConnectionStrings? ConnectionStrings { get; set; }
        public SendGridSettings? SendGridSettings { get; set; }
        public string? Url { get; set; }
    }
}
