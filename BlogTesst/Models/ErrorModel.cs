using Newtonsoft.Json;

namespace BlogTesstAPI.Models
{
    internal class ErrorModel
    {
        /// <summary>
        /// Api error message
        /// </summary>
        [JsonProperty("error_message")]
        public string ErrorMessage { get; set; }

        /// <summary>
        ///  Api error code
        /// </summary>
        [JsonProperty("error_code")]
        public int ErrorCode { get; set; }
    }
}
