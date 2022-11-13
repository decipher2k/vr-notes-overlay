using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;

public class TxtToImage
{/// <summary>
 /// Converting text to image (png).
 /// </summary>
 /// <param name="text">text to convert</param>
 /// <param name="font">Font to use</param>
 /// <param name="textColor">text color</param>
 /// <param name="maxWidth">max width of the image</param>
 /// <param name="path">path to save the image</param>
    public static byte[] DrawText(String text, System.Drawing.Font font, System.Drawing.Color textColor, int maxWidth, String path)
    {
        
        //first, create a dummy bitmap just to get a graphics object
        System.Drawing.Image img = (System.Drawing.Image)new System.Drawing.Bitmap(1, 1);
        System.Drawing.Graphics drawing = System.Drawing.Graphics.FromImage(img);
        //measure the string to see how big the image needs to be
        SizeF textSize = drawing.MeasureString(text, font, maxWidth);

        //set the stringformat flags to rtl
        StringFormat sf = new StringFormat();
        //uncomment the next line for right to left languages
        //sf.FormatFlags = StringFormatFlags.DirectionRightToLeft;
        sf.Trimming = StringTrimming.Word;
        //free up the dummy image and old graphics object
        img.Dispose();
        drawing.Dispose();

        //create a new image of the right size
        img = new System.Drawing.Bitmap((int)textSize.Width, (int)textSize.Height);

        drawing = System.Drawing.Graphics.FromImage(img);
        //Adjust for high quality
        drawing.CompositingQuality = CompositingQuality.HighQuality;
        drawing.InterpolationMode = InterpolationMode.HighQualityBilinear;
        drawing.PixelOffsetMode = PixelOffsetMode.HighQuality;
        drawing.SmoothingMode = SmoothingMode.HighQuality;
        drawing.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

        //paint the background
        drawing.Clear(System.Drawing.Color.Transparent);

        //create a brush for the text
        Brush textBrush = new SolidBrush(textColor);

        drawing.DrawString(text, font, textBrush, new RectangleF(0, 0, textSize.Width, textSize.Height), sf);

        drawing.Save();

        textBrush.Dispose();
        drawing.Dispose();
        MemoryStream s = new MemoryStream();
        img.Save(s, System.Drawing.Imaging.ImageFormat.Png);

        Image img1 = Image.FromStream(s);

        //img.Save(path, ImageFormat.Png);
        //img.Dispose();

        return s.ToArray();

    }
}
