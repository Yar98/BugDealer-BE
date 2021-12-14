using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Entities.Model
{
    public class Timezone : IEntityBase
    {
        public string GmtOffset { get; private set; }
        public string CountryCode { get; private set; }
        public string CountryName { get; private set; }
        private Timezone() { }
        public Timezone
            (string gmtOffset,
            string countryCode,
            string countryName)
        {
            GmtOffset = gmtOffset;
            CountryCode = countryCode;
            CountryName = countryName;
        }
    }
}
