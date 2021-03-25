using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesktopPublisher
{
    public enum Command
    {
        Join,				//User request for joining room
        Left,				//User request for leaving room
        Msg,				//User has sent a message
        List,       //User request for users list in a room
        Draw,				//User request has sent drawing parameters
        Room,				//User is in room
        Alive,			//Keeping Alive Status
        Audio,			//User has connected to Audio Server
        Image,			//User has connected to Video Server
        Screen,			//User has connected to ScreenSharing Server
        Null,

        //drawing enums
        Pen,        //pen Shape tool
        Line,       //Line Shape Tool
        Rectangle,  //Rectangle Shape Tool 
        Circle,     //Circle Shape Tool
        Capture,    //capture (Cut, Copy, Past) tool
        Eraser,     //Eraser
        Move,       // Move the Captured Area
        Highlighter,//Highliter Tool
        Curve,      //Curves
        Polygons,    //Drawing the Polygons
        Clear,       //command to clear the Whole Canvas

        //Capture Or SelectionCommand
        DrawSelectionArea,  // to select the area to be action (Cut, Copy or Paste)
        Cut,                // to Cut the selected area
        Copy,               // to Copy the selected area
        Paste,               // to Paste the the selected copy on the Canvas
        PlaceChanged,        // on changing the place of object
        OnRelease,

        //fill the canvas with image
        fillCanvasImage
    }

    public class Message
    {
        public string strName;      //Name by which the client logs into the room
        public string strMessage;   //Message text
        public string strRoom;			//Room Name
        public Command enumCommand;  //Command type (login, logout, send message, etcetera)
        public byte[] databytes;

        //Default constructor
        public Message()
        {
            this.enumCommand = Command.Null;
            this.strMessage = null;
            this.strName = null;
            this.strRoom = null;
            this.databytes = null;
        }

        //Converts the bytes into an object of type Data
        public Message(byte[] data)
        {
            //The first four bytes are for the Command
            this.enumCommand = (Command)BitConverter.ToInt32(data, 0);

            //The next four store the length of the name
            int nameLen = BitConverter.ToInt32(data, 4);

            //The next four store the length of the message
            int msgLen = BitConverter.ToInt32(data, 8);

            //The next four store the length of the roomname
            int roomLen = BitConverter.ToInt32(data, 12);

            //The next four store the length of the databytes
            int databytesLen = BitConverter.ToInt32(data, 16);

            //This check makes sure that strName has been passed in the array of bytes
            if (nameLen > 0)
                this.strName = Encoding.UTF8.GetString(data, 20, nameLen);
            else
                this.strName = null;

            //This checks for a null message field
            if (msgLen > 0)
                this.strMessage = Encoding.UTF8.GetString(data, 20 + nameLen, msgLen);

            else
                this.strMessage = null;

            if (roomLen > 0)
                this.strRoom = Encoding.UTF8.GetString(data, 20 + nameLen + msgLen, roomLen);

            else
                this.strRoom = null;

            if (databytesLen > 0)
            {
                databytes = new byte[databytesLen];
                Buffer.BlockCopy(data, 20 + nameLen + msgLen + roomLen, databytes, 0, databytesLen);
            }
            else
                databytes = null;
        }
        //Converts the Data structure into an array of bytes
        public byte[] ToByte()
        {
            List<byte> result = new List<byte>();

            //First four are for the Command
            result.AddRange(BitConverter.GetBytes((int)enumCommand));

            //Add the length of the name
            if (strName != null)
                result.AddRange(BitConverter.GetBytes(this.strName.Length));
            else
                result.AddRange(BitConverter.GetBytes(0));

            //Length of the message
            if (strMessage != null)
                result.AddRange(BitConverter.GetBytes(this.strMessage.Length));
            else
                result.AddRange(BitConverter.GetBytes(0));

            //Length of the room
            if (strRoom != null)
                result.AddRange(BitConverter.GetBytes(this.strRoom.Length));
            else
                result.AddRange(BitConverter.GetBytes(0));

            //lenght of the databyes
            if (databytes != null)
                result.AddRange(BitConverter.GetBytes(this.databytes.Length));
            else
                result.AddRange(BitConverter.GetBytes(0));
            //Add the name
            if (strName != null)
                result.AddRange(Encoding.UTF8.GetBytes(this.strName));

            //add the message text to our array of bytes
            if (strMessage != null)
                result.AddRange(Encoding.UTF8.GetBytes(this.strMessage));

            //add the room
            if (strRoom != null)
                result.AddRange(Encoding.UTF8.GetBytes(this.strRoom));
            //add the databytes
            if (databytes != null)
                result.AddRange(databytes);

            return result.ToArray();
        }
    }
}