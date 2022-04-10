using ESP8266_Controller_tests.Configuration;
using ESP8266_Controller_tests.Configuration.Model;
using ESP8266_Controller_tests.Configuration.Model.Test;
using NUnit.Framework;
using System;
using System.Collections.Concurrent;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;

namespace ESP8266_Controller_tests.Helpers
{
    public class SerialHelper : IDisposable
    {
        private ConcurrentQueue<string> _serialResponse = new ConcurrentQueue<string>();
        private bool _readSerial = true;
        private SerialPort _serialPort;
        private Task _readSerialTask;
        private SerialPortSettings _serialPortConfig;

        public SerialHelper()
        {
            _serialPortConfig  = ConfigAggregator.Get<SerialPortSettings>();
            CreateSerialPort();

            _serialPort.Open();
            _readSerialTask = Task.Run(() => ReadSerial());
        }

        public void Dispose()
        {
            _readSerial = false;
            _readSerialTask.Wait(5000);
            _serialPort.Close();
        }
        public string GetSerialResponse()
        {
            string response;

            if (_serialResponse.TryDequeue(out response))
            {
                return response;
            }

            return string.Empty;
        }

        public string[] GetSerialResponses(int count)
        {
            string[] response = new string[count];
            for (int i = 0; i < count; i++)
            {
                response[i] = GetSerialResponse();
            }

            return response;
        }

        public void WriteSerial(string text)
        {
            _serialPort.Write(text);
        }

        private void CreateSerialPort()
        {
            _serialPort = new SerialPort();
            _serialPort.PortName = _serialPortConfig .PortName;
            _serialPort.BaudRate = _serialPortConfig .BaudRate;
            _serialPort.Parity = (Parity)_serialPortConfig .Parity;
            _serialPort.DataBits = _serialPortConfig .DataBits;
            _serialPort.StopBits = (StopBits)_serialPortConfig .StopBits;
            _serialPort.Handshake = (Handshake)_serialPortConfig .Handshake;

            _serialPort.ReadTimeout = 500;
            _serialPort.WriteTimeout = 500;
        }

        private void ReadSerial()
        {
            while (_readSerial)
            {
                try
                {
                    string message = _serialPort.ReadLine();
                    _serialResponse.Enqueue(message);
                }
                catch (TimeoutException) { }
            }
        }
    }
}
