# PactNetMessages
Pact.Net v3 implementation of message only for services that communicate via event streams and message queues.

This project started off as a port of [PactNet 1.3.2](https://github.com/pact-foundation/pact-net/tree/1.3.2).  Implementations for HTTP request and response are not supported.  PactNetMessages is aiming to be only compliant with the messages portion of the [Pact Specification Version 3](https://github.com/pact-foundation/pact-specification/tree/version-3). 

Read more about Pact and the problems it solves at https://github.com/realestate-com-au/pact

Please feel free to contribute, we do accept pull requests.

## Usage
Below are some samples of usage.  

### Installing

Via Nuget Package Manager https://www.nuget.org/packages/PactNetMessages/
> Install-Package PactNetMessages

### Service Consumer

#### 1. Build your message consumer
It should have some sort of message handler.  Which may look something like this.

```c#
    public class BusinessLogic
    {
        public Message BuildMessage(string json)
        {
            var newMessage = (Message) JsonConvert.DeserializeObject<Message>(json);
            return newMessage;
        }
    }
    
    public class Message
    {
        public DateTime end { get; set; }
        public DateTime start { get; set; }
        public string request { get; set; }

        [JsonIgnore]
        public string receiverOnlyInfo { get; set; }
   
        public Message()
        {
            start = new DateTime();
            end = new DateTime();
            request = "";
            receiverOnlyInfo = "";
        }
    }
```

#### 2. Describe and configure the pact as a service consumer
Create a new test case within your service consumer test project, using whatever test framework you like (in this case we used NUnit).  
This should only be instantiated once for the consumer you are testing.

```c#
[TestFixture]
public class Tests
{
    public IPactMessageBuilder PactMessageBuilder { get; set; }

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        PactMessageBuilder = new PactMessageBuilder()
            .ServiceConsumer("Receive")
            .HasPactWith("Send");
    }

    [OneTimeTearDown]
    public void Cleanup()
    {
        PactMessageBuilder.Build();
    }
}
```

#### 3. Write your test
Create a new test case and implement it.


```c#
[Test]
public void ReceiveTest()
{
    string json = "{\"end\":\"2018-01-01T00:00:00\",\"start\":\"2017-12-01T00:00:00\",\"request\":\"Hello World!\",\"senderOnlyInfo\":\"I want something in here to be private to the sender\"}";
    var message = new Receive.BusinessLogic().BuildMessage(json);

    PactMessageBuilder.MockMq().Given("Send Hello World")
        .UponReceiving("Hello World message")
        .WithMetaData(new {routingKey = "hello"})
        .WithContent(message);

    Assert.AreEqual("Hello World!", message.request, "Check the request.");
}
```

#### 4. Run the test
Everything should be green


### Service Provider

#### 1. Create the Message
You can create the message using whatever message queue library you want.  Translate your message queue object to a Message object

```c#
private PactNetMessages.Mocks.MockMq.Models.Message SetupState()
{
    var message = new Send.BusinessLogic().GetMessage();
    var pactNetMessage = new PactNetMessages.Mocks.MockMq.Models.Message()
    {
        MetaData = new {routingKey = "hello"},
        Contents = message
    };

    return pactNetMessage;
}
```

#### 2. Tell the provider it needs to honour the pact
Create a new test case within your service provider test project, using whatever test framework you like (in this case we used NUnit).

* Note: PactUri() Only supports local file paths currently since the Pact Broker does not handle v3 specifications yet.

```c#
[Test]
public void PactTest()
{
    var config = new PactVerifierConfig();
    IPactVerifier pactVerifier = new PactVerifier(() => { }, () => { }, config);
    pactVerifier
        .ProviderState("Send Hello World", setUp: SetupState);

    pactVerifier
        .MessageProvider("Send")
        .HonoursPactWith("Receive")
        .PactUri("/pacts/receive-send.json")
        .Verify();
}
```

#### 4. Run the test
Everything should be green


#### Related Tools

You might also find the following tool and library helpful:

* [Pact.Net V3 Broker Connect](https://github.com/Mattersight/pact-net-v3-broker-connect): lUtility to bridge pact v3 specifications with the pact broker v1.1 support