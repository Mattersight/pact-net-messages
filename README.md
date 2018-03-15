# PactNetMeessages
Pact.Net v3 implementation of message only for services that communicate via event streams and message queues.

This project started off as a port of [PactNet 1.3.2](https://github.com/pact-foundation/pact-net/tree/1.3.2).  Implementations for HTTP request and response are not supported.  PactNetMessagess is aiming to be only compliant with the messages portion of the [Pact Specification Version 3](https://github.com/pact-foundation/pact-specification/tree/version-3). 

Read more about Pact and the problems it solves at https://github.com/realestate-com-au/pact

Please feel free to contribute, we do accept pull requests.

## Usage
Below are some samples of usage.  

### Installing

Via Nuget Package Manager https://www.nuget.org/packages/PactNetMessages/
> Install-Package PactNetMessages

### Service Consumer

#### 1. Build your client
Which may look something like this.

```c#
some code here
```

#### 2. Describe and configure the pact as a service consumer
Create a new test case within your service consumer test project, using whatever test framework you like (in this case we used xUnit).  
This should only be instantiated once for the consumer you are testing.

```c#
some more code here
```

#### 3. Write your test
Create a new test case and implement it.


```c#
some more code here
```

#### 4. Run the test
Everything should be green


### Service Provider

#### 1. Create the Message
You can create the message using whatever message queue library you want.  Translate your message queue object to a Message object

#### 2. Tell the provider it needs to honour the pact
Create a new test case within your service provider test project, using whatever test framework you like (in this case we used xUnit).

```c#
some code here
```

#### 4. Run the test
Everything should be green


#### Related Tools

You might also find the following tool and library helpful:

* [Pact.Net V3 Broker Connect](https://github.com/Mattersight/pact-net-v3-broker-connect): lUtility to bridge pact v3 specifications with the pact broker v1.1 support
* 