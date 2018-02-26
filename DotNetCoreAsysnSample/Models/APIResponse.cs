using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DotNetCoreAsysnSample.Models
{
    /// <summary>
    ///     Bad API response model
    /// </summary>
    public class ApiResponse
    {
        public bool Status { get; set; }
        public string Error { get; set; }
        public object DeveloperMessage { get; set; }
        public ModelStateDictionary ModelState { get; set; }
    }
}