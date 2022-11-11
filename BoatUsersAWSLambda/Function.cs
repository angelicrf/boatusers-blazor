using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using Newtonsoft.Json;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace BoatUsersAWSLambda;

public class Function
{

    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="input"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    private bool correct { get; set; } = false;
    private string speech { get; set; } = "";
    //AccountId 322658870421
    //Canonical User ID: cd90151aed8a9555f8b2564ea69aa896f8c4281e85ce7028882fbc94b2b240ff
    //Access Key ID AKIAUWH7XUSK6A6TCNVX
    //Secret Access Key 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
    //EC2 Access Key APKAUWH7XUSKXQXJBEEK
    //new access key AKIAUWH7XUSKW46R7HUD
    //new secret key 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
    //users name boatusers
    //link to console https://322658870421.signin.aws.amazon.com/console
    public SkillResponse FunctionHandler(SkillRequest input, ILambdaContext context)
    {
        ILambdaLogger log = context.Logger;
        log.LogLine($"Skill Request Object:" + JsonConvert.SerializeObject(input));

        //Session session = input.Session;
        //session.Attributes ??= new Dictionary<string, object>();

        Type requestType = input.GetRequestType();
        if (requestType == typeof(LaunchRequest))
        {
            string speech = "Welcome! Say your command";
            Reprompt rp = new Reprompt("Say turn on or off or help or cancel to start");
            return ResponseBuilder.Ask(speech, rp);
        }
        else if (input.GetRequestType() == typeof(SessionEndedRequest))
        {
            return ResponseBuilder.Tell("Goodbye!");
        }
        else if (input.GetRequestType() == typeof(IntentRequest))
        {
            var intentRequest = (IntentRequest)input.Request;
            correct = false;
            speech = "";
            Int32 userInt = 0;

            switch (intentRequest.Intent.Name)
            {
                case " AMAZON.FallbackIntent":
                    return ResponseBuilder.Tell("Hi Something went wrong.");
                //case "AMAZON.StopIntent":
                case "ShellySwitchOff":
                    return ResponseBuilder.Tell("Device is Tutned off now.");

                case "ShellySwitchOn":
                    {

                        return ResponseBuilder.Tell("Device is Tutned on now.");
                    }
                case "AMAZON.HelpIntent":
                case "ShellyAskHelp":
                    {
                        // session.Attributes["num_entered"] = 0;
                        string userString = intentRequest.Intent.Slots["myNumber"].Value;
                        //userInt = (Int32)(long)session.Attributes["num_guesses"];
                        Int32.TryParse(userString, out userInt);
                        //correct = (userInt == (Int32)(long)session.Attributes["num_entered"]);
                        string next = "what is next";

                        Reprompt rp = new Reprompt(next);
                        return ResponseBuilder.Ask("Do you need help?", rp);
                    }
                case "AMAZON.CancelIntent":
                case "ShellySwitchCancel":
                    return ResponseBuilder.Tell("Process is canceled.Goodbye!");
                case "ShellySwichAnswer":
                    {
                        if (correct)
                        {
                            speech = "Correct! You entered number " + userInt.ToString() + " and matched up number is " + correct.ToString();
                            //session.Attributes["num_entered"] = 0;
                        }
                        else
                        {
                            speech = "Nope, incorrect number";
                            //session.Attributes["num_entered"] = userInt;
                        }
                        Reprompt rp = new Reprompt("enter a number");
                        return ResponseBuilder.Ask(speech, rp);
                    }
                default:
                    {
                        log.LogLine($"Unknown intent: " + intentRequest.Intent.Name);
                        speech = "I didn't understand - try again later!";
                        Reprompt rp = new Reprompt(speech);
                        return ResponseBuilder.Ask(speech, rp);
                    }
            }
        }
        return ResponseBuilder.Tell("Goodbye!");
    }
}

