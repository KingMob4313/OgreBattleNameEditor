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
            List<NameRecord> currentNameRecords = new List<NameRecord>();

            int nameCount = 0;
            nameCount = getRecordCount(fileBytes);
            return currentNameRecords;
        }

        public List<NameRecord> processNameRecords(byte[] fileBytes)
        {
            int byteStart = ;
            int recordCount = getRecordCount(fileBytes, byteStart);
            List<NameRecord> collectedNameRecords = new List<NameRecord>();
            int byteLocation = byteStart;

            for (int i = 0; i < recordCount; i++)
            {
                NameRecord currentNameRecord = new NameRecord();
                currentNameRecord.NameByteStart = byteStart;
                currentNameRecord.NameLength = (readEndByte(fileBytes, byteStart)) - byteStart;
                currentNameRecord.NameId = i;
                currentNameRecord.NameStartInHex = byteStart.ToString("X");
                currentNameRecord.OldName = getNameFromBytes(fileBytes, byteStart, currentNameRecord.NameLength);
            }
        }

        private string getNameFromBytes(byte[] fileBytes, int byteStart, int nameLength)
        {
            string currentName = string.Empty;
            List<byte> nameByte = new List<byte>();
            for (int i = 0; i < nameLength; i++)
            {
                nameByte.Add(fileBytes[byteStart + i]);
            }
            currentName = System.Text.Encoding.Default.GetString(nameByte.ToArray());
            return currentName;
        }

        //Working quickly should be a smarter way that won't involve reading the file multiple times
        public int getRecordCount(byte[] fileBytes, int byteLocationStart)
        {
            int recordCount = 0;
            int startByte = byteLocationStart;
            int endByte = 0; 
            bool areRecordsDone = false;
            while (areRecordsDone)
            {
                endByte = startByte + readEndByte(fileBytes, startByte);
                startByte = startByte + endByte;
                areRecordsDone = startByte == endByte;
                if (!areRecordsDone)
                {
                    recordCount++;
                }
            }
            return recordCount;
        }
        
        public int readEndByte(byte[] fileBytes, int intByteStartValue)
        {
            int byteLocationCounter = intByteStartValue;
            while (fileBytes[intByteStartValue] != 0)
            {
                byteLocationCounter++;
            }
            return (byteLocationCounter - 1);
        }

    }
}
