using Octokit;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TwemojiMinecraftGen
{
    class GithubDownloader
    {
        private string _owner;
        private string _name;
        private string? _branchName;
        private GitHubClient _github;

        public GithubDownloader(string owner, string name, string? branchName = null)
        {
            _owner = owner;
            _name = name;
            _branchName = branchName;
            _github = new(new ProductHeaderValue("TwemojiMinecraftGen"));
        }
        public async Task<SortedDictionary<uint, byte[]>> Download()
        {
            if (_branchName == null) _branchName = (await _github.Repository.Get(_owner, _name)).DefaultBranch;
            Branch branch = await _github.Repository.Branch.Get(_owner, _name, _branchName);

            byte[] result = await _github.Repository.Content.GetArchive(_owner, _name, ArchiveFormat.Zipball);
            Regex searchPattern = new($"{_owner}-{_name}-{branch.Commit.Sha[0..7]}/assets/svg/.+?", RegexOptions.IgnoreCase);

            List<ZipArchiveEntry> filteredEntries = new();

            using MemoryStream stream = new(result);
            using ZipArchive archive = new(stream, ZipArchiveMode.Read, false);

            filteredEntries.AddRange(archive.Entries.Where(file => searchPattern.IsMatch(file.FullName)).Where(file => !file.Name.Contains('-')));

            SortedDictionary<uint, byte[]> emojis = new();

            foreach (ZipArchiveEntry entry in filteredEntries)
            {
                uint codepoint = Convert.ToUInt32(entry.Name.Replace(".svg", null), 16);
                uint utf16 = ToUtf16(codepoint);

                byte[] data = new byte[entry.Length];
                entry.Open().Read(data, 0, data.Length);
                emojis.Add(utf16, data);
            }

            return emojis;
        }
        public async Task<string> GetURL(bool omitProtocol = true)
        {
            string url = (await _github.Repository.Get(_owner, _name)).HtmlUrl;
            if (omitProtocol) url = url.Replace(@"http://", null).Replace(@"https://", null);

            return url;
        }
        private uint ToUtf16(uint codepoint)
        {
            // https://stackoverflow.com/questions/6240055/manually-converting-unicode-codepoints-into-utf-8-and-utf-16
            if (codepoint < 0xD800) return codepoint;
            if (codepoint < 0xE000) throw new ArgumentOutOfRangeException("Reserved codepoint");
            if (codepoint < 0x10000) return codepoint;
            if (codepoint < 0x110000)
            {
                codepoint = codepoint - 0x10000;
                codepoint = (codepoint & 0x3FF) | ((codepoint & 0xFFC00) << 6);
                codepoint = codepoint | 0b11011000000000001101110000000000;

                return codepoint;
            }

            throw new ArgumentOutOfRangeException("Out of range codepoint");
        }
    }
}
