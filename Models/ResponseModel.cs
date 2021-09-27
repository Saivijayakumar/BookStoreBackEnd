using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class ResponseModel<T>
    {
        /// <summary>
        /// Gets or sets a value indicating whether Status is True
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Message is Given
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Data is Given
        /// </summary>
        public T Data { get; set; }
    }
}
