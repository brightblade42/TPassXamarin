using Android.Bluetooth;
using System;
using System.Threading.Tasks;
using TPass.Droid;
using TPass.XPInterfaces;


[assembly: Xamarin.Forms.Dependency(typeof(PrintService))]
namespace TPass.Droid
{

    public class PrintService : IPrinter
    {

        public BluetoothDevice BlueDevice { get; set; } = null;

        public BluetoothSocket BlueSocket { get; private set; }
        string btAddress = "";
        public string PrinterAddress { get { return btAddress; } }

        public PrintService()
        {

        }


        public async Task<bool> PrintZPL(string data)
        {
            //need bluetooth shenanigans

            if (BlueSocket == null)
            {

                BlueSocket = InitBluetooth();

                if (BlueSocket == null)
                    return false;

                try
                {
                    if (!BlueSocket.IsConnected)
                        BlueSocket.Connect();
                }
                catch (Exception ex)
                {
                    string wtf = ex.Message;
                    return false;
                }

                if (!BlueSocket.IsConnected)
                {
                    return false;
                }


                try
                {

                    return await WriteToSocket(data);
                }
                catch (Exception ex)
                {
                    var wtf = ex.Message;
                    return false;
                }

            }
            else
            {

                try
                {
                    if (!BlueSocket.IsConnected)
                        BlueSocket.Connect();
                }
                catch (Exception ex)
                {
                    string wtf = ex.Message;
                    return false;
                }

                return await WriteToSocket(data);
            }



        }

        BluetoothAdapter btAdapter = null;

        async Task<bool> WriteToSocket(string data)
        {

            try
            {
                var bytes = System.Text.Encoding.Default.GetBytes(data); //baby needs some bytes
                await BlueSocket.OutputStream.WriteAsync(bytes, 0, bytes.Length);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

          
        }



        BluetoothSocket InitBluetooth()
        {
            btAdapter = BluetoothAdapter.DefaultAdapter;

            if (btAdapter == null)
            {

                var bada_boom = "big boom";
                return null;

            }

            var devs = btAdapter.BondedDevices;

            btAdapter.CancelDiscovery(); //for speed reasons

            foreach (var item in devs)
            {

                if (String.IsNullOrEmpty(btAddress))
                {
                    btAddress = item.Address;
                }


                BlueDevice = btAdapter.GetRemoteDevice(item.Address);
                var uid = BlueDevice.GetUuids();
            }


            if (BlueDevice != null)
            {
                var id = Java.Util.UUID.FromString("00001101-0000-1000-8000-00805F9B34FB");
                return BlueDevice.CreateRfcommSocketToServiceRecord(id);
            }
            else
            {
                return null;
            }
        }
    }
}