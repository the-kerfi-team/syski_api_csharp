﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Syski.API.Models
{
    public class StorageDataDTO
    {

        public float Time { get; set; }

        public float Transfers { get; set; }

        public float Reads { get; set; }

        public float Writes { get; set; }

        public float ByteReads { get; set; }

        public float ByteWrites { get; set; }

        public DateTime CollectionDateTime { get; set; }

    }
}
