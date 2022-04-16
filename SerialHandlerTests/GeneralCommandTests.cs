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
            string[] expectedResponse = { "> help\r", "Available commands:\r", "\tesp restart - restart ESP\r", "", "\thelp - print this message\r" };

            TestCommand(testCommand, expectedResponse, expectedResponse.Length);
        }

        [Test]
        public void UnrecognizedCommand()
        {
            string testCommand = "commandNotFound";
            string[] expectedResponse = { $"Command \"{testCommand}\" is not recognized. Type help for available commands.\r", $"> {testCommand}\r" };
    
                TestCommand(testCommand, expectedResponse, expectedResponse.Length);
        }
    }
}