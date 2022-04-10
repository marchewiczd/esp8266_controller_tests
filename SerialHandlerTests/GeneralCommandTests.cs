using ESP8266_Controller_tests.SerialHandlerTests.TestBase;
using NUnit.Framework;
using System.Threading;

namespace ESP8266_Controller_tests.SerialHandlerTests
{
    [Order(0)]
    public class GeneralCommandTests : SerialHandlerTestBase
    { 
        [Test]
        public void Help_PrintsAllCommands()
        {
            string testCommand = "help";
            string[] expectedResponse = { "\trestart - restarts ESP\r", "ESP\r", "> help\r" };

            TestCommand(testCommand, expectedResponse, 3);
        }

        [Test]
        public void UnrecognizedCommand()
        {
            string testCommand = "commandNotFound";
            string[] expectedResponse = { $"Command \"{testCommand}\" is not recognized.\r", $"> {testCommand}\r" };
    
                TestCommand(testCommand, expectedResponse, 2);
        }
    }
}