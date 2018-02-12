using DotNetCoreAsysnSample.Models;
using DotNetCoreAsysnSample.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetCoreAsysnSample.Repository
{
    /// <summary>
    /// Customer's async repo contract
    /// </summary>
    public interface ICustomersRepositoryAsync
    {
        Task<List<Customer>> GetCustomersAsync();
        Task<PagingResult<Customer>> GetCustomersPageAsync(int skip, int take);
        Task<Customer> GetCustomerAsync(int id);
    }
}
