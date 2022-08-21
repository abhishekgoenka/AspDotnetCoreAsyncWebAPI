using System.Collections.Generic;

namespace DotNetCoreAsysnSample.ViewModel
{
    /// <summary>
    ///     Page result view model
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct PagingResult<T>
    {
        public IEnumerable<T> Records { get; set; }
        public int TotalRecords { get; set; }

        public PagingResult(IEnumerable<T> items, int totalRecords)
        {
            TotalRecords = totalRecords;
            Records = items;
        }
    }
}