using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace WindowMediaPlayer.Model
{
    [JsonObject(MemberSerialization.OptIn)]
    public class MediaSingleElement
    {
        [JsonProperty]
        public string Title { get; set; }

        public ImageSource IconImage { get; set; }

        [JsonProperty]
        public string mediaUri { get; set; }

        [JsonProperty]
        public string MediaDuration { get; set; }

        [JsonProperty]
        public string Extension { get; set; }
    }
}
