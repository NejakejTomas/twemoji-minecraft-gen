using Svg;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace TwemojiMinecraftGen
{
    class EmotePacker
    {
        public int EmoteSize { get; set; }
        public uint ThumbnailUTF16 { get; set; } = 0xD83DDE03;
        public SortedDictionary<uint, byte[]> Emotes { get; set; } = new();
        public string Name { get; set; } = "twemoji";
        private Bitmap[,] _emotesBitmaps = new Bitmap[0, 0];
        private string[,] _emotesNumbers = new string[0, 0];

        private void AddToBitmap(int posX, int posY, Bitmap bitmap)
        {
            _emotesBitmaps[posY, posX] = bitmap;
        }
        private void AddToJson(int posX, int posY, uint utf16)
        {
            _emotesNumbers[posY, posX] = string.Empty;

            ushort left = (ushort)((utf16 & 0xFFFF0000) >> 16);
            ushort right = (ushort)utf16;

            if (left != 0) _emotesNumbers[posY, posX] += $"\\u{left.ToString("X4")}";
            _emotesNumbers[posY, posX] += $"\\u{right.ToString("X4")}";
        }
        private Bitmap MakeBitmap()
        {
            Bitmap bitmap = new(_emotesBitmaps.GetLength(0) * EmoteSize, _emotesBitmaps.GetLength(1) * EmoteSize);
            Graphics graphics = Graphics.FromImage(bitmap);

            for (int y = 0; y < _emotesBitmaps.GetLength(0); y++)
            {
                for (int x = 0; x < _emotesBitmaps.GetLength(1); x++)
                {
                    graphics.DrawImage(_emotesBitmaps[y, x], new Point(x * EmoteSize, y * EmoteSize));
                }
            }

            return bitmap;
        }

        private string MakeJson()
        {
            string codes = string.Empty;

            for (int y = 0; y < _emotesNumbers.GetLength(0); y++)
            {
                codes += "\"";

                for (int x = 0; x < _emotesNumbers.GetLength(1); x++)
                {
                    codes += _emotesNumbers[y, x];
                }

                codes += $"\"";
                if (y < _emotesNumbers.GetLength(0) - 1) codes += ",";
                codes += Environment.NewLine;
            }

            string json = Properties.Resources.default_json.Replace(@"%chars%", codes).Replace(@"%name%", Name);

            return json;
        }
        public void MakeAtlas(Stream bitmapStream, Stream jsonStream)
        {
            int emotesSqrt = (int)Math.Ceiling(Math.Sqrt(Emotes.Count));
            int height = emotesSqrt * EmoteSize;
            int width = emotesSqrt * EmoteSize;
            _emotesBitmaps = new Bitmap[emotesSqrt, emotesSqrt];
            _emotesNumbers = new string[emotesSqrt, emotesSqrt];

            int position = 0;

            foreach (var emote in Emotes)
            {
                using (MemoryStream stream = new(emote.Value))
                {
                    SvgDocument? svg = SvgDocument.Open<SvgDocument>(stream);
                    if (svg == null) svg = new();

                    Bitmap emoteBitmap = svg.Draw(EmoteSize, EmoteSize);

                    AddToBitmap(position % emotesSqrt, position / emotesSqrt, emoteBitmap);
                    AddToJson(position % emotesSqrt, position / emotesSqrt, emote.Key);
                }

                position++;
            }

            for (; position < emotesSqrt * emotesSqrt; position++)
            {
                Bitmap emoteBitmap = new(EmoteSize, EmoteSize);

                AddToBitmap(position % emotesSqrt, position / emotesSqrt, emoteBitmap);
                AddToJson(position % emotesSqrt, position / emotesSqrt, 0);
            }

            MakeBitmap().Save(bitmapStream, ImageFormat.Png);

            using StreamWriter writer = new(jsonStream);
            writer.Write(MakeJson());

            return;
        }
        public void MakeThumbnail(Stream thumbnailStream)
        {
            if (Emotes.Count == 0) return;

            byte[]? svgBitmap;

            if (!Emotes.TryGetValue(ThumbnailUTF16, out svgBitmap)) svgBitmap = Emotes.First().Value;

            using MemoryStream stream = new(svgBitmap);

            SvgDocument? svg = SvgDocument.Open<SvgDocument>(stream);
            if (svg == null) svg = new();

            Bitmap emoteBitmap = svg.Draw(128, 128);
            emoteBitmap.Save(thumbnailStream, ImageFormat.Png);

            return;
        }
    }
}
