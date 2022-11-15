using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using Newtonsoft.Json;


[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace BoatUsersAWSLambda;

public class Function
{

    /// <summary>
    /// A simple function to propmpt user to enter Alexa command and return a value
    /// </summary>
    /// <param name="input"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public Function() { }
    private bool correct { get; set; } = false;
    private string speech { get; set; } = "";
    private bool KeepLooping { get; set; } = true;
    //AccountId 322658870421 - or 315253745047
    //Canonical User ID: cd90151aed8a9555f8b2564ea69aa896f8c4281e85ce7028882fbc94b2b240ff
    //Access Key ID AKIAUWH7XUSK6A6TCNVX
    //Secret Access Key 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
    //EC2 Access Key APKAUWH7XUSKXQXJBEEK
    //new access key AKIAUWH7XUSKW46R7HUD
    //new secret key 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
    //users name boatusers
    //link to console https://322658870421.signin.aws.amazon.com/console
    //arn:aws:lambda:us-east-1:322658870421:function:BoatUsersLmbdFunc


    public SkillResponse FunctionHandler(SkillRequest skillRequest, ILambdaContext context)
    {

        ILambdaLogger log = context.Logger;

        //var newSkillRequest = new SkillRequest()
        //{

        //    Version = skillRequest.Version,
        //    Session =
        //    new Session
        //    {
        //        Attributes = parameters,
        //        SessionId = "amzn1.echo-api.session.5933eb2e-a97b-4dce-9420-d3bd646dd7a9",
        //        User = new User { UserId = "AEFDCFQZBIOPYSTDU6PFCOCFBLMZCAWC3FZCNMI7BXAFFLNY3VILR5M52EXO5ITPFWSRIGBIJLQPR6GISWXZOE5YNVBF5O2WUCDPVDGOCM3UKVV7HJMM4QKNP3HYCG4TRQ23LGLWDVKMJSTRMODYM6WRMD5VDUBKARRF2NALJUGJKIBLP2QRRI3AKEEICFYN56NZO465RZMARKY" },
        //        Application = new Application { ApplicationId = "amzn1.ask.skill.2294d305-508a-40b8-8f3f-b9db00d9de28" }
        //    }
        //};
        //skillRequest = newSkillRequest;

        log.LogLine($"Skill Request Object:" + JsonConvert.SerializeObject(skillRequest.Session.SessionId));

        Session session = skillRequest.Session;
        session.Attributes ??= new Dictionary<string, object>();

        Type requestType = skillRequest.GetRequestType();

        log.LogLine(requestType.ToString());

        if (requestType == typeof(LaunchRequest))
        {

            string speech = "Welcome! Say your command";
            Reprompt rp = new Reprompt("Say turn on or off or help or cancel to start");
            return ResponseBuilder.Ask(speech, rp);
        }
        else if (skillRequest.GetRequestType() == typeof(SessionEndedRequest))
        {
            return ResponseBuilder.Tell("Goodbye!");
        }
        else if (skillRequest.GetRequestType() == typeof(IntentRequest))
        {
            var intentRequest = (IntentRequest)skillRequest.Request;
            correct = false;
            speech = "";
            Int32 userInt = 0;

            while (KeepLooping)
                switch (intentRequest.Intent.Name)
                {
                    case " AMAZON.FallbackIntent":
                        {
                            KeepLooping = false;
                            return ResponseBuilder.Tell("Hi Something went wrong.");
                        }
                    case "AMAZON.StopIntent":
                    case "ShellySwitchOff":
                        {
                            KeepLooping = false;
                            return ResponseBuilder.Tell("Device is Turned off now.");
                        }

                    case "ShellySwitchOn":
                        {
                            KeepLooping = false;
                            return ResponseBuilder.Tell("Device is Turned on now.");
                        }

                    // case "AMAZON.HelpIntent":
                    case "ShellyAskHelp":
                        {
                            session.Attributes["num_entered"] = 0;
                            string userString = intentRequest.Intent.Slots["myNumber"].Value;
                            log.LogLine(userString);
                            userInt = Int32.Parse(userString);
                            Int32.TryParse(userString, out userInt);
                            correct = (userInt == (Int32)session.Attributes["num_entered"]);
                            //string next = "what is next";

                            //Reprompt rp = new Reprompt(next);
                            //return ResponseBuilder.Ask("Do you need help?", rp);
                            if (correct)
                            {
                                intentRequest.Intent.Name = "ShellySwitchAnswer";
                                KeepLooping = true;
                            }
                            else
                            {
                                KeepLooping = false;
                            }
                        }
                        break;
                    case "AMAZON.CancelIntent":
                    case "ShellySwitchCancel":
                        {
                            KeepLooping = false;
                            return ResponseBuilder.Tell("Process is canceled.Goodbye!");
                        }
                    case "ShellySwitchAnswer":
                        {
                            KeepLooping = false;
                            if (correct)
                            {
                                speech = "Correct! You entered number " + userInt.ToString() + " and matched up number is " + correct.ToString();
                                session.Attributes["num_entered"] = 0;
                            }
                            else
                            {
                                speech = "incorrect number";
                                session.Attributes["num_entered"] = userInt;
                                return ResponseBuilder.Tell($"You entered an {speech}");
                            }
                            Reprompt rp = new Reprompt("enter a number");
                            return ResponseBuilder.Ask(speech, rp);
                        }
                    default:
                        {
                            KeepLooping = false;
                            log.LogLine($"Unknown intent: " + intentRequest.Intent.Name);
                            speech = "I didn't understand - try again later!";
                            Reprompt rp = new Reprompt(speech);
                            return ResponseBuilder.Ask(speech, rp);
                        }
                }
        }
        Reprompt rp2 = new Reprompt("GoodBye!");
        return ResponseBuilder.Ask("Goodbye!", rp2);
    }
}

