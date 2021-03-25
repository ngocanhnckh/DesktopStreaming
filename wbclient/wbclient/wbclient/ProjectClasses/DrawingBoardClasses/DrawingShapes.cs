using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace wbclient
{
    public class DrawingShapes
    {
        private Line newLine;
        private Rectangle newRect;
        private Line newPen;
        private Ellipse newCircle;
        private Polygon newTriangle;

        public void drawLine(Point p1, Point p2, int strokThik, Color color, Canvas can)
        {
            newLine = new Line();
            newLine.X1 = 0;
            newLine.Y1 = 0;
            newLine.X2 = 0;
            newLine.Y2 = 0;

            newLine.Stroke = new SolidColorBrush(color);
            newLine.StrokeThickness = strokThik;

            can.Children.Add(newLine);

            // Create a duration of 2 seconds.
            Duration duration = new Duration(TimeSpan.FromSeconds(1));

            // Create two DoubleAnimations and set their properties.
            DoubleAnimation myDoubleAnimation1 = new DoubleAnimation();
            DoubleAnimation myDoubleAnimation2 = new DoubleAnimation();
            DoubleAnimation myDoubleAnimation3 = new DoubleAnimation();
            DoubleAnimation myDoubleAnimation4 = new DoubleAnimation();

            myDoubleAnimation1.Duration = duration;
            myDoubleAnimation2.Duration = duration;
            myDoubleAnimation3.Duration = duration;
            myDoubleAnimation4.Duration = duration;

            Storyboard sb = new Storyboard();
            sb.Duration = duration;

            sb.Children.Add(myDoubleAnimation1);
            sb.Children.Add(myDoubleAnimation2);
            sb.Children.Add(myDoubleAnimation3);
            sb.Children.Add(myDoubleAnimation4);

            Storyboard.SetTarget(myDoubleAnimation1, newLine);
            Storyboard.SetTarget(myDoubleAnimation2, newLine);
            Storyboard.SetTarget(myDoubleAnimation3, newLine);
            Storyboard.SetTarget(myDoubleAnimation4, newLine);

            // Set the attached properties of Canvas.Left and Canvas.Top
            // to be the target properties of the two respective DoubleAnimations.
            Storyboard.SetTargetProperty(myDoubleAnimation1, new PropertyPath("(X2)"));
            Storyboard.SetTargetProperty(myDoubleAnimation2, new PropertyPath("(Y2)"));
            Storyboard.SetTargetProperty(myDoubleAnimation3, new PropertyPath("(X1)"));
            Storyboard.SetTargetProperty(myDoubleAnimation4, new PropertyPath("(Y1)"));

            myDoubleAnimation1.To = p2.X;
            myDoubleAnimation2.To = p2.Y;
            myDoubleAnimation3.To = p1.X;
            myDoubleAnimation4.To = p1.Y;

            // Make the Storyboard a resource.
            can.Resources.Add("unique_id"+can.Resources.Count, sb);

            // Begin the animation.
            sb.Begin();
        }

        public void drawRectangle(Point p1, Point p2, int strokThik, Color col, Canvas can, Color fillColor)
        {
            newRect = new Rectangle();

            double Left = Math.Min(Math.Abs(p1.X), Math.Abs(p2.X));
            double Top = Math.Min(Math.Abs(p1.Y), Math.Abs(p2.Y));

            Canvas.SetLeft(newRect, 0);
            Canvas.SetTop(newRect, 0);

            newRect.Height = 0;
            newRect.Width = 0;
            newRect.Fill = new SolidColorBrush(fillColor);
            newRect.Stroke = new SolidColorBrush(col);
            newRect.StrokeThickness = strokThik;
            can.Children.Add(newRect);


            // Create a duration of 2 seconds.
            Duration duration = new Duration(TimeSpan.FromSeconds(1));

            // Create two DoubleAnimations and set their properties.
            DoubleAnimation myDoubleAnimation1 = new DoubleAnimation();
            DoubleAnimation myDoubleAnimation2 = new DoubleAnimation();
            DoubleAnimation myDoubleAnimation3 = new DoubleAnimation();
            DoubleAnimation myDoubleAnimation4 = new DoubleAnimation();

            myDoubleAnimation1.Duration = duration;
            myDoubleAnimation2.Duration = duration;
            myDoubleAnimation3.Duration = duration;
            myDoubleAnimation4.Duration = duration;

            Storyboard sb = new Storyboard();
            sb.Duration = duration;

            sb.Children.Add(myDoubleAnimation1);
            sb.Children.Add(myDoubleAnimation2);
            sb.Children.Add(myDoubleAnimation3);
            sb.Children.Add(myDoubleAnimation4);

            Storyboard.SetTarget(myDoubleAnimation1, newRect);
            Storyboard.SetTarget(myDoubleAnimation2, newRect);
            Storyboard.SetTarget(myDoubleAnimation3, newRect);
            Storyboard.SetTarget(myDoubleAnimation4, newRect);

            // Set the attached properties of Canvas.Left and Canvas.Top
            // to be the target properties of the two respective DoubleAnimations.
            Storyboard.SetTargetProperty(myDoubleAnimation1, new PropertyPath("(Canvas.Left)"));
            Storyboard.SetTargetProperty(myDoubleAnimation2, new PropertyPath("(Canvas.Top)"));
            Storyboard.SetTargetProperty(myDoubleAnimation3, new PropertyPath("(Height)"));
            Storyboard.SetTargetProperty(myDoubleAnimation4, new PropertyPath("(Width)"));

            myDoubleAnimation1.To = Left;
            myDoubleAnimation2.To = Top;
            myDoubleAnimation3.To = Math.Abs(p1.Y - p2.Y);
            myDoubleAnimation4.To = Math.Abs(p1.X - p2.X);

            // Make the Storyboard a resource.
            can.Resources.Add("unique_id" + can.Resources.Count, sb);

            // Begin the animation.
            sb.Begin();
        }

        public void drawCircle(Point p1, Point p2, int strokThik, Color col, Canvas can, Color fillColor)
        {
            newCircle = new Ellipse();

            double Left = Math.Min(Math.Abs(p1.X), Math.Abs(p2.X));
            double Top = Math.Min(Math.Abs(p1.Y), Math.Abs(p2.Y));

            Canvas.SetLeft(newCircle, 0);
            Canvas.SetTop(newCircle, 0);

            newCircle.Height = 0;
            newCircle.Width = 0;
            newCircle.Fill = new SolidColorBrush(fillColor);
            newCircle.Stroke = new SolidColorBrush(col);
            newCircle.StrokeThickness = strokThik;
            can.Children.Add(newCircle);

            // Create a duration of 2 seconds.
            Duration duration = new Duration(TimeSpan.FromSeconds(1));

            // Create two DoubleAnimations and set their properties.
            DoubleAnimation myDoubleAnimation1 = new DoubleAnimation();
            DoubleAnimation myDoubleAnimation2 = new DoubleAnimation();
            DoubleAnimation myDoubleAnimation3 = new DoubleAnimation();
            DoubleAnimation myDoubleAnimation4 = new DoubleAnimation();

            myDoubleAnimation1.Duration = duration;
            myDoubleAnimation2.Duration = duration;
            myDoubleAnimation3.Duration = duration;
            myDoubleAnimation4.Duration = duration;

            Storyboard sb = new Storyboard();
            sb.Duration = duration;

            sb.Children.Add(myDoubleAnimation1);
            sb.Children.Add(myDoubleAnimation2);
            sb.Children.Add(myDoubleAnimation3);
            sb.Children.Add(myDoubleAnimation4);

            Storyboard.SetTarget(myDoubleAnimation1, newCircle);
            Storyboard.SetTarget(myDoubleAnimation2, newCircle);
            Storyboard.SetTarget(myDoubleAnimation3, newCircle);
            Storyboard.SetTarget(myDoubleAnimation4, newCircle);

            // Set the attached properties of Canvas.Left and Canvas.Top
            // to be the target properties of the two respective DoubleAnimations.
            Storyboard.SetTargetProperty(myDoubleAnimation1, new PropertyPath("(Canvas.Left)"));
            Storyboard.SetTargetProperty(myDoubleAnimation2, new PropertyPath("(Canvas.Top)"));
            Storyboard.SetTargetProperty(myDoubleAnimation3, new PropertyPath("(Height)"));
            Storyboard.SetTargetProperty(myDoubleAnimation4, new PropertyPath("(Width)"));

            myDoubleAnimation1.To = Left;
            myDoubleAnimation2.To = Top;
            myDoubleAnimation3.To = Math.Abs(p1.Y - p2.Y);
            myDoubleAnimation4.To = Math.Abs(p1.X - p2.X);

            // Make the Storyboard a resource.
            can.Resources.Add("unique_id" + can.Resources.Count, sb);

            // Begin the animation.
            sb.Begin();
        }

        public void drawTriangle(Point p1, Point p2, int strokThik, Color Bordercol, Canvas can, Color fillColor)
        {
            newTriangle = new Polygon();
            Point[] trianglePoints = new Point[3];
            trianglePoints[0] = new Point(Math.Abs(p1.X + p2.X) / 2, p1.Y);
            trianglePoints[1] = new Point(Math.Abs(p1.X), Math.Abs(p2.Y));
            trianglePoints[2] = new Point(Math.Abs(p2.X), Math.Abs(p2.Y));
            foreach (Point p in trianglePoints)
            {
                newTriangle.Points.Add(p);
            }
            newTriangle.Stroke = new SolidColorBrush(Bordercol);
            newTriangle.StrokeThickness = strokThik;
            newTriangle.Fill = new SolidColorBrush(fillColor);

            can.Children.Add(newTriangle);

            // MessageBox.Show("First Point: " + p1.ToString() + "\n\nSecond Point: " + p2.ToString());
        }

        public void drawPenLine(Point[] points, int strokThik, Color borderColor, Canvas can)
        {
            newPen = null;
            try
            {
                for (int i = 0; i < points.Length; i++)
                {
                    newPen = new Line();
                    newPen.X1 = points[i].X;
                    newPen.Y1 = points[i].Y;

                    newPen.X2 = points[i + 1].X;
                    newPen.Y2 = points[i + 1].Y;
                    newPen.Stroke = new SolidColorBrush(borderColor);
                    newPen.StrokeThickness = strokThik;
                    can.Children.Add(newPen);
                    if (i >= points.Length - 2)
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        #region Reciving Functions and other func[CropImageFung, ColorCodeConverter]...
        public void recieveDrwingBytes(byte[] drawingBytes, Canvas paintcanvas)
        {
            TextMessage recDrawDataMsg = new TextMessage(drawingBytes);
            if (recDrawDataMsg.enumCommand == Command.Draw)
            {
                string[] drawingInfo = recDrawDataMsg.strMessage.Split('?');
                string selectedTool = drawingInfo[0];
                switch (selectedTool)
                {
                    case "Pen":
                        {
                            try
                            {
                                List<Point> pointList = new List<Point>();
                                Point[] pointsAsArg;
                                string[] pointInStr = drawingInfo[1].Split('*');

                                for (int i = 0; i < pointInStr.Length - 1; i++)
                                {
                                    string[] point = pointInStr[i].Split(',');
                                    pointList.Add(new Point(int.Parse(point[0]), int.Parse(point[1])));
                                }

                                pointsAsArg = new Point[pointList.Count];
                                int x = 0;
                                foreach (Point p in pointList)
                                {
                                    pointsAsArg[x++] = p;
                                }
                                drawPenLine(pointsAsArg, int.Parse(drawingInfo[2]), colorCodeConverter(drawingInfo[3]), paintcanvas);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message + " in Receive function.");
                            }
                            break;
                        }
                    case "Highlighter":
                        {
                            try
                            {
                                List<Point> pointList = new List<Point>();
                                Point[] pointsAsArg;
                                string[] pointInStr = drawingInfo[1].Split('*');

                                for (int i = 0; i < pointInStr.Length - 1; i++)
                                {
                                    string[] point = pointInStr[i].Split(',');
                                    pointList.Add(new Point(int.Parse(point[0]), int.Parse(point[1])));
                                }

                                pointsAsArg = new Point[pointList.Count];
                                int x = 0;
                                foreach (Point p in pointList)
                                {
                                    pointsAsArg[x++] = p;
                                }
                                drawPenLine(pointsAsArg, int.Parse(drawingInfo[2]), colorCodeConverter(drawingInfo[3], "4F"), paintcanvas);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message + " in Receive function.");
                            }
                            break;
                        }
                    case "Line":
                        {
                            try
                            {
                                string[] recDataStr = recDrawDataMsg.strMessage.Split('?');
                                string[] firstPoint = recDataStr[1].Split(',');
                                string[] secondPoint = recDataStr[2].Split(',');

                                drawLine(new Point(double.Parse(firstPoint[0]), double.Parse(firstPoint[1])),
                                    new Point(double.Parse(secondPoint[0]), double.Parse(secondPoint[1])),
                                    int.Parse(recDataStr[3]),
                                    colorCodeConverter(recDataStr[4], null),
                                    paintcanvas);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message + " in Receive function.");
                            }
                            break;
                        }
                    case "Rectangle":
                        {
                            try
                            {
                                string[] recDataStr = recDrawDataMsg.strMessage.Split('?');
                                string[] firstPoint = recDataStr[1].Split(',');
                                string[] secondPoint = recDataStr[2].Split(',');

                                drawRectangle(new Point(double.Parse(firstPoint[0]), double.Parse(firstPoint[1])),
                                    new Point(double.Parse(secondPoint[0]), double.Parse(secondPoint[1])),
                                    int.Parse(recDataStr[3]), colorCodeConverter(recDataStr[4]), paintcanvas,
                                    colorCodeConverter(recDataStr[5]));
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message + " in Receive function.");
                            }
                            break;
                        }
                    case "Triangle":
                        {
                            try
                            {
                                string[] recDataStr = recDrawDataMsg.strMessage.Split('?');
                                string[] firstPoint = recDataStr[1].Split(',');
                                string[] secondPoint = recDataStr[2].Split(',');

                                drawTriangle(new Point(double.Parse(firstPoint[0]), double.Parse(firstPoint[1])),
                                    new Point(double.Parse(secondPoint[0]), double.Parse(secondPoint[1])),
                                    int.Parse(recDataStr[3]), colorCodeConverter(recDataStr[4]), paintcanvas,
                                    colorCodeConverter(recDataStr[5]));
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message + " in Receive function.");
                            }
                            break;
                        }
                    case "Circle":
                        {
                            try
                            {
                                string[] recDataStr = recDrawDataMsg.strMessage.Split('?');
                                string[] firstPoint = recDataStr[1].Split(',');
                                string[] secondPoint = recDataStr[2].Split(',');

                                drawCircle(new Point(double.Parse(firstPoint[0]), double.Parse(firstPoint[1])),
                                    new Point(double.Parse(secondPoint[0]), double.Parse(secondPoint[1])),
                                    int.Parse(recDataStr[3]), colorCodeConverter(recDataStr[4]), paintcanvas,
                                    colorCodeConverter(recDataStr[5]));
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message + " in Receive function.");
                            }
                            break;
                        }
                    case "Capture":
                        {
                            break;
                        }
                    case "Eraser":
                        {
                            try
                            {
                                List<Point> pointList = new List<Point>();
                                Point[] pointsAsArg;
                                string[] pointInStr = drawingInfo[1].Split('*');

                                for (int i = 0; i < pointInStr.Length - 1; i++)
                                {
                                    string[] point = pointInStr[i].Split(',');
                                    pointList.Add(new Point(int.Parse(point[0]), int.Parse(point[1])));
                                }

                                pointsAsArg = new Point[pointList.Count];
                                int x = 0;
                                foreach (Point p in pointList)
                                {
                                    pointsAsArg[x++] = p;
                                }
                                drawPenLine(pointsAsArg, int.Parse(drawingInfo[2]), colorCodeConverter(drawingInfo[3]), paintcanvas);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message + " in Receive function.");
                            }
                            break;
                        }
                    case "Clear":
                        {
                            paintcanvas.Children.Clear();
                            break;
                        }
                }
            }
        }


        public WriteableBitmap CropImage(WriteableBitmap source, int xOffset, int yOffset, int width, int height)
        {
            // Get the width of the source image
            var sourceWidth = source.PixelWidth;

            // Get the resultant image as WriteableBitmap with specified size
            var result = new WriteableBitmap(width, height);

            // Create the array of bytes
            for (var x = 0; x <= height - 1; x++)
            {
                var sourceIndex = xOffset + (yOffset + x) * sourceWidth;
                var destinationIndex = x * width;

                Array.Copy(source.Pixels, sourceIndex, result.Pixels, destinationIndex, width);
            }
            return result;
        }

        public Color colorCodeConverter(string str)
        {
            string myStr = str.Replace("#", "");
            byte a, r, g, b;
            r = 255;
            g = 255;
            b = 255;
            a = 255;


            int start = 0;

            if (myStr.Length == 8)
            {
                a = byte.Parse(myStr.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                start = 2;
            }

            r = byte.Parse(myStr.Substring(start, 2), System.Globalization.NumberStyles.HexNumber);
            g = byte.Parse(myStr.Substring(start + 2, 2), System.Globalization.NumberStyles.HexNumber);
            b = byte.Parse(myStr.Substring(start + 4, 2), System.Globalization.NumberStyles.HexNumber);

            return Color.FromArgb(a, r, g, b);
        }

        public Color colorCodeConverter(string str, string transprancyInHex)
        {
            string myStr = str.Replace("#", "");
            byte a, r, g, b;
            r = 255;
            g = 255;
            b = 255;

            if (transprancyInHex == null)
            {
                a = 255;
            }
            else
            {
                a = byte.Parse(transprancyInHex, System.Globalization.NumberStyles.HexNumber);
            }

            int start = 2;

            //if (myStr.Length == 8)
            //{
            //    a = byte.Parse(myStr.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            //    start = 2;
            //}

            r = byte.Parse(myStr.Substring(start, 2), System.Globalization.NumberStyles.HexNumber);
            g = byte.Parse(myStr.Substring(start + 2, 2), System.Globalization.NumberStyles.HexNumber);
            b = byte.Parse(myStr.Substring(start + 4, 2), System.Globalization.NumberStyles.HexNumber);

            return Color.FromArgb(a, r, g, b);
        }
        #endregion

    }
}
