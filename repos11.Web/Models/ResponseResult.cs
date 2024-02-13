using Newtonsoft.Json;
using System;
using System.Collections;
using System.ComponentModel;

namespace repos11.Web.Models
{
    public class ResponseResult<TResult>
    {
        public ResponseResult()
        {
            isError = false;
        }

        public TResult data { get; set; }
        public bool isError { get; set; }
        public Exception exception { get; set; }
        public string errorMessage
        {
            get
            {
                var msg = string.Empty;

                if (exception != null)
                {
                    msg = exception.Message;

                    var innerEx = exception.InnerException;

                    while (innerEx != null)
                    {
                        msg += $"\n. InnerException : {innerEx.Message}"; ;
                        innerEx = innerEx.InnerException;
                    }
                }

                return msg;
            }
        }
    }

    public class ResponseLoadResult
    {
        public ResponseLoadResult()
        {
            isError = false;
        }

        public IEnumerable data { get; set; }

        [DefaultValue(-1)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int totalCount { get; set; } = -1;


        [DefaultValue(-1)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int groupCount { get; set; } = -1;


        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public object[] summary { get; set; }

        public bool isError { get; set; }
        public Exception exception { get; set; }
        public string errorMessage
        {
            get
            {
                var msg = string.Empty;

                if (exception != null)
                {
                    msg = exception.Message;

                    var innerEx = exception.InnerException;

                    while (innerEx != null)
                    {
                        msg += $"\n. InnerException : {innerEx.Message}"; ;
                        innerEx = innerEx.InnerException;
                    }
                }

                return msg;
            }
        }
    }
}