using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Common
{
    public class JsonError
    {
        public JsonError()
        {
            this.ErrorMessage = string.Empty;
            this.ErrorDetails = string.Empty;
            this.ErrorNumber = 0;
        }

        /// <summary>
        /// Overloaded constructor to initialise with default error message. 
        /// Err number is assumed to be -1 as the error message has been specified.
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="errorDetails"></param>
        public JsonError(string errorMessage, string errorDetails)
        {
            this.ErrorMessage = errorMessage;
            this.ErrorDetails = errorDetails;
            this.ErrorNumber = -1;
        }

        /// <summary>
        /// Overloaded constructor to initialise with default error number and error message
        /// </summary>
        /// <param name="errorNumber"></param>
        /// <param name="errorMessage"></param>
        public JsonError(int errorNumber, string errorMessage, string errorDetails)
        {
            this.ErrorMessage = errorMessage;
            this.ErrorDetails = errorDetails;
            this.ErrorNumber = errorNumber;
        }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets the error message details especially for stacktrace.
        /// </summary>
        /// <value>
        /// The error message detail.
        /// </value>
        public string ErrorDetails { get; set; }

        /// <summary>
        /// Gets or sets the error number.
        /// </summary>
        /// <value>
        /// The error number.
        /// </value>
        public int ErrorNumber { get; set; }
    }
}
