using System;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IEmailConfirmationService
    {
        public Task ConfirmAsync(Guid confirmationCode, Guid userId);
    }
}
