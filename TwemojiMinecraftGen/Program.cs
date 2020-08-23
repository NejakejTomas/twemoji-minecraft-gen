using Svg;
using System;
using System.Collections.Generic;
using System.IO;
using TwemojiMinecraftGen.Properties;

namespace TwemojiMinecraftGen
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> sizes = new();

            foreach (string s in args)
            {
                try
                {
                    int size = Convert.ToInt32(s);
                    sizes.Add(size);
                }
                catch
                {
                    Console.WriteLine($"Unrecognized size: {s}");
                }
            }

#if DEBUG
            sizes.Add(128);
#endif

            EmotePacker packer = new();

            try
            {
                SvgDocument.EnsureSystemIsGdiPlusCapable();

                GithubDownloader githubDownloader = new("twitter", "twemoji");
                SortedDictionary<uint, byte[]> emotes = githubDownloader.Download().GetAwaiter().GetResult();
                packer.Emotes = emotes;

                foreach (int size in sizes)
                {
                    packer.EmoteSize = size;

                    using (ResourcePackCreator resourcePackCreator = new($"Twemoji-{packer.EmoteSize}px.zip"))
                    {
                        packer.MakeAtlas(
                            resourcePackCreator.Add($"assets/minecraft/textures/font/{packer.Name}.png"),
                            resourcePackCreator.Add(@"assets/minecraft/font/default.json"));

                        using (StreamWriter writer = new(resourcePackCreator.Add(@"pack.mcmeta")))
                        {
                            string githubLink = githubDownloader.GetURL().GetAwaiter().GetResult();
                            writer.Write(Resources.pack_mcmeta.Replace("%github_link%", githubLink));
                        }

                        packer.MakeThumbnail(resourcePackCreator.Add(@"pack.png"));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
