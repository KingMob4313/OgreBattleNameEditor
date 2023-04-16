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
        int startByte = Convert.ToInt32("0x00088d3c", 16);

        public List<NameRecord> processNameRecords(byte[] fileBytes)
        {
            int recordCount = getRecordCount(fileBytes, startByte);
            List<NameRecord> collectedNameRecords = new List<NameRecord>();
            int currentByteLocation = startByte;
            
            for (int i = 0; i < recordCount; i++)
            {
                
                int currentNameLength = (getNameLength(fileBytes, startByte, fileBytes.Length));
                NameRecord currentNameRecord = new NameRecord();
                currentNameRecord.NameByteStart = currentByteLocation;
                currentNameRecord.NameLength = currentNameLength;
                currentNameRecord.NameId = i;
                currentNameRecord.NameStartInHex = currentByteLocation.ToString("X");
                currentNameRecord.OldName = getNameFromBytes(fileBytes, currentByteLocation, currentNameRecord.NameLength);

                collectedNameRecords.Add(currentNameRecord);
                currentByteLocation = currentByteLocation + currentNameLength;
            }

            return collectedNameRecords;
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
            
            bool areRecordsDone = false;
            while (!areRecordsDone)
            {
                int nameLength = 0;
                nameLength = getNameLength(fileBytes, startByte, fileBytes.Length);
                startByte = startByte + nameLength;
                areRecordsDone = nameLength == 0;
                if (!areRecordsDone)
                {
                    recordCount++;
                }
            }
            return recordCount;
        }
        
        public int getNameLength(byte[] fileBytes, int intByteStartValue, int veryLastByte)
        {
            int byteLocationCounter = intByteStartValue;
            int counter = 0;
            while (fileBytes[byteLocationCounter] != 0)
            {
                byteLocationCounter++;
                counter++;
            }
            int lastByteLocation = (byteLocationCounter);
            return (lastByteLocation - intByteStartValue);
        }

    }
}
