using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp.Models
{
    /// <summary>
    /// Simple Error class
    /// </summary>
    public class ErrorDto
    {
        /// <summary>
        /// Error code
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Error Message
        /// </summary>
        public string Message { get; set; }


        /// <summary>
        /// Other Fields
        /// </summary>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
