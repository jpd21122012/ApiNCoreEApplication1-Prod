using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ApiNCoreEApplication1.Api.Helpers
{
    [DataContract]
    public class MessageHelpers<T>
    {
        [DataMember(Name = "Status")]
        public int Status { get; set; }

        [DataMember(Name = "Data")]
        public List<T> Data { get; set; }
    }
}
