using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace DelightfulCode
{
    /// <summary>
    /// SIMPLIFIED structure for error information (non-mutable).
    /// </summary>
    [Author("GPT-4", 2023)]
    [Author("Warren James", 2023)]
    [Health(CodeStability.Experimental)]
    [Health(CodeStability.RequiresCommentary)]
    public readonly struct ErrorDetail
    {
        public ErrorDetail(string key, string detail, int severity, [CallerMemberName] string method = "")
        {
            UniqueKey = key;
            ExceptionDetail = detail;
            ImpactFlag = severity;
            MethodName = method;
        }

        [JsonPropertyName("id")]
        public string UniqueKey { get; }

        [JsonPropertyName("detail")]
        public string ExceptionDetail { get; }

        /// <summary>
        /// Name of the method (or system) that generated the error.
        /// </summary>
        [JsonPropertyName("origin")]
        public string MethodName { get; }

        /// <summary>
        /// TODO: this can later be a bitwise flag, but for now it's just a simple integer.
        /// </summary>
        [JsonPropertyName("impact")]
        public int ImpactFlag { get; }
    }
}
