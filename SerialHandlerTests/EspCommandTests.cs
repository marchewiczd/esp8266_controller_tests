using ESP8266_Controller_tests.SerialHandlerTests.TestBase;
using NUnit.Framework;
using System.Threading;

namespace ESP8266_Controller_tests.SerialHandlerTests
{
    [Order(999)]
    public class EspCommandTests : SerialHandlerTestBase
    {
        [Test, Order(1)]
        public void Esp_UnrecognizedCommandArgument()
        {
            string testCommand = "esp badArg";
            string[] expectedResponse = { $"Argument \"{testCommand.Split(" ")[1]}\" is not recognized for \"esp\" command.\r", $"> {testCommand}\r" };

            TestCommand(testCommand, expectedResponse, 2);
        }

        [Test, Order(1)]
        public void Esp_UnrecognizedCommandArgument_MultipleArguments()
        {
            string testCommand = "esp badArg1 badArg2 badArg3";
            string[] expectedResponse = { $"Argument \"{testCommand.Split(" ")[1]}\" is not recognized for \"esp\" command.\r", $"> {testCommand}\r" };

            TestCommand(testCommand, expectedResponse, 2);
        }

        [Test, Order(999)]
        public void Esp_Restart()
        {
            int readDelayMs = 500;
            string testCommand = "esp restart";
            string[] expectedResponse =
            {
                "~ld\r",
                "v0004f7c0\r",
                "csum 0xc9\r",
                "chksum 0xc9\r",
                "tail 4\r",
                "load 0x3fff20b8, len 40, room 4 \r",
                "chksum 0xcc\r",
                "tail 4\r",
                "load 0x4010f000, len 3460, room 16 \r",
                "\r",
                " ets Jan  8 2013,rst cause:2, boot mode:(3,6)\r",
                "\r",
                "Restarting...\r",
                "> esp restart\r"
            };

            TestCommand(testCommand, expectedResponse, 14, readDelayMs);
        }
    }
}