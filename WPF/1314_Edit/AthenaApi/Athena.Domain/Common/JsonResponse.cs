using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Common
{
    public class JsonResponse
    {
        #region Private members.

        //Defining properties with this approach is not required in general context howoever helps in partial 
        //overriding the 'Ok' and Err flags automatically without manual assignment.
        private bool ok = false;
        private object response = null;
        private object extraData = null; //Any extra data
        private JsonError error;
        private List<JsonError> errorsList = null;
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonReturnObject"/> class.
        /// </summary>
        public JsonResponse()
        {
            this.Response = null;
            this.Ok = false;
            this.Error = new JsonError();
        }

        public JsonResponse(object response)
        {
            this.Response = response;
            this.Ok = true;
        }

        public JsonResponse(JsonError jsonError)
        {
            this.Response = null;
            this.Ok = false;
            this.Error = jsonError;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="JsonReturnObject"/> is success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if success; otherwise, <c>false</c>.
        /// </value>
        public bool Ok
        {
            get { return ok; }
            set { ok = value; }
        }

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        public JsonError Error
        {
            get { return error; }

            set
            {
                error = value;
                ok = false;
            }
        }

        /// <summary>
        /// Gets or sets the res.
        /// </summary>
        /// <value>
        /// The res.
        /// </value>
        public object Response
        {
            get { return response; }
            set
            {
                response = value;
                ok = true;
            }
        }

        /// <summary>
        /// Gets or sets the res.
        /// </summary>
        /// <value>
        /// The res.
        /// </value>
        public object ExtraData
        {
            get { return extraData; }
            set
            {
                extraData = value;
            }
        }
    }
}
