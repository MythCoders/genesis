using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSIS.Core
{
    public enum AccountStatusEnum
    {
        [Description("Unvalidated")]
        Unvalidated = 0,

        [Description("Validated")]
        Validated = 5
    }

    public enum ResponseCodes
    {
        [Description("Success")]
        Success = 1,

        [Description("Validation Error")]
        ValidationError = 2,

        [Description("Exception")]
        Exception = 3
    }
}
