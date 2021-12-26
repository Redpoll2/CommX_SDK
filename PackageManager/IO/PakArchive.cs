using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace PackageManager.IO
{
    public class PakArchive : IDisposable
    {
        private const int Signature = 0x50414B;  // In ASCII, the letters 'PAK'

        private readonly PakArchiveType _type;
        private readonly GameVersion _gameVersion;
        private readonly int _unknownInt;

        private readonly List<PakArchiveEntry> _entries;
        private readonly ReadOnlyCollection<PakArchiveEntry> _entriesCollection;

        public PakArchive(Stream stream)
        {
            if (stream is null)
                throw new ArgumentNullException(nameof(stream));

            var reader = new AsciizBinaryReader(stream);

            byte[] signatureBytes = reader.ReadBytes(3);

            if ((signatureBytes[0] << 16 | signatureBytes[1] << 8 | signatureBytes[2]) != Signature)
                throw new InvalidDataException("The specified stream is not .pak archive.");

            _type = (PakArchiveType)reader.ReadByte();
            _gameVersion = (GameVersion)reader.ReadInt32();
            _unknownInt = reader.ReadInt32();

            int entriesCount = reader.ReadInt32();
            var entries = new PakArchiveEntry[entriesCount];

            for (int i = 0; i < entriesCount; i++)
            {
                string fullName = reader.ReadString();
                int position = reader.ReadInt32();
                int length = reader.ReadInt32();
                int unknownInt = reader.ReadInt32();
                int unknownInt2 = reader.ReadInt32();

                if (_gameVersion is GameVersion.Release)
                    position -= 13;

                entries[i] = new PakArchiveEntry(this, fullName, position, length, unknownInt, unknownInt2);
            }

            _entries = new List<PakArchiveEntry>(entries);
            _entriesCollection = new ReadOnlyCollection<PakArchiveEntry>(_entries);

            Reader = reader;
        }

        ~PakArchive()
        {
            Dispose(false);
        }

        public PakArchiveType Type => _type;
        public GameVersion GameVersion => _gameVersion;
        public int UnknownInt => _unknownInt;

        public ReadOnlyCollection<PakArchiveEntry> Entries => _entriesCollection;

        internal BinaryReader Reader { get; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Reader.Dispose();

                _entries.Clear();
            }
        }

        public PakArchiveEntry? GetEntry(string name)
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            foreach (PakArchiveEntry entry in _entriesCollection)
                if (entry.FullName == name)
                    return entry;

            return null;
        }
    }
}
