using System;
using System.IO;
using Newtonsoft.Json;
using PactNetMessages.Mocks.MockMq.Comparers;
using Xunit;


namespace PactNetMessages.Tests.Specifications
{
    public class MessageBodyTests
    {
        private readonly MessageComparer comparer;

        public MessageBodyTests()
        {
            comparer = new MessageComparer();
        }
        private void RunTestCase(string path)
        {
            var testFileJson = File.ReadAllText(path);
            var testCase = JsonConvert.DeserializeObject<TestCase>(testFileJson);
            var results = comparer.Compare(testCase.Expected, testCase.Actual);          

            //when Match is true, it represents a not having failures
            Assert.Equal(testCase.Match, !results.HasFailure);
            
        }

        [Fact]
        public void ArrayAtTopLevel_ShouldPass()
        {
            RunTestCase("../../Specifications/TestCases/Message/Body/array at top level.json");
        }

        [Fact]
        public void ArrayInDifferentOrder_ShouldFail()
        {
            RunTestCase("../../Specifications/TestCases/Message/Body/array in different order.json");
        }

        [Fact]
        public void ArraySizeLessThanRequired_ShouldFail()
        {
            RunTestCase("../../Specifications/TestCases/Message/Body/array size less than required.json");
        }

        internal void ArrayWithAtLeastOneElementMatchingByExample_ShouldPass()
        {
            throw new NotImplementedException();
        }

        internal void ArrayWithAtLeastOneElementNotMatchingByExampleType_ShouldFail()
        {
            throw new NotImplementedException();
        }

        internal void ArrayWithNestedArrayThatDoesNotMatch_ShouldFail()
        {
            throw new NotImplementedException();
        }

        internal void ArrayWithNestedArrayThatMatches_ShouldPass()
        {
            throw new NotImplementedException();
        }

        internal void ArrayWithRegularExpressionInElement_ShouldPass()
        {
            throw new NotImplementedException();
        }

        internal void ArrayWithRegularExpressionThatDoesNotMatchInElement_ShouldFail()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void DifferentValueFoundAtIndex_ShouldFail()
        { 
            RunTestCase("../../Specifications/TestCases/Message/Body/different value found at index.json");
        }

        [Fact]
        public void DifferentValueFoundAtKey_ShouldFail()
        {

            RunTestCase("../../Specifications/TestCases/Message/Body/different value found at key.json");
        }
        internal void MatchesWithRegexWithBracketNotation_ShouldPass()
        {
            throw new NotImplementedException();
        }

        internal void MatchesWithRegex_ShouldPass()
        {
            throw new NotImplementedException();
        }

        internal void MatchesWithType_ShouldPass()
        {
            throw new NotImplementedException();
        }

        internal void Matches_ShouldPass()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void MissingIndex_ShouldFail()
        {
            RunTestCase("../../Specifications/TestCases/Message/Body/missing index.json");
        }

        [Fact]
        public void MissingKey_ShouldFail()
        {
            RunTestCase("../../Specifications/TestCases/Message/Body/missing key.json");
        }

        [Fact]
        public void NoBodyNoContentType_ShouldPass()
        {
            RunTestCase("../../Specifications/TestCases/Message/Body/no body no content type.json");
        }

        [Fact]
        public void NoBody_ShouldPass()
        {
            RunTestCase("../../Specifications/TestCases/Message/Body/no body.json");
        }

        [Fact]
        public void NotNullFoundAtKeyWhenNullExpected_ShouldFail()
        {
            RunTestCase("../../Specifications/TestCases/Message/Body/not null found at key when null expected.json");
        }

        [Fact]
        public void NotNullFoundInArrayWhenNullExpected_ShouldFail()
        {
            RunTestCase("../../Specifications/TestCases/Message/Body/not null found in array when null expected.json");
        }

        [Fact]
        public void NullFoundAtKeyWhereNotNullExpected_ShouldFail()
        {
            RunTestCase("../../Specifications/TestCases/Message/Body/null found at key where not null expected.json");
        }

        [Fact]
        public void NullFoundInArrayWhenNotNullExpected_ShouldFail()
        {
            RunTestCase("../../Specifications/TestCases/Message/Body/null found in array when not null expected.json");
        }

        [Fact]
        public void NumberFoundAtKeyWhenStringExpected_ShouldFail()
        {
            RunTestCase("../../Specifications/TestCases/Message/Body/number found at key when string expected.json");
        }

        [Fact]
        public void NumberFoundInArrayWhenStringExpected_ShouldFail()
        {
            RunTestCase("../../Specifications/TestCases/Message/Body/number found in array when string expected.json");
        }

        [Fact]
        public void StringFoundAtKeyWhenNumberExpected_ShouldFail()
        {
            RunTestCase("../../Specifications/TestCases/Message/Body/string found at key when number expected.json");
        }

        [Fact]
        public void StringFoundInArrayWhenNumberExpected_ShouldFail()
        {
            RunTestCase("../../Specifications/TestCases/Message/Body/string found in array when number expected.json");
        }

        [Fact]
        public void UnexpectedIndexWithNotNullValue_ShouldFail()
        {
            RunTestCase("../../Specifications/TestCases/Message/Body/unexpected index with not null value.json");
        }

        [Fact]
        public void UnexpectedIndexWithNullValue_ShouldFail()
        {
            RunTestCase("../../Specifications/TestCases/Message/Body/unexpected index with null value.json");
        }

        [Fact]
        public void UnexpectedKeyWithNotNullValue_ShouldPass()
        {
            RunTestCase("../../Specifications/TestCases/Message/Body/unexpected key with not null value.json");
        }

        [Fact]
        public void UnexpectedKeyWithNullValue_ShouldPass()
        {
            RunTestCase("../../Specifications/TestCases/Message/Body/unexpected key with null value.json");
        }
    }
}
