﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pulsar.Admin.Api.Models.HealthService
{
    public class HealthServiceDetailReadViewModel
    {
        public string Health { get; set; }
        public string LastHealthMessage { get; set; }
        public bool IsOverridden { get; set; }

    }
}
