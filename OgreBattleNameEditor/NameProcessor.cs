using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgreBattleNameEditor
{
    class NameProcessor
    {
        //Will name this smarter later
        string prefixedHex = "0x00088d3c";

        public List<NameRecord> processByteRecords(byte[] fileBytes)
        {
            int nameCount = 0;
            nameCount = getRecordCount(fileBytes);
        }

        public List<NameRecord> processNameRecords(byte[] fileBytes)
        {

        }

        //Working quickly should be a smarter way that won't involve reading the file multiple times
        public int getRecordCount(byte[] fileBytes)
        {
            int startByte = Convert.ToInt32(prefixedHex, 16);
            var endByte = startByte + readEndByte(fileBytes, startByte);



        }
        
        public int readEndByte(byte[] fileBytes, int intByteStartValue)
        {
            int byteLocationCounter = intByteStartValue;
            while (0 != fileBytes[intByteStartValue])
            {
                byteLocationCounter++;
            }
            return (byteLocationCounter - 1);
        }

    }
}
