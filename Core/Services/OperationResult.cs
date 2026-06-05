using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OperationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public static OperationResult Ok() =>
            new OperationResult { Success = true };

        public static OperationResult Fail(string message) =>
            new OperationResult { Success = false, Message = message };
    }
}
