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
    //Access Key deleted ID AKIAUWH7XUSK6A6TCNVX
    //Secret Access Key 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
    //EC2 Access Key APKAUWH7XUSKXQXJBEEK
    //new access key AKIAUWH7XUSKW46R7HUD
    //new secret key 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
    //users name boatusers
    //link to console https://322658870421.signin.aws.amazon.com/console
    //arn:aws:lambda:us-east-1:322658870421:function:BoatUsersLmbdFunc
    //clientid: amzn1.application-oa2-client.66d1acd7d0514467837c784ded292ac7
    // new clientId:  amzn1.application-oa2-client.8e364cf34cb649508a1746e26a4429d4
    // new client secret: 55e478a258cc7e74ad623dd3a5439e501dfad27c8ef710daa7f73b391c98a899
    //aud: amzn1.application-oa2-client.aad322b5faab44b980c8f87f94fbac56
    //app id: amzn1.application.915369ebbc1242368b3179a9cf03c32a
    //secret: 9f5cf16af1470f002710f17edeb6ec88097bbd9bfc4a382b46df8b48fd490971
    //userId; amzn1.ask.account.AEFDCFQZBIOPYSTDU6PFCOCFBLMZCAWC3FZCNMI7BXAFFLNY3VILR5M52EXO5ITPFWSRIGBIJLQPR6GISWXZOE5YNVBF5O2WUCDPVDGOCM3UKVV7HJMM4QKNP3HYCG4TRQ23LGLWDVKMJSTRMODYM6WRMD5VDUBKARRF2NALJUGJKIBLP2QRRI3AKEEICFYN56NZO465RZMARKY
    //user_id: amzn1.account.AH4K27WNOYNFE2BEQULAVLAQPIYQ
    //vendor id: M2FC9FL6SREJQF
    // url to post change State: "https://api.amazonalexa.com/v3/events"
    //https://github.com/angelicrf/alexa-boatusers.git
    public SkillResponse FunctionHandler(SkillRequest skillRequest, ILambdaContext context)
    {

        ILambdaLogger log = context.Logger;

        //        var newSkillRequest = new SkillRequest()
        //        {

        //            Version = "1.0",
        //            Session =
        //            new Session
        //            {
        //                Attributes = new Dictionary<string, object>(),
        //                SessionId = "amzn1.echo-api.session.04d7ced7-8e65-42e5-a385-6e9bebdfa8fa",
        //                User = new User { UserId = "AEFDCFQZBIOPYSTDU6PFCOCFBLMZCAWC3FZCNMI7BXAFFLNY3VILR5M52EXO5ITPFWSRIGBIJLQPR6GISWXZOE5YNVBF5O2WUCDPVDGOCM3UKVV7HJMM4QKNP3HYCG4TRQ23LGLWDVKMJSTRMODYM6WRMD5VDUBKARRF2NALJUGJKIBLP2QRRI3AKEEICFYN56NZO465RZMARKY" },
        //                Application = new Application { ApplicationId = "amzn1.ask.skill.2294d305-508a-40b8-8f3f-b9db00d9de28" }
        //            },
        //            Request = {
        //                   Type = "LaunchRequest",
        //                   RequestId = "amzn1.echo-api.request.9e7a830f-5eb1-4e60-b954-57c11fc68a91",
        //                   Locale = "en-US",
        //                   Timestamp = DateTime.Today
        //            "shouldLinkResultBeReturned": false
        //                      }
        //};
        // skillRequest = newSkillRequest;
        //eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImtpZCI6IjEifQ.eyJhdWQiOiJodHRwczovL2FwaS5hbWF6b25hbGV4YS5jb20iLCJpc3MiOiJBbGV4YVNraWxsS2l0Iiwic3ViIjoiYW16bjEuYXNrLnNraWxsLjIyOTRkMzA1LTUwOGEtNDBiOC04ZjNmLWI5ZGIwMGQ5ZGUyOCIsImV4cCI6MTY2ODYxMDYzOSwiaWF0IjoxNjY4NjEwMzM5LCJuYmYiOjE2Njg2MTAzMzksInByaXZhdGVDbGFpbXMiOnsiY29udGV4dCI6IkFBQUFBQUFBQUFCbjlzdWZsSEhXVUQ2Q0FrZ3BOOHZsS3dFQUFBQUFBQUJQZ0Jxb1YwMEZZcXRYemYySUUrWG1kSVlWVWEzZlJmTy9qT0VCVWM3ZmxLaVVFSk8yeVVMQWs5Y0NwQko3R3JmK2hvc0ZxRFhma2lrVkVMTjUxR1ZjelNMSlRocE0yV1NRWHMvMEc4a25PQ3dIdDJKbWcxMXU5amxIb2pQTVBWWU5UaXUxUWQwRFhsbG5LUjFjTHZtdHg4RnRtSkpKRkVvWmxmRG1VQURpUlVsV3V6SUR2c1I3VGVxR2tjNXZ6M2F6K2RnSEVuZStrV0NuUzFlamNKS3BUNmtSRVUvUmNQaFJRcGZ1VTNJc3AyTm1WN08xaXRuc0NFLy82L01tWmZqZkVIMW8zc3RQdVJmOTFEZkwwT3VheWF0N2pKV0dTdWdLcm40VDBwNXc2Q2dJVDNNMFF5NWdmcEpaYnVobW9BLzhMZENscEVHeHd6R3puTmY5UjJtclJZK2tGRXZ6cU9uY2hpV1lVY0lhaUc5SmhianVIU1ZFTEtxZ1l6dU1yKzZ1blRvRUtxZ2xtOS9rSVE9PSIsImRldmljZUlkIjoiYW16bjEuYXNrLmRldmljZS5BR05QNFdJWEpBVU40SVpKNTVHUFlWSkdTVjNHMlpIWU5BSUlHMlI0U0o1VVZZRFVSSklGUllaVklOT0RONVFLNzY3VElEN0tLTEJHWEpER1dKN1dPUjJSN1pHTk83NjZZVDVVTEVSNjdIUjdBWVZZNU81QVJVVVdWUlNZT1RQQkRIQVUzN1JORUdHTU8zM0tSUTNESVZUNUMzVFEyTVJPNUY1TkpJWU5HREdGWkRPTUlRS0FZIiwidXNlcklkIjoiYW16bjEuYXNrLmFjY291bnQuQUVGRENGUVpCSU9QWVNURFU2UEZDT0NGQkxNWkNBV0MzRlpDTk1JN0JYQUZGTE5ZM1ZJTFI1TTUyRVhPNUlUUEZXU1JJR0JJSkxRUFI2R0lTV1haT0U1WU5WQkY1TzJXVUNEUFZER09DTTNVS1ZWN0hKTU00UUtOUDNIWUNHNFRSUTIzTEdMV0RWS01KU1RSTU9EWU02V1JNRDVWRFVCS0FSUkYyTkFMSlVHSktJQkxQMlFSUkkzQUtFRUlDRllONTZOWk80NjVSWk1BUktZIn19.L2lsiSQoeA9Dwf0arMiTBAuSHCS3d5lEIYVa_XQyFe8fSQWd1-J4mC4dIcQBmcjkVTbFdBE8r_qidHFFJLZf0TTJvFq2GrjGWaNmr75xjvhEoj6MdihCKbnEbpz8k1DMkOKv3AnVoMDN6RfannGGw-O1oieEWHHQ68c1gGVjJxuEM2Q42sOZb6MM7TcAQd9oUqrR4XLGFMuMxDv282ONZz4YYWBlCUZ4DbDHrc0l9ZkYT2y8Rk2bW6A-jVAuhIdWxRoTY8FpWva_yArS-4EhN95j6u4O6SVVkSULbfnuqaCDb5Zv6IOnQZozQiiB3JPRWwqFDoLvey6Eu767-mUElQ
        //amzn1.ask.account.AEFDCFQZBIOPYSTDU6PFCOCFBLMZCAWC3FZCNMI7BXAFFLNY3VILR5M52EXO5ITPFWSRIGBIJLQPR6GISWXZOE5YNVBF5O2WUCDPVDGOCM3UKVV7HJMM4QKNP3HYCG4TRQ23LGLWDVKMJSTRMODYM6WRMD5VDUBKARRF2NALJUGJKIBLP2QRRI3AKEEICFYN56NZO465RZMARKY
        log.LogLine($"Skill Request Object:" + JsonConvert.SerializeObject(skillRequest));

        Session session = skillRequest.Session;
        session.Attributes ??= new Dictionary<string, object>();

        Type requestType = skillRequest.GetRequestType();

        log.LogLine(requestType.ToString());

        if (requestType == typeof(LaunchRequest))
        {

            string speech = "Welcome! Say your command";
            Reprompt rp = new Reprompt("Say turn on or off or help or cancel to start");
            return ResponseBuilder.Ask(speech, rp, session);
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
                            return ResponseBuilder.Ask(speech, rp, session);
                        }
                    default:
                        {
                            KeepLooping = false;
                            log.LogLine($"Unknown intent: " + intentRequest.Intent.Name);
                            speech = "I didn't understand - try again later!";
                            Reprompt rp = new Reprompt(speech);
                            return ResponseBuilder.Ask(speech, rp, session);
                        }
                }
        }
        Reprompt rp2 = new Reprompt("GoodBye!");
        return ResponseBuilder.Ask("Goodbye!", rp2, session);
    }
}

