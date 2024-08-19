using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Services
{
    public class SubscriptionsWriteService : ISubscriptionsWriteService
    {
        public Guid CreateSubscription(string suscriptionType, Guid adminId)
        {
            return Guid.NewGuid();
        }
    }
}
