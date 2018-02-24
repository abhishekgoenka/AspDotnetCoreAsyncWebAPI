using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DotNetCoreAsysnSample.Models
{
    /// <summary>
    /// Bad API response model
    /// </summary>
    public class APIResponse
    {
        public bool Status { get; set; }
        public string Error { get; set; }
        public ModelStateDictionary ModelState { get; set; }
    }
}
