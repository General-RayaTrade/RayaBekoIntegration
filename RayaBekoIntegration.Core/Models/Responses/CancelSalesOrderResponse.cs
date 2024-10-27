﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayaBekoIntegration.Core.Models.Responses
{
    public class CancelSalesOrderResponse : IStatus
    {
        public bool? status { set; get; }
        public string Message { set; get; }
    }
}
