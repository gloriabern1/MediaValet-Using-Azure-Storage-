using AzureStorageServices.Entities;
using SupervisorAPI.Models;
using System.Collections.Generic;

namespace SupervisorAPI.Extension
{
    public static class ToConfirmationDTO
    {
        public static IEnumerable<ConfirmationDTO> ToDTO(this IEnumerable<Confirmations> confirmations)
        {
            if (confirmations == null) return null;

            var dto = new List<ConfirmationDTO>();

            foreach (var confirmation in confirmations)
            {
                dto.Add(new ConfirmationDTO()
                {
                    AgentId = confirmation.AgentId,
                    OrderId = confirmation.OrderId,
                    OrderStatus = confirmation.OrderStatus
                });
            }

            return dto;
        }
    }
}