
using TPIS.Model.Common;

namespace TPIS.Model
{
    public class Property
    {
        private string v1;
        private string v2;
        private string nA;
        private P_Type toSetAsString;

        public Property(string v1, string v2, string nA, P_Type toSetAsString, string key)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.nA = nA;
            this.toSetAsString = toSetAsString;
        }
    }
}