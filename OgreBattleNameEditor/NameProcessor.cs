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
        int startByte = Convert.ToInt32("00088D3C", 16);
        int endByte = Convert.ToInt32("0008BA30", 16);

        public List<NameRecord> processNameRecords(byte[] fileBytes)
        {
            int recordCount = getRecordCount(fileBytes, startByte);
            List<NameRecord> collectedNameRecords = new List<NameRecord>();
            int currentByteLocation = startByte;
            
            for (int i = 0; i < recordCount; i++)
            {
                
                int currentNameLength = (getNameLength(fileBytes, currentByteLocation, fileBytes.Length));
                NameRecord currentNameRecord = new NameRecord();
                currentNameRecord.NameByteStart = currentByteLocation;
                currentNameRecord.NameLength = currentNameLength;
                currentNameRecord.NameId = i;
                currentNameRecord.NameStartInHex = currentByteLocation.ToString("X");
                currentNameRecord.OldName = getNameFromBytes(fileBytes, currentByteLocation, currentNameRecord.NameLength);

                collectedNameRecords.Add(currentNameRecord);
                currentByteLocation = currentByteLocation + currentNameLength + 1;
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

        public int getRecordCount(byte[] fileBytes, int byteLocationStart)
        {
            int recordCount = 0;
            int currentStartByte = byteLocationStart;
            
            bool areRecordsDone = false;
            while (!areRecordsDone)
            {
                int nameLength = 0;
                nameLength = getNameLength(fileBytes, currentStartByte, fileBytes.Length);
                currentStartByte = currentStartByte + nameLength + 1;
                areRecordsDone = currentStartByte >= endByte;
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
