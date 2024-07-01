using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ElementsCPS.Data.SubSonicClient
{
    partial class JumpstationGroupPivot
    {
        public void CopyFromCalcOnFly(VwMapJumpstationGroupCalcOnFly origial)
        {
            this.Brand = origial.BrandQueryParameterValue;
            this.Cycle = origial.CycleQueryParameterValue;
            this.Locale = origial.LocaleQueryParameterValue;
            this.Touchpoint = origial.TouchpointQueryParameterValue;
            this.PartnerCategory = origial.PartnerCategoryQueryParameterValue;
            this.Platform = origial.PlatformQueryParameterValue;
        }
    }
}
