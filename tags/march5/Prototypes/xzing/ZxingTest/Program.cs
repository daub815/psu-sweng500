using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.google.zxing.qrcode;
using com.google.zxing.common;
using com.google.zxing;
using System.Drawing;
using com.google.zxing.oned;

namespace ZxingTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Bitmap bitmap = new Bitmap(@"K:\Homework\Penn State University\SWENG 500\zxing\CameraTest\bin\Debug\Snapshot_20120114_084956.jpg");
            
            var reader = new MultiFormatReader();
            var image = new RGBLuminanceSource(bitmap, bitmap.Width, bitmap.Height);
            var binarizer = new HybridBinarizer(image);
            var binaryBitmap = new BinaryBitmap(binarizer);


            var hints = new System.Collections.Hashtable();
            hints.Add(DecodeHintType.TRY_HARDER, true);

            var result = reader.decode(binaryBitmap, hints);
            Console.ReadLine();
            bitmap.Dispose();
        }
    }
}
