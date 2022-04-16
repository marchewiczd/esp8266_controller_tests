using ESP8266_Controller_tests.SerialHandlerTests.TestBase;
using NUnit.Framework;
using System.Threading;

namespace ESP8266_Controller_tests.SerialHandlerTests
{
    [Order(999)]
    public class EspCommandTests : SerialHandlerTestBase
    {
        [Test, Order(999)]
        public void Esp_Restart()
        {
            int readDelayMs = 500;

            string testCommand = "esp restart";
            string[] expectedResponse =
            {
                "> esp restart\r",
                "Restarting...\r",
                "\r",
                " ets Jan  8 2013,rst cause:2, boot mode:(3,6)\r",
                "\r",
                "load 0x4010f000, len 3460, room 16 \r",
                "tail 4\r",
                "chksum 0xcc\r",
                "load 0x3fff20b8, len 40, room 4 \r",
                "tail 4\r",
                "chksum 0xc9\r",
                "csum 0xc9\r",
                string.Empty, //skip as it's different every time firmware changes
                "~ld\r"
            };

            WriteSerial(testCommand);

            Thread.Sleep(readDelayMs);
            var actualResponse = GetSerialResponses(expectedResponse.Length);

            Assert.AreEqual(expectedResponse.Length, actualResponse.Length, "Expected response and actual response lenghts are not equal");

            for (int i = 0; i < expectedResponse.Length; i++)
            {
                if (expectedResponse[i] == string.Empty)
                    continue;

                Assert.AreEqual(expectedResponse[i], actualResponse[i]);
            }
        }
    }
}