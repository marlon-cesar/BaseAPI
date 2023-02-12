using System;

namespace BaseAPI.API.Controllers.Common
{
    public class FailDto
    {
        public FailDto(Exception ex)
        {
            Message = ex.Message;
            StackTrace = ex.StackTrace;
            Details = ex.ToString();
        }

        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string Details { get; set; }
    }
}
