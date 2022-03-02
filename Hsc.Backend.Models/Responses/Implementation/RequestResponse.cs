using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsc.Backend.Models.Responses
{
    public class RequestResponse : IRequestResponse
    {
        public string? Status { get; set; }
        public string? Message { get; set; }
    }
}
