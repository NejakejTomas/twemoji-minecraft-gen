using System;
using System.IO;
using System.IO.Compression;

namespace TwemojiMinecraftGen
{
    class ResourcePackCreator : IDisposable
    {
        private bool _disposed = false;
        private FileStream _fileStream;
        private ZipArchive _zipArchive;
        public ResourcePackCreator(string location)
        {
            _fileStream = new(location, FileMode.Create);
            _zipArchive = new ZipArchive(_fileStream, ZipArchiveMode.Update);
        }

        public Stream Add(string path)
        {
            ZipArchiveEntry entry = _zipArchive.CreateEntry(path);

            return entry.Open();
        }



        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources.
                    _zipArchive.Dispose();
                    _fileStream.Dispose();
                }
                // Dispose unmanaged resources here.

                _disposed = true;
            }
        }
        ~ResourcePackCreator()
        {
            Dispose(false);
        }
    }
}
