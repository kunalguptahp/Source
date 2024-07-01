using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ElementsCPS.Data.SubSonicClient
{
    public partial class VwMapJumpstationGroupCalcOnFlyController
    {
        public static VwMapJumpstationGroupCalcOnFly FetchValue(int id)
        {
            var coll = VwMapJumpstationGroupCalcOnFlyController.FetchByID(id);
            return (coll.Count == 0) ? null : coll[0];
        }
    }
}
