using FluentAssertions;
using Kwetter.Library.Messaging.Datatypes;
using Messaging.RabbitMQ;
using NUnit.Framework.Internal;

namespace MessagingTests;

public class Tests
{
    PublishDataBuilder builder;
    [SetUp]
    public void Setup()
    {
        builder = new PublishDataBuilder();
    }

    [Test]
    public void Builder_Fails_WhenNoBodySet()
    {
        //Arrange
        //No arrange needed
        
        //Act
        Func<IPublishData> result = () => builder.setCustomRoutingKey("hi").build();

        // Assert
        result.Should().Throw<InvalidOperationException>();
        
    }
    [Test]
    public void Builder_Fails_WhenNoKeySet()
    {
        //Arrange
        MessageContainer<string> body = new MessageContainer<string>("hi");
        //No arrange needed
        
        //Act
        Func<IPublishData> result = () => builder.setBody(body).build();

        // Assert
        result.Should().Throw<InvalidOperationException>();
        
    }
    [Test]
    public void Builder_Creates_IPublishData_WhenSettingKeyAndBody()
    {
        //Arrange
        MessageContainer<string> body = new MessageContainer<string>("hi");
       
        
        //Act
        IPublishData result = builder.setBody(body).setCustomRoutingKey("test").build();

        // Assert
        result.data.Should().NotBeNull().And.BeOfType<MessageContainer<string>>();
        result.RoutingKey.Should().NotBeEmpty();
        result.Properties.Should().BeNull();
        result.QueueOptions.Should().BeNull();
        result.CorreletionID.Should().NotBeEmpty();
        

    }
}