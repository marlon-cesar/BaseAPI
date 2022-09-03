using System;
using System.Collections.Generic;
using System.Text;

namespace BaseAPI.Domain.Infra.Settings
{
    public class SendGridSettings
    {
        public string? ChaveSendGrid { get; set; }
        public string? Remetente { get; set; }
    }
}
