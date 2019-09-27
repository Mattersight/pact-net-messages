using Newtonsoft.Json;
using PactNetMessages.Models;
using Xunit;


namespace PactNetMessages.Tests.Models
{
    public class PactFileTests
    {
        [Fact]
        public void Ctor_WhenInstantiated_SetsPactSpecificationVersionMetaDataTo3()
        {
            var pactFile = new PactFile();
            var expected = new { pactSpecification = new { version = "3.0.0" }, pactSpecificationVersion = "3.0" };

            Assert.Equal(JsonConvert.SerializeObject(expected), JsonConvert.SerializeObject(pactFile.Metadata));
        }
    }
}