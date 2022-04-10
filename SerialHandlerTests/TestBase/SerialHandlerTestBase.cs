using ESP8266_Controller_tests.Helpers;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace ESP8266_Controller_tests.SerialHandlerTests.TestBase
{
    public abstract class SerialHandlerTestBase
    {
        private int _testReadDelayMs = 20;

        private SerialHelper _serialHelper;

        [OneTimeSetUp]
        public void SerialSetup()
        {
            _serialHelper = new SerialHelper();
        }

        [OneTimeTearDown]
        public void SerialTeardown()
        {
            _serialHelper.Dispose();
        }

        protected string GetSerialResponse()
        {
            return _serialHelper.GetSerialResponse();
        }

        protected string[] GetSerialResponses(int count)
        {
            return _serialHelper.GetSerialResponses(count);
        }

        protected void WriteSerial(string text)
        {
            _serialHelper.WriteSerial(text);
        }

        protected void TestCommand(string testCommand, string[] expectedResponse, int responseCount, int timeoutMsOverride = int.MinValue)
        {
            WriteSerial(testCommand);

            Thread.Sleep(timeoutMsOverride == int.MinValue ? _testReadDelayMs : timeoutMsOverride);
            var actualResponse = GetSerialResponses(responseCount);

            CollectionAssert.AreEquivalent(expectedResponse, actualResponse);
        }
    }
}
