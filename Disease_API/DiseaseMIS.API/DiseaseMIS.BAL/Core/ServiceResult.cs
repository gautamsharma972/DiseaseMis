using System.Collections.Generic;

namespace DiseaseMIS.BAL.Core
{
    public class ServiceResult
    {
        public ServiceResult(string message = "")
        {
            Message = message;
            Errors = new List<string>();
        }
        public bool HasError => Errors != null && Errors.Count > 0;
        public List<string> Errors { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public object Extras { get; set; }
    }
}
