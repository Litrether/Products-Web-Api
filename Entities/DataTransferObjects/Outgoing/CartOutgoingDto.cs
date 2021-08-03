using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.Outgoing
{
    public class CartOutgoingDto
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string User { get; set; }

    }
}
