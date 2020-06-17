using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPass.XPInterfaces
{
        public interface IImageConverter
        {

                byte[] GetBytes(string path);
                Task<byte[]> GetBytesAsync(string path);

                int GetRowLength();
                Task<string> GetBase64(string path);
                int CalculateChecksum(byte[] data);
        }
}



