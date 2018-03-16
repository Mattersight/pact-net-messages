using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PactNetMessages.Tests
{
    public class PactMessageBuilderTests
    {
        [Fact]
        public void PactMessageBuilder_ReturnsDefaultConfig()
        {
            var defaultConfig = new PactConfig();
            var builder = new PactMessageBuilder();
            
            Assert.Equal(defaultConfig.PactDir, builder.pactConfig.PactDir);
        }

        [Fact]
        public void PactMEssageBuilder_OverrideConfig()
        {
            var customConfig = new PactConfig();
            customConfig.PactDir = "/customPactDir";

            var builder = new PactMessageBuilder(customConfig);

            Assert.Equal("/customPactDir", builder.pactConfig.PactDir);

        }
    }
}
