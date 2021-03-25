using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace wbclient
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
        Video,			//User has connected to Video Server
        Screen,			//User has connected to ScreenSharing Server
        Null,

        //drawing enums
        Pen,        //pen Shape tool
        Line,       //Line Shape Tool
        Rectangle,  //Rectangle Shape Tool 
        Circle,     //Circle Shape Tool
        Triangle,   //Triangle Shape Tool
        Capture,    //capture (Cut, Copy, Past) tool
        Eraser,     //Eraser
        Move,       // Move the Captured Area
        Highlighter,//Highliter Tool
        Clear
    }

    public class TextMessage
    {
        public string strName;      //Name by which the client logs into the room
        public string strMessage;   //Message text
        public string strRoom;			//Room Name
        public Command enumCommand;  //Command type (login, logout, send message, etcetera)

        //Default constructor
        public TextMessage()
        {
            this.enumCommand = Command.Null;
            this.strMessage = null;
            this.strName = null;
            this.strRoom = null;
        }

        //Converts the bytes into an object of type Data
        public TextMessage(byte[] data)
        {
            //The first four bytes are for the Command
            this.enumCommand = (Command)BitConverter.ToInt32(data, 0);

            //The next four store the length of the name
            int nameLen = BitConverter.ToInt32(data, 4);

            //The next four store the length of the message
            int msgLen = BitConverter.ToInt32(data, 8);

            //The next four store the length of the message
            int roomLen = BitConverter.ToInt32(data, 12);

            //This check makes sure that strName has been passed in the array of bytes
            if (nameLen > 0)
                this.strName = Encoding.UTF8.GetString(data, 16, nameLen);
            else
                this.strName = null;

            //This checks for a null message field
            if (msgLen > 0)
                this.strMessage = Encoding.UTF8.GetString(data, 16 + nameLen, msgLen);

            else
                this.strMessage = null;

            if (roomLen > 0)
                this.strRoom = Encoding.UTF8.GetString(data, 16 + nameLen + msgLen, roomLen);

            else
                this.strRoom = null;
        }

        //Converts the Data structure into an array of bytes
        public byte[] ToByte()
        {
            List<byte> result = new List<byte>();
            //First four are for the Command
            result.AddRange(BitConverter.GetBytes((int)enumCommand));

            //Add the length of the name
            if (strName != null)
                result.AddRange(BitConverter.GetBytes(strName.Length));
            else
                result.AddRange(BitConverter.GetBytes(0));

            //Length of the message
            if (strMessage != null)
                result.AddRange(BitConverter.GetBytes(strMessage.Length));
            else
                result.AddRange(BitConverter.GetBytes(0));

            //Length of the room
            if (strRoom != null)
                result.AddRange(BitConverter.GetBytes(strRoom.Length));
            else
                result.AddRange(BitConverter.GetBytes(0));

            //Add the name
            if (strName != null)
                result.AddRange(Encoding.UTF8.GetBytes(strName));

            //add the message text to our array of bytes
            if (strMessage != null)
                result.AddRange(Encoding.UTF8.GetBytes(strMessage));

            //add the room
            if (strRoom != null)
                result.AddRange(Encoding.UTF8.GetBytes(strRoom));

            return result.ToArray();
        }
    }
}
