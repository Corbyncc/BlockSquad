using System;
using System.Collections.Generic;
using System.Text;

namespace BlockSquad.Sdk
{
    public class BlockSquadApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
    }
}
