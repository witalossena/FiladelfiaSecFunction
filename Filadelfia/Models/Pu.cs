using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiladelfiaFunction.Filadelfia.Models
{
    public class Pu
    {
        [JsonProperty("cct_status")]
        public string CctStatus { get; set; }

        [JsonProperty("cct_author_id")]
        public string CctAuthorId { get; set; }

        [JsonProperty("cct_created")]
        public string CctCreated { get; set; }

        [JsonProperty("cct_modified")]
        public string CctModified { get; set; }

        [JsonProperty("jurosesperado")]
        public string JurosEsperado { get; set; }

        [JsonProperty("jurosextra")]
        public string JurosExtra { get; set; }

        [JsonProperty("jurospago")]
        public string JurosPago { get; set; }

        [JsonProperty("vnaj")]
        public string VNAJ { get; set; }

        [JsonProperty("seriesid")]
        public string SeriesId { get; set; }

        [JsonProperty("puresidual")]
        public string PUResidual { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("cct_slug")]
        public string CctSlug { get; set; }
    }

}

