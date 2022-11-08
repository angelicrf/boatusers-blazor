using Alexa.NET.Request;
using Amazon.Lambda.TestUtilities;
using Xunit;

namespace BoatUsersAWSLambda.Tests;

public class FunctionTest
{
    [Fact]
    public void TestToUpperFunction()
    {

        // Invoke the lambda function and confirm the string was upper cased.
        var function = new Function();
        var context = new TestLambdaContext();
        var thisRequest = new SkillRequest();
        var requestPrompt = function.FunctionHandler(thisRequest, context);

        Assert.NotNull(requestPrompt);
    }
}
