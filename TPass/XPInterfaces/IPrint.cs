
using System.Threading.Tasks;
using TPass.Models;

namespace TPass.XPInterfaces {

	public interface IBluetoothDiscovery {

	}

	public interface IPrinter {

		Task<bool> PrintZPL(string data);
        string PrinterAddress { get; }
	}

	public interface INetworkCheck {

		bool IsOnline();

	}
}
