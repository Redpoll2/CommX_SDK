using System.IO;
using System.Reflection;

namespace PackageManager.IO.Tests.Resources
{
    internal static class TestResources
    {
        private static readonly Assembly _assembly = typeof(TestResources).Assembly;

        internal static string[] ResourceNames => _assembly.GetManifestResourceNames();

        internal static Stream? GetResourceStream(string filename)
        {
            return _assembly.GetManifestResourceStream($"{_assembly.GetName().Name}.Resources.{filename}");
        }
    }
}
