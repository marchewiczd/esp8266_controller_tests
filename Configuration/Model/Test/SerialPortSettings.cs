namespace ESP8266_Controller_tests.Configuration.Model.Test
{
    public class SerialPortSettings
    {
        public string PortName { get; set; } = "COM1";
        public int BaudRate { get; set; } = 9600;
        public int Parity { get; set; } = 0;
        public int DataBits { get; set; } = 8;
        public int StopBits { get; set; } = 1;
        public int Handshake { get; set; } = 0;
    }
}
