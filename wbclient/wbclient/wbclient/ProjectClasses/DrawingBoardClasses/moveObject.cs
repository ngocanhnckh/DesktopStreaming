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

namespace wbclient
{
    public class moveObject
    {
        private bool _isMovingObject;
        private Shape moveableShape;

        public moveObject()
        {
            moveableShape = null;
            _isMovingObject = false;
        }

        public Shape set_get_MoveableShape
        {
            set { moveableShape = value; }
            get { return moveableShape; }
        }

        public bool _isObjMove
        {
            set { _isMovingObject = value; }
            get { return _isMovingObject; }
        }

        public void shape_MouseEnter(object sender, MouseEventArgs e)
        {
            moveableShape = sender as Shape;
            moveableShape.Opacity = 0.6;

            moveableShape.Cursor = Cursors.Hand;
        }

        public void shape_MouseLeave(object sender, MouseEventArgs e)
        {
            moveableShape = sender as Shape;
            moveableShape.Opacity = 1;

        }
        public void shape_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            try
            {
                moveableShape = sender as Shape;
                moveableShape.Opacity = 0.4;
                _isMovingObject = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n in object moving mouseLeftButtonDown");
            }
        }

        public void shape_MouseLeftButtonUp(object sender, MouseEventArgs e)
        {
            try
            {
                moveableShape = sender as Shape;
                moveableShape.Opacity = 0.6;
                moveableShape = null;
                _isMovingObject = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n in object moving mouseLeftButtonDown");
            }
        }
    }
}
