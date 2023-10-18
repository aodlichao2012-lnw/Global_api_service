using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_HelperCore
{
    public class Extention
    {
        Guid Guid { get; set; }
        //Capcha jigsaw
        public string Jigsaw()
        {
            Random random = new Random();
            int number_from_random = random.Next(0, 10);
            using (Bitmap image = new Bitmap($@"{Directory.GetCurrentDirectory()}\Image\{number_from_random}.jpg"))
            {
                int piecesWight = image.Width / 2;
                int piecesHeight = image.Width / 2;
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        Rectangle piecseRetangle = new Rectangle(i * piecesWight, j * piecesHeight, piecesWight, piecesHeight);
                        using (Bitmap picese = new Bitmap(piecesWight, piecesWight))
                        {
                            using (Graphics g = Graphics.FromImage(picese))
                            {
                                g.DrawImage(image, new Rectangle(0, 0, piecesWight, piecesWight), piecseRetangle, GraphicsUnit.Pixel);
                            }

                        }
                    }
                    return "";
                }
            }
            return "";
        }

    }
}