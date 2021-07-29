using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace API.Helpers
{
    [Serializable]
    [DataContract]
    public class ApiResponse
    {
        [JsonConstructor]
        public ApiResponse(int statusCode, string message = null, object result = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
            Result = result;
        }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad request",

                401 => "You are not, authorized",

                404 => "Resource not found",

                500 => "Error",

                _ => null 
            };
        }

        [DataMember]
        public int StatusCode { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public object Result {get; set;} = null;
    }
}