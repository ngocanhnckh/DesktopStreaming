using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;
namespace wbclient
{
    public enum DrawCommand
    {
        Connect,
        Join,
        Left,
        Draw,
        Null
    }
    public class DrawMessage
    {
        public DrawCommand DCommand;
        public int x;
        public int y;
        public string strRoom;
        public byte[] imgdata;

        //Default constructor
        public DrawMessage()
        {
            DCommand = DrawCommand.Null;
            strRoom = null;
            imgdata = null;
        }
        //Converts the bytes into an object of type Data
        public DrawMessage(byte[] data)
        {
            DCommand = (DrawCommand)BitConverter.ToInt32(data, 0);

            x = BitConverter.ToInt32(data, 4);

            y = BitConverter.ToInt32(data, 8);

            int roomlen = BitConverter.ToInt32(data, 12);

            int imgdataLen = BitConverter.ToInt32(data, 16);

            if (roomlen > 0)
                strRoom = Encoding.UTF8.GetString(data, 20, roomlen);
            else
                strRoom = null;

            if (imgdataLen > 0)
            {
                imgdata = new byte[imgdataLen];
                Buffer.BlockCopy(data, 20 + roomlen, imgdata, 0, imgdataLen);
            }
            else
                imgdata = null;
        }
        //Converts the Data structure into an array of bytes
        public byte[] ToByte()
        {
            List<byte> result = new List<byte>();
            //4bytes for command
            result.AddRange(BitConverter.GetBytes((int)DCommand));
            //4bytes for x
            result.AddRange(BitConverter.GetBytes(x));
            //4bytes for y
            result.AddRange(BitConverter.GetBytes(y));
            //4bytes for room lenght
            if (strRoom != null)
                result.AddRange(BitConverter.GetBytes(strRoom.Length));
            else
                result.AddRange(BitConverter.GetBytes(0));
            //4bytes of data lenght
            if (imgdata != null)
                result.AddRange(BitConverter.GetBytes(imgdata.Length));
            else
                result.AddRange(BitConverter.GetBytes(0));

            if (strRoom != null)
                result.AddRange(Encoding.UTF8.GetBytes(strRoom));

            if (imgdata != null)
                result.AddRange(imgdata);

            return result.ToArray();
        }
    }
}
