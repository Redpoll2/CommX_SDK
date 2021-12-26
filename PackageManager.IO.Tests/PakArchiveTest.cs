using PackageManager.IO.Tests.Resources;
using System;
using System.IO;
using Xunit;

namespace PackageManager.IO.Tests
{
    public class PakArchiveTest
    {
        [Fact]
        public void Ctor()
        {
            using Stream stream = TestResources.GetResourceStream("TheWolfsDen.pak") ?? throw new Exception("Can't find test resource.");
            using var archive = new PakArchive(stream);

            Assert.Equal(PakArchiveType.Map, archive.Type);
            Assert.Equal(GameVersion.Release, archive.GameVersion);
            Assert.Equal(1, archive.UnknownInt);
            Assert.Equal(780, archive.Entries.Count);
        }
    }
}
