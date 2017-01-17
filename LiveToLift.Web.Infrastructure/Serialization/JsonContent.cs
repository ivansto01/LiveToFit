using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LiveToLift.Web.Infrastructure.Serialization
{
    public class JsonContent : HttpContent
    {
        private readonly MemoryStream Stream = new MemoryStream();

        public JsonContent(object value)
        {

            Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var jw = new JsonTextWriter(new StreamWriter(Stream));
            jw.Formatting = Formatting.Indented;
            var serializer = new JsonSerializer();
            serializer.Serialize(jw, value);
            jw.Flush();
            Stream.Position = 0;

        }
        protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            return Stream.CopyToAsync(stream);
        }

        protected override bool TryComputeLength(out long length)
        {
            length = Stream.Length;
            return true;
        }
    }
}
