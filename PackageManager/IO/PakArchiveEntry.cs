using System.IO;

namespace PackageManager.IO
{
    public class PakArchiveEntry
    {
        private readonly string _fullName;
        private readonly int _position;
        private readonly int _length;
        private readonly int _unknownInt;
        private readonly int _unknownInt2;

        private readonly PakArchive _archive;

        internal PakArchiveEntry(PakArchive archive, string fullName, int position, int length, int unknownInt, int unknownInt2)
        {
            _archive = archive;

            _fullName = fullName;
            _position = position;
            _length = length;
            _unknownInt = unknownInt;
            _unknownInt2 = unknownInt2;
        }

        public string FullName => _fullName;
        public int Length => _length;
        public int UnknownInt => _unknownInt;
        public int UnknownInt2 => _unknownInt2;

        public string Name => Path.GetFileName(_fullName);

        public void Delete()
        {

        }

        public Stream Open()
        {
            return new MemoryStream();
        }

        public override string ToString()
        {
            return FullName;
        }
    }
}
