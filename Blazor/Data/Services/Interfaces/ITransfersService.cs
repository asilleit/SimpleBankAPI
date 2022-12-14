using Blazor.Data.Models;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
namespace Blazor.Data.Services.Interfaces
{
    public interface ITransfersService
    {
        Task<(bool, string?, string?)> Transfer(Transfer transfer);
    }
}