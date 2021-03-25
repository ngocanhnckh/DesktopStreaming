using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text;
using System.IO;
using VoiceEncoder;
using System.Windows.Data;

namespace wbclient
{
    public partial class MainPage : UserControl
    {
        private TextMessage message;
        private string room;
        private Client clientobj;
        private DrawingImageClient drawingclientobj;

        #region Global Declarations
        private CaptureSource _source = new CaptureSource();
        private PcmToWave _pcm = new PcmToWave();
        private MemoryAudioSink _sink;
        public bool _isRecording;
        public bool _isConnected = false;
        private AudioClient audcli;
        private delegate void mydelegate(byte[] buffer);
        public delegate void ShowMessagedelegate(string MSG);
        private delegate void Enabledelegate(bool value);
        #endregion Global Declarations
             

        //	Delegates
        public delegate void Drawmydelegate(byte[] buffer);
        public delegate void conDelegate();
        private delegate void playaudiodelegate(byte[] buffer);

        private delegate void RoomChangedelegate();
        private delegate void drawsendjoinnotification();
        private delegate void SetCallBack(TextMessage msg);
        DrawingShapes drawShapes = new DrawingShapes();

        // Delegate Objects
        public conDelegate condelobj;
        public Drawmydelegate drimgobj;
        private RoomChangedelegate rc;
        public ShowMessagedelegate showmessage;


        public MainPage(string uname, string roomz)
        {
            InitializeComponent();
            //////////////////////initparams///////////////////////////////////
            uname_textbox.Text = uname;
            roomselected_combo.Items.Add("Please Select...");
            string[] rmz = roomz.Split('?');
            foreach (string rom in rmz)
                roomselected_combo.Items.Add(rom);
            roomselected_combo.SelectedIndex = 0;
            /////////////////////////client objects/////////////////////////////////
            clientobj = new Client(this);
            drawingclientobj = new DrawingImageClient(this);
            audcli = new AudioClient(this);
            //////////////////////attach delegates///////////////////////////////////
            rc += new RoomChangedelegate(roomchanged);
            condelobj += new conDelegate(sendconnectionparam);
            drimgobj += new Drawmydelegate(DrawViewReceivedImage);
            showmessage += new ShowMessagedelegate(ShowMessageBox);

            //////////////////////initialize drawing//////////////////////////////////////////
            DrawingElementInit();
        }

        private void DrawingElementInit()
        {
            combColorPik.SelectedIndex = 0;
            if (((Rectangle)combColorPik.SelectedItem).Fill is SolidColorBrush)
            {
                SolidColorBrush sb = new SolidColorBrush();
                sb = ((Rectangle)combColorPik.SelectedItem).Fill as SolidColorBrush;
                selectedColor = sb.Color;
            }
            combFillCol.SelectedIndex = 0;
            SelectedfillColor = new Color();
            tool = Command.Pen;
            for (int i = 1; i <= 20; i++)
            {
                newLine = new Line();
                newLine.StrokeThickness = i;
                newLine.Stroke = new SolidColorBrush(Colors.Black);
                newLine.Height = 20;
                newLine.Width = 100;
                newLine.X1 = 0;
                newLine.Y1 = 10;
                newLine.X2 = 100;
                newLine.Y2 = 10;
                combBordSize.Items.Add(newLine);
            }
            combBordSize.SelectedIndex = 0;
        }

        public void OnConncetCompleted(object sender, SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                this.Dispatcher.BeginInvoke(new ShowMessagedelegate(ShowMessageBox), "Connceted Successfully!");
                audcli.StartReceiving();
                _isConnected = true;
            }
            else
            {
                this.Dispatcher.BeginInvoke(new ShowMessagedelegate(ShowMessageBox), e.SocketError.ToString());
            }

        }
        public void OnSendCompleted(object sender, SocketAsyncEventArgs e)
        {

        }
        public void OnReceiveCompleted(object sender, SocketAsyncEventArgs e)
        {
            this.Dispatcher.BeginInvoke(new mydelegate(PlayReceivedBuffer), e.Buffer);

        }
        #region Audio Encoding/Decoding Methods

        void SendVoiceBuffer(object VoiceBuffer, EventArgs e)
        {

            byte[] PCM_Buffer = (byte[])VoiceBuffer;
            byte[] Encoded = VoiceEncoder.G711Audio.ALawEncoder.ALawEncode(PCM_Buffer);
            audcli.Send_Bytes(Encoded);
        }

        void OpenWavFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog().Value)
            {
                Stream s = ofd.File.OpenRead();
                WaveMediaStreamSource wavMss = new WaveMediaStreamSource(s);
                mediaElement1.SetSource(wavMss);
                mediaElement1.Play();
            }
        }

        private void PlayReceivedBuffer(byte[] Encodedbuffer)
        {
            if (MuteCheckBox.IsChecked == false)
                try
                {
                    //Decode to Wave Format Then Play
                    byte[] DecodedBuffer = new byte[Encodedbuffer.Length * 2];
                    VoiceEncoder.G711Audio.ALawDecoder.ALawDecode(Encodedbuffer, out DecodedBuffer); ;
                    PlayWave(DecodedBuffer);

                }
                catch (Exception) { }

            audcli.StartReceiving();
        }

        void PlayWave(byte[] PCMBytes)
        {
            MemoryStream ms_PCM = new MemoryStream(PCMBytes);
            MemoryStream ms_Wave = new MemoryStream();

            _pcm.SavePcmToWav(ms_PCM, ms_Wave, 16, 8000, 1);

            WaveMediaStreamSource WaveStream = new WaveMediaStreamSource(ms_Wave);
            mediaElement1.SetSource(WaveStream);
            mediaElement1.Play();
        }

        #endregion Audio Encoding/Decoding Methods

        private void send_draw_room_change_param()
        {
            DrawMessage sm = new DrawMessage();
            sm.DCommand = DrawCommand.Join;
            sm.strRoom = roomselected_combo.SelectedItem.ToString();
            drawingclientobj.Send_Bytes(sm.ToByte());
        }

        private void ShowMessageBox(string MSG)
        {
            MessageBox.Show(MSG);
        }

        private void DrawViewReceivedImage(byte[] buffer)
        {
            try
            {
                DrawMessage dm = new DrawMessage(buffer);
                MemoryStream ms = new MemoryStream(dm.imgdata);
                BitmapImage result = new BitmapImage();
                result.SetSource(ms);
                ms.Close();
                Rectangle rect = new Rectangle();
                ImageBrush ib = new ImageBrush();
                ib.ImageSource = result;
                rect.Height = result.PixelHeight;
                rect.Width = result.PixelWidth;
                Canvas.SetTop(rect, dm.y);
                Canvas.SetLeft(rect, dm.x);
                rect.Fill = ib;
                paintcanvas.Children.Add(rect);
            }
            catch (Exception) { }
            finally
            {
                drawingclientobj.DrawStartReceiving();
            }
        }

        public void EncodeJpeg(WriteableBitmap bmp, Stream destinationStream)
        {
            // Init buffer in FluxJpeg format
            int w = bmp.PixelWidth;
            int h = bmp.PixelHeight;
            int[] p = bmp.Pixels;
            byte[][,] pixelsForJpeg = new byte[3][,]; // RGB colors
            pixelsForJpeg[0] = new byte[w, h];
            pixelsForJpeg[1] = new byte[w, h];
            pixelsForJpeg[2] = new byte[w, h];

            // Copy WriteableBitmap data into buffer for FluxJpeg
            int i = 0;
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    int color = p[i++];
                    pixelsForJpeg[0][x, y] = (byte)(color >> 16); // R
                    pixelsForJpeg[1][x, y] = (byte)(color >> 8);  // G
                    pixelsForJpeg[2][x, y] = (byte)(color);       // B
                }
            }

            // Encode Image as JPEG using the FluxJpeg library
            // and write to destination stream
            //ColorModel cm = new ColorModel { colorspace = ColorSpace.RGB };
            //FluxJpeg.Core.Image jpegImage = new FluxJpeg.Core.Image(cm, pixelsForJpeg);
            //JpegEncoder encoder = new JpegEncoder(jpegImage, 95, destinationStream);
            //encoder.Encode();
        }

        private void Connect_btn_Click(object sender, RoutedEventArgs e)
        {
            clientobj.Connect();
            drawingclientobj.Connect();
        }

        private void sendconnectionparam()
        {
            message = new TextMessage();
            message.enumCommand = Command.Join;
            message.strName = uname_textbox.Text;
            if (roomselected_combo.SelectedIndex > 0)
                message.strRoom = roomselected_combo.SelectedItem.ToString();
            else
                message.strRoom = null;
            clientobj.Send(message);
        }

        public void Set(TextMessage msg)
        {
            if (this.Dispatcher.CheckAccess())
            {
                if (msg.enumCommand == Command.Msg)
                {
                    receiveBox.Text += msg.strName + ":" + msg.strMessage + "\n";
                }
                else if (msg.enumCommand == Command.List)
                {
                    roomusers_lbox.Items.Clear();
                    string[] users = msg.strMessage.Split('*');
                    for (int i = 0; i <= users.Length - 1; i++)
                    {
                        roomusers_lbox.Items.Add(users[i]);
                    }
                }
                else if (msg.enumCommand == Command.Draw)
                {
                    drawShapes.recieveDrwingBytes(msg.ToByte(),this.paintcanvas);
                }
                else if (msg.enumCommand == Command.Join)
                {
                    receiveBox.Text += msg.strMessage + "\n";
                }
                else if (msg.enumCommand == Command.Left)
                {
                    receiveBox.Text += msg.strMessage + "\n";
                }
            }
            else
                this.Dispatcher.BeginInvoke(new SetCallBack(Set), msg);
        }

        private void sendButton_Click(object sender, RoutedEventArgs e)
        {
            message = new TextMessage();
            message.enumCommand= Command.Msg;
            message.strMessage = sendBox.Text;
            message.strName = uname_textbox.Text;
            message.strRoom = room;
            //receiveBox.Text += message.strName + ":" + sendBox.Text + "\n";
            clientobj.Send(message);
            sendBox.Text = "";
        }

        private void sendBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                sendButton_Click(this, null);
        }

        private void receiveBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            receiveBox.SelectAll();
            sendBox.Focus();
        }

        private void roomchanged()
        {
            //send room name to text and drawing server
            message = new TextMessage();
            message.enumCommand = Command.Left;
            message.strName = uname_textbox.Text;
            message.strRoom = roomselected_combo.SelectedItem.ToString();
            room = roomselected_combo.SelectedItem.ToString();
            sendBox.Text = "";
            clientobj.Send(message);
            //send room name to imaging server
            send_draw_room_change_param();
        }

        private void roomselected_combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(rc);
        }



        #region Shapes Functions Complete Drawing Section....

        #region Shape Globle Variables...

        moveObject movingObject = new moveObject();

        Color selectedColor;
        Color SelectedfillColor;
        int borderThiknes;

        private DrawingShapes makeShape = new DrawingShapes();

        Line newLine;
        Rectangle newRect;
        Rectangle newSelectionTool = null;
        Ellipse newCircle;
        Polygon newTriangle;
        Shape onEventShape, lastShape = null;
        Point[] lastPoints = null;
        ImageBrush ib;

        Point fPoint, sPoint;
        List<Point> pointList;
        int shapeCouter = 0;

        Command tool;
        bool mouseLeftButtonPress = false;
        #endregion

        #region Mouse Events To Draw Shape Functions [mouseButtonDown, mouseMove & mouseButtonUp]...

        private void paintcanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!movingObject._isObjMove)
            {
                if (tool == Command.Capture && newSelectionTool != null)
                {
                    newRect = new Rectangle();
                    paintcanvas.Children.Remove(newSelectionTool);
                    newRect.Fill = newSelectionTool.Fill;
                    newRect.Width = newSelectionTool.Width;
                    newRect.Height = newSelectionTool.Height;
                    Canvas.SetLeft(newRect, Canvas.GetLeft(newSelectionTool));
                    Canvas.SetTop(newRect, Canvas.GetTop(newSelectionTool));
                    newRect.StrokeThickness = 0;
                    paintcanvas.Children.Add(newRect);
                }
                mouseLeftButtonPress = true;
                lastShape = null;
                switch (tool)
                {
                    case Command.Pen:
                        {
                            pointList = new List<Point>();
                            fPoint = e.GetPosition(paintcanvas);
                            newLine = new Line();
                            newLine.X1 = fPoint.X;
                            newLine.Y1 = fPoint.Y;
                            newLine.StrokeThickness = borderThiknes;
                            newLine.Stroke = new SolidColorBrush(selectedColor);
                            onEventShape = newLine;
                            break;
                        }
                    case Command.Line:
                        {
                            newLine = new Line();
                            fPoint = e.GetPosition(this.paintcanvas);
                            newLine.X1 = fPoint.X;
                            newLine.Y1 = fPoint.Y;
                            newLine.Stroke = new SolidColorBrush(selectedColor);
                            newLine.Fill = new SolidColorBrush(SelectedfillColor);
                            newLine.StrokeThickness = borderThiknes;
                            onEventShape = newLine;
                            break;
                        }
                    case Command.Rectangle:
                        {
                            newRect = new Rectangle();
                            fPoint = e.GetPosition(this.paintcanvas);
                            //Canvas.SetTop(newRect, fPoint.Y);
                            //Canvas.SetLeft(newRect, fPoint.X);

                            newRect.Stroke = new SolidColorBrush(selectedColor);
                            newRect.StrokeThickness = borderThiknes;
                            newRect.Fill = new SolidColorBrush(SelectedfillColor);
                            onEventShape = newRect;
                            break;
                        }
                    case Command.Circle:
                        {
                            newCircle = new Ellipse();
                            fPoint = e.GetPosition(this.paintcanvas);
                            //Canvas.SetTop(newCircle, fPoint.Y);
                            //Canvas.SetLeft(newCircle, fPoint.X);
                            newCircle.Stroke = new SolidColorBrush(selectedColor);
                            newCircle.StrokeThickness = borderThiknes;
                            newCircle.Fill = new SolidColorBrush(SelectedfillColor);
                            onEventShape = newCircle;
                            break;
                        }
                    case Command.Triangle:
                        {
                            newTriangle = new Polygon();
                            fPoint = e.GetPosition(paintcanvas);
                            Canvas.SetLeft(newTriangle, fPoint.X);
                            Canvas.SetTop(newTriangle, fPoint.Y);
                            newTriangle.Stroke = new SolidColorBrush(selectedColor);
                            newTriangle.Fill = new SolidColorBrush(SelectedfillColor);
                            newTriangle.StrokeThickness = borderThiknes;
                            onEventShape = newTriangle;
                            break;
                        }
                    case Command.Capture:
                        {
                            newSelectionTool = new Rectangle();
                            fPoint = e.GetPosition(paintcanvas);
                            //Canvas.SetTop(newRect, fPoint.Y);
                            //Canvas.SetLeft(newRect, fPoint.X);

                            newSelectionTool.StrokeDashArray.Add(4);
                            newSelectionTool.StrokeDashArray.Add(2);
                            newSelectionTool.Stroke = new SolidColorBrush(Colors.Black);
                            newSelectionTool.StrokeThickness = 0.5;
                            //newSelectionTool.Fill = new SolidColorBrush(Colors.Purple);
                            onEventShape = newSelectionTool;
                            break;
                        }
                    case Command.Eraser:
                        {
                            pointList = new List<Point>();
                            fPoint = e.GetPosition(paintcanvas);
                            newLine = new Line();
                            newLine.X1 = fPoint.X;
                            newLine.Y1 = fPoint.Y;
                            newLine.StrokeThickness = 5;

                            newLine.Stroke = new SolidColorBrush(new Color());
                            onEventShape = newLine;
                            break;
                        }
                    case Command.Highlighter:
                        {
                            //DrawingShapes getColor = new DrawingShapes();
                            pointList = new List<Point>();
                            fPoint = e.GetPosition(paintcanvas);
                            newLine = new Line();
                            newLine.X1 = fPoint.X;
                            newLine.Y1 = fPoint.Y;
                            //newLine.Stroke = new SolidColorBrush(Colors.Yellow);
                            newLine.Stroke = new SolidColorBrush(Color.FromArgb
                                (byte.Parse("7F", System.Globalization.NumberStyles.HexNumber),
                                byte.Parse("FF", System.Globalization.NumberStyles.HexNumber),
                                byte.Parse("FF", System.Globalization.NumberStyles.HexNumber),
                                byte.Parse("17", System.Globalization.NumberStyles.HexNumber)));
                            newLine.StrokeThickness = 10;
                            onEventShape = newLine;
                            break;
                        }
                    case Command.Move:
                        {
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
        }

        private void paintcanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseLeftButtonPress && !movingObject._isObjMove)
            {
                onEventShape.MouseLeftButtonDown += new MouseButtonEventHandler(paintcanvas_MouseMove);
                switch (tool)
                {
                    case Command.Pen:
                        {
                            onEventShape = newLine;

                            sPoint = e.GetPosition(paintcanvas);
                            newLine.X2 = sPoint.X;
                            newLine.Y2 = sPoint.Y;
                            paintcanvas.Children.Add(newLine);
                            pointList.Add(sPoint);

                            newLine = new Line();
                            newLine.Stroke = new SolidColorBrush(selectedColor);
                            newLine.StrokeThickness = borderThiknes;
                            newLine.X1 = sPoint.X;
                            newLine.Y1 = sPoint.Y;

                            break;
                        }
                    case Command.Line:
                        {
                            onEventShape = newLine;

                            sPoint = e.GetPosition(paintcanvas);
                            newLine.X2 = sPoint.X;
                            newLine.Y2 = sPoint.Y;

                            if (lastShape != null)
                            {
                                paintcanvas.Children.Remove(newLine);
                            }

                            lastShape = newLine;

                            paintcanvas.Children.Add(newLine);
                            break;
                        }
                    case Command.Rectangle:
                        {

                            onEventShape = newRect;
                            sPoint = e.GetPosition(paintcanvas);
                            double X2 = sPoint.X;
                            double Y2 = sPoint.Y;
                            double X1 = fPoint.X;
                            double Y1 = fPoint.Y;

                            double x = 0.0, y = 0.0;

                            if (X1 > X2)
                            {
                                newRect.Width = X1 - X2;
                                x = X2;
                            }
                            else
                            {
                                newRect.Width = X2 - X1;
                                x = X1;
                            }

                            if (Y1 > Y2)
                            {
                                newRect.Height = Y1 - Y2;
                                y = Y2;
                            }
                            else
                            {
                                newRect.Height = Y2 - Y1;
                                y = Y1;
                            }

                            newRect.SetValue(Canvas.LeftProperty, x);
                            newRect.SetValue(Canvas.TopProperty, y);


                            if (lastShape != null)
                            {
                                paintcanvas.Children.Remove(newRect);
                            }
                            lastShape = newRect;

                            paintcanvas.Children.Add(newRect);
                            break;
                        }
                    case Command.Circle:
                        {
                            onEventShape = newCircle;
                            sPoint = e.GetPosition(paintcanvas);
                            double X2 = sPoint.X;
                            double Y2 = sPoint.Y;
                            double X1 = fPoint.X;
                            double Y1 = fPoint.Y;

                            double x = 0.0, y = 0.0;

                            if (X1 > X2)
                            {
                                newCircle.Width = X1 - X2;
                                x = X2;
                            }
                            else
                            {
                                newCircle.Width = X2 - X1;
                                x = X1;
                            }

                            if (Y1 > Y2)
                            {
                                newCircle.Height = Y1 - Y2;
                                y = Y2;
                            }
                            else
                            {
                                newCircle.Height = Y2 - Y1;
                                y = Y1;
                            }

                            newCircle.SetValue(Canvas.LeftProperty, x);
                            newCircle.SetValue(Canvas.TopProperty, y);

                            if (lastShape != null)
                            {
                                paintcanvas.Children.Remove(newCircle);
                            }
                            lastShape = newCircle;

                            paintcanvas.Children.Add(newCircle);
                            break;
                        }
                    case Command.Triangle:
                        {
                            onEventShape = newTriangle;
                            sPoint = e.GetPosition(paintcanvas);

                            Point[] trianglePoints = new Point[3];
                            trianglePoints[0] = new Point(Math.Abs(fPoint.X + sPoint.X) / 2, Math.Abs(fPoint.Y));
                            trianglePoints[1] = new Point(Math.Abs(fPoint.X), Math.Abs(sPoint.Y));
                            trianglePoints[2] = new Point(Math.Abs(sPoint.X), Math.Abs(sPoint.Y));

                            if (lastPoints != null)
                            {
                                foreach (Point lp in lastPoints)
                                {
                                    newTriangle.Points.Remove(lp);
                                }
                            }

                            lastPoints = trianglePoints;

                            foreach (Point p in trianglePoints)
                            {
                                newTriangle.Points.Add(p);
                            }

                            if (lastShape != null)
                            {
                                paintcanvas.Children.Remove(newTriangle);
                            }

                            lastShape = newTriangle;
                            paintcanvas.Children.Add(newTriangle);
                            break;
                        }
                    case Command.Capture:
                        {
                            onEventShape = newSelectionTool;
                            sPoint = e.GetPosition(paintcanvas);
                            double X2 = sPoint.X;
                            double Y2 = sPoint.Y;
                            double X1 = fPoint.X;
                            double Y1 = fPoint.Y;

                            double x = 0.0, y = 0.0;

                            if (X1 > X2)
                            {
                                newSelectionTool.Width = X1 - X2;
                                x = X2;
                            }
                            else
                            {
                                newSelectionTool.Width = X2 - X1;
                                x = X1;
                            }

                            if (Y1 > Y2)
                            {
                                newSelectionTool.Height = Y1 - Y2;
                                y = Y2;
                            }
                            else
                            {
                                newSelectionTool.Height = Y2 - Y1;
                                y = Y1;
                            }

                            newSelectionTool.SetValue(Canvas.LeftProperty, x);
                            newSelectionTool.SetValue(Canvas.TopProperty, y);

                            if (lastShape != null)
                            {
                                paintcanvas.Children.Remove(newSelectionTool);
                            }

                            lastShape = newSelectionTool;

                            Shape shape = newSelectionTool;

                            shape.Name = "Selection:" + shapeCouter.ToString();
                            ToolTipService.SetToolTip(shape, newSelectionTool.Name + "\n\r counter : " + shape.GetType().ToString());
                            shape.MouseEnter += new MouseEventHandler(movingObject.shape_MouseEnter);
                            shape.MouseLeave += new MouseEventHandler(movingObject.shape_MouseLeave);
                            shape.MouseLeftButtonDown += new MouseButtonEventHandler(movingObject.shape_MouseLeftButtonDown);
                            shape.MouseLeftButtonUp += new MouseButtonEventHandler(movingObject.shape_MouseLeftButtonUp);

                            paintcanvas.Children.Add(newSelectionTool);

                            break;
                        }
                    case Command.Eraser:
                        {
                            onEventShape = newLine;

                            sPoint = e.GetPosition(paintcanvas);
                            newLine.X2 = sPoint.X;
                            newLine.Y2 = sPoint.Y;
                            paintcanvas.Children.Add(newLine);
                            pointList.Add(sPoint);

                            newLine = new Line();

                            newLine.Stroke = new SolidColorBrush(Colors.White);
                            newLine.StrokeThickness = 5;
                            newLine.X1 = sPoint.X;
                            newLine.Y1 = sPoint.Y;

                            break;
                        }
                    case Command.Highlighter:
                        {
                            onEventShape = newLine;

                            sPoint = e.GetPosition(paintcanvas);
                            newLine.X2 = sPoint.X;
                            newLine.Y2 = sPoint.Y;
                            paintcanvas.Children.Add(newLine);
                            pointList.Add(sPoint);

                            newLine = new Line();

                            newLine.Stroke = new SolidColorBrush(Color.FromArgb
                            (byte.Parse("7F", System.Globalization.NumberStyles.HexNumber),
                            byte.Parse("FF", System.Globalization.NumberStyles.HexNumber),
                            byte.Parse("FF", System.Globalization.NumberStyles.HexNumber),
                            byte.Parse("17", System.Globalization.NumberStyles.HexNumber)));

                            newLine.StrokeThickness = 10;
                            newLine.X1 = sPoint.X;
                            newLine.Y1 = sPoint.Y;

                            break;
                        }
                    case Command.Move:
                        {
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            if (movingObject._isObjMove)
            {
                double x = e.GetPosition(paintcanvas).X - movingObject.set_get_MoveableShape.Width / 2;
                double y = e.GetPosition(paintcanvas).Y - movingObject.set_get_MoveableShape.Height / 2;

                movingObject.set_get_MoveableShape.SetValue(Canvas.LeftProperty, x);
                movingObject.set_get_MoveableShape.SetValue(Canvas.TopProperty, y);
            }
            
        }

        private void paintcanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TextMessage drawShapeMsg = new TextMessage();
            mouseLeftButtonPress = false;
            drawShapeMsg.enumCommand = Command.Draw;
            drawShapeMsg.strName = uname_textbox.Text;
            drawShapeMsg.strRoom = room;

            switch (tool)
            {
                case Command.Pen:
                    {
                        try
                        {
                            Point[] points = new Point[pointList.Count];
                            List<Point> distinctPointsList = new List<Point>();
                            //MessageBox.Show("Actual Sise of pen list = "+pointList.Count.ToString());
                            distinctPointsList.AddRange(pointList.Distinct());
                            //MessageBox.Show("Distinct size of pen list = " + distinctPointsList.Count.ToString());
                            int i = 0;
                            drawShapeMsg.strMessage = Command.Pen.ToString() + "?";
                            foreach (Point p in distinctPointsList)
                            {
                                points[i++] = p;
                            }
                            for (int x = 0; x < points.Length; x++)
                            {
                                drawShapeMsg.strMessage += points[x].ToString() + "*";
                            }
                            drawShapeMsg.strMessage += "?" + borderThiknes.ToString() + "?" + selectedColor.ToString() + "?";
                            
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("in 'Command.Pen' Switch case" + ex.Message);
                        }
                        
                        //recieveDrwingBytes(drawShapeMsg.ToByte());
                        break;
                    }
                case Command.Line:
                    {
                        drawShapeMsg.strMessage = Command.Line.ToString() + "?" + fPoint.ToString() + "?" + sPoint.ToString() + "?" + borderThiknes.ToString() + "?" + selectedColor.ToString() + "?";
                        //recieveDrwingBytes(drawShapeMsg.ToByte());
                        break;
                    }
                case Command.Rectangle:
                    {
                        drawShapeMsg.strMessage = Command.Rectangle.ToString() + "?" + fPoint.ToString() + "?" + sPoint.ToString() + "?" + borderThiknes.ToString() + "?" + selectedColor.ToString() + "?" + SelectedfillColor.ToString() + "?";
                        //recieveDrwingBytes(drawShapeMsg.ToByte());
                        break;
                    }
                case Command.Circle:
                    {
                        drawShapeMsg.strMessage = Command.Circle.ToString() + "?" + fPoint.ToString() + "?" + sPoint.ToString() + "?" + borderThiknes.ToString() + "?" + selectedColor.ToString() + "?" + SelectedfillColor.ToString() + "?";
                        //recieveDrwingBytes(drawShapeMsg.ToByte());
                        break;
                    }
                case Command.Triangle:
                    {
                        //MessageBox.Show("Fisrt : " + fPoint.ToString() + "Second : " + sPoint.ToString());
                        drawShapeMsg.strMessage = Command.Triangle.ToString() +"?"+ fPoint.ToString() + "?" + sPoint.ToString() + "?" + borderThiknes.ToString() + "?" + selectedColor.ToString() + "?" + SelectedfillColor.ToString() + "?";
                        //recieveDrwingBytes(drawShapeMsg.ToByte());

                        //drawTriangle(fPoint, sPoint, borderThiknes, selectedColor, testCanvas, SelectedfillColor);
                        break;
                    }
                case Command.Capture:
                    {
                        try
                        {
                            DrawingShapes cropImg = new DrawingShapes();
                            newSelectionTool.StrokeThickness = 0;
                            Rectangle locRect = new Rectangle();
                            locRect.Width = newSelectionTool.Width;
                            locRect.Height = newSelectionTool.Height;
                            locRect.StrokeThickness = 0;
                            Canvas.SetLeft(locRect, 0);
                            Canvas.SetTop(locRect, 0);

                            WriteableBitmap image = new WriteableBitmap(paintcanvas, null);
                            WriteableBitmap cropedImage = cropImg.CropImage(image, (int)fPoint.X, (int)fPoint.Y, (int)locRect.Width, (int)locRect.Height);
                            ib = new ImageBrush();
                            MemoryStream stream = new MemoryStream();
                            BitmapImage bitmapImg = new BitmapImage();

                            EncodeJpeg(cropedImage, stream);
                            bitmapImg.SetSource(stream);
                            ib.ImageSource = bitmapImg;

                            locRect.Fill = ib;
                            newSelectionTool.Fill = ib;

                            //testCanvas.Children.Add(locRect);
                            newSelectionTool.StrokeThickness = 0.5;
                            shapeCouter++;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message + "\n\n in mouse up event of pantcanvs");
                        }

                        break;
                    }
                case Command.Eraser:
                    {
                        Point[] points = new Point[pointList.Count];
                        int i = 0;
                        drawShapeMsg.strMessage = Command.Eraser.ToString() + "?";
                        foreach (Point p in pointList)
                        {
                            points[i++] = p;
                        }
                        for (int x = 0; x < points.Length; x++)
                        {
                            drawShapeMsg.strMessage += points[x].ToString() + "*";
                        }
                        drawShapeMsg.strMessage +="?" + 5.ToString() + "?" + Colors.White.ToString() + "?";
                        break;
                    }
                case Command.Highlighter:
                    {
                        Point[] points = new Point[pointList.Count];
                        int i = 0;
                        drawShapeMsg.strMessage = Command.Highlighter.ToString() + "?";
                        foreach (Point p in pointList)
                        {
                            points[i++] = p;
                        }
                        for (int x = 0; x < points.Length; x++)
                        {
                            drawShapeMsg.strMessage += points[x].ToString() + "*";
                        }
                        drawShapeMsg.strMessage += "?" + 10.ToString() + "?" + Colors.Red.ToString() + "?";
                        break;
                    }
                case Command.Move:
                    {
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            try
            {
                //myStr = drawShapeMsg.strMessage;
                //MessageBox.Show("Size of mesage : " + myStr.Length.ToString());
                clientobj.Send(drawShapeMsg);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "in the drawShape sending exeption");
            }
            //List<ArraySegment<byte>> list = new List<ArraySegment<byte>>();
            //sendEventArgs.BufferList = list;
            //socket.SendAsync(sendEventArgs);
            
        }

        #endregion

        #region Drawing Tool Selection Buttion Events....

        private void btnDrawLine_Click(object sender, RoutedEventArgs e)
        {
            if (tool == Command.Capture)
            {
                newRect = new Rectangle();
                paintcanvas.Children.Remove(newSelectionTool);
                newRect.Fill = newSelectionTool.Fill;
                newRect.Width = newSelectionTool.Width;
                newRect.Height = newSelectionTool.Height;
                Canvas.SetLeft(newRect, Canvas.GetLeft(newSelectionTool));
                Canvas.SetTop(newRect, Canvas.GetTop(newSelectionTool));
                newRect.StrokeThickness = 0;
                paintcanvas.Children.Add(newRect);
            }
            tool = Command.Line;
        }

        private void combFillCol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (((Rectangle)combFillCol.SelectedItem).Fill is SolidColorBrush)
            {
                SolidColorBrush sb = new SolidColorBrush();
                sb = ((Rectangle)combFillCol.SelectedItem).Fill as SolidColorBrush;
                SelectedfillColor = sb.Color;
            }
            else
            {
                try
                {
                    SelectedfillColor = new Color();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnDrawRect_Click(object sender, RoutedEventArgs e)
        {
            if (tool == Command.Capture)
            {
                newRect = new Rectangle();
                paintcanvas.Children.Remove(newSelectionTool);
                newRect.Fill = newSelectionTool.Fill;
                newRect.Width = newSelectionTool.Width;
                newRect.Height = newSelectionTool.Height;
                Canvas.SetLeft(newRect, Canvas.GetLeft(newSelectionTool));
                Canvas.SetTop(newRect, Canvas.GetTop(newSelectionTool));
                newRect.StrokeThickness = 0;
                paintcanvas.Children.Add(newRect);
            }
            tool = Command.Rectangle;
        }

        private void btnDrawCircle_Click(object sender, RoutedEventArgs e)
        {
            if (tool == Command.Capture)
            {
                newRect = new Rectangle();
                paintcanvas.Children.Remove(newSelectionTool);
                newRect.Fill = newSelectionTool.Fill;
                newRect.Width = newSelectionTool.Width;
                newRect.Height = newSelectionTool.Height;
                Canvas.SetLeft(newRect, Canvas.GetLeft(newSelectionTool));
                Canvas.SetTop(newRect, Canvas.GetTop(newSelectionTool));
                newRect.StrokeThickness = 0;
                paintcanvas.Children.Add(newRect);
            }
            tool = Command.Circle;
        }

        private void btnDrawTriangle_Click(object sender, RoutedEventArgs e)
        {
            if (tool == Command.Capture)
            {
                newRect = new Rectangle();
                paintcanvas.Children.Remove(newSelectionTool);
                newRect.Fill = newSelectionTool.Fill;
                newRect.Width = newSelectionTool.Width;
                newRect.Height = newSelectionTool.Height;
                Canvas.SetLeft(newRect, Canvas.GetLeft(newSelectionTool));
                Canvas.SetTop(newRect, Canvas.GetTop(newSelectionTool));
                newRect.StrokeThickness = 0;
                paintcanvas.Children.Add(newRect);
            }
            tool = Command.Triangle;
        }

        private void combBordSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tool == Command.Capture)
            {
                newRect = new Rectangle();
                paintcanvas.Children.Remove(newSelectionTool);
                newRect.Fill = newSelectionTool.Fill;
                newRect.Width = newSelectionTool.Width;
                newRect.Height = newSelectionTool.Height;
                Canvas.SetLeft(newRect, Canvas.GetLeft(newSelectionTool));
                Canvas.SetTop(newRect, Canvas.GetTop(newSelectionTool));
                newRect.StrokeThickness = 0;
                paintcanvas.Children.Add(newRect);
            }
            borderThiknes = (int)((Line)combBordSize.SelectedItem).StrokeThickness;
            //MessageBox.Show(borderThiknes.ToString());
        }

        private void btnDrawSelect_Click(object sender, RoutedEventArgs e)
        {
            tool = Command.Capture;
            //lastWasSelection = true;
        }

        private void btnDrawPen_Click(object sender, RoutedEventArgs e)
        {
            if (tool == Command.Capture)
            {
                newRect = new Rectangle();
                paintcanvas.Children.Remove(newSelectionTool);
                newRect.Fill = newSelectionTool.Fill;
                newRect.Width = newSelectionTool.Width;
                newRect.Height = newSelectionTool.Height;
                Canvas.SetLeft(newRect, Canvas.GetLeft(newSelectionTool));
                Canvas.SetTop(newRect, Canvas.GetTop(newSelectionTool));
                newRect.StrokeThickness = 0;
                paintcanvas.Children.Add(newRect);
            }
            tool = Command.Pen;
        }

        private void btnDrawClearCanvas_Click(object sender, RoutedEventArgs e)
        {
            TextMessage clearCmd = new TextMessage();
            clearCmd.enumCommand = Command.Draw;
            clearCmd.strName = uname_textbox.Text;
            clearCmd.strRoom = room;
            clearCmd.strMessage = Command.Clear.ToString() + "?";
            clientobj.Send(clearCmd);

            //paintcanvas.Children.Clear();
        }

        private void btnDrawEraser_Click(object sender, RoutedEventArgs e)
        {
            if (tool == Command.Capture)
            {
                newRect = new Rectangle();
                paintcanvas.Children.Remove(newSelectionTool);
                newRect.Fill = newSelectionTool.Fill;
                newRect.Width = newSelectionTool.Width;
                newRect.Height = newSelectionTool.Height;
                Canvas.SetLeft(newRect, Canvas.GetLeft(newSelectionTool));
                Canvas.SetTop(newRect, Canvas.GetTop(newSelectionTool));
                newRect.StrokeThickness = 0;
                paintcanvas.Children.Add(newRect);
            }
            tool = Command.Eraser;
        }

        private void combColorPik_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((Rectangle)combColorPik.SelectedItem).Fill is SolidColorBrush)
            {
                SolidColorBrush sb = new SolidColorBrush();
                sb = ((Rectangle)combColorPik.SelectedItem).Fill as SolidColorBrush;
                selectedColor = sb.Color;
            }
        }

        private void btnHiglight_Click(object sender, RoutedEventArgs e)
        {
            if (tool == Command.Capture)
            {
                newRect = new Rectangle();
                paintcanvas.Children.Remove(newSelectionTool);
                newRect.Fill = newSelectionTool.Fill;
                newRect.Width = newSelectionTool.Width;
                newRect.Height = newSelectionTool.Height;
                Canvas.SetLeft(newRect, Canvas.GetLeft(newSelectionTool));
                Canvas.SetTop(newRect, Canvas.GetTop(newSelectionTool));
                newRect.StrokeThickness = 0;
                paintcanvas.Children.Add(newRect);
            }
            tool = Command.Highlighter;
        }
        #endregion

        #endregion
    }
}