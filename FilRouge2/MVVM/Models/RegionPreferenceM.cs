using BO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace FilRouge2
{
    class RegionPreferenceM
    {
        private const string KEY_REGIONPREFERENCE = "RegionPreference";

        private static volatile RegionPreferenceM instance;
        private static readonly object syncRoot = new Object();

        private RegionPreferenceM() { }

        public static RegionPreferenceM Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            if (String.IsNullOrEmpty(ApplicationData.Current.LocalSettings.Values[KEY_REGIONPREFERENCE] as String))
                            { instance = new RegionPreferenceM(); }
                            else
                            { instance = JsonConvert.DeserializeObject<RegionPreferenceM>(ApplicationData.Current.LocalSettings.Values[KEY_REGIONPREFERENCE] as string); }
                        }
                    }
                }
                return instance;
            }
        }

        private RegionFrancaise _preferedRegion;

        public RegionFrancaise PreferedRegion
        { 
            get { return _preferedRegion; }
            set { _preferedRegion = value; }
        }

        public void Save(RegionFrancaise region)
        {
            _preferedRegion = region;
            ApplicationData.Current.LocalSettings.Values[KEY_REGIONPREFERENCE] = JsonConvert.SerializeObject(this);
        }
    }
}
