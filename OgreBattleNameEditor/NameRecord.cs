using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgreBattleNameEditor
{
    class NameRecord
    {
        private int nameByteStart;
        private int nameLength;

        private int nameId;
        private string nameStartInHex;

        private string oldName;
        private string newName;

        public int NameByteStart { get => nameByteStart; set => nameByteStart = value; }
        public int NameLength { get => nameLength; set => nameLength = value; }
        public string OldName { get => oldName; set => oldName = value; }
        public string NewName { get => newName; set => newName = value; }
        public int NameId { get => nameId; set => nameId = value; }
        public string NameStartInHex { get => nameStartInHex; set => nameStartInHex = value; }
    }
}
