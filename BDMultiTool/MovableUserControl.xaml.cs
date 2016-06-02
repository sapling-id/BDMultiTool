using BDMultiTool.Macros;
using BDMultiTool.Persistence;
using BDMultiTool.Utilities.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BDMultiTool {
    /// <summary>
    /// Interaction logic for MovableUserControl.xaml
    /// </summary>
    public partial class MovableUserControl : UserControl {
        private static int minSize = 100;
        private bool windowEventInitialized;
        private Point oldMousePosition;
        private Point anchorPoint;
        private Point currentMousePosition;
        public String lockedCollider { private set; get; }
        private Grid parent;

        public MovableUserControl(Grid parent) {
            InitializeComponent();
            anchorPoint = new Point();
            this.parent = parent;
        }

        public void setTitle(String title) {
            this.subWindowTitle.Content = title;
            tryLoadCurrentWindow();
        }

        public void setGridContent(UserControl userControl) {
            this.contentGrid.Children.Add(userControl);
        }

        private void generalImageMouseDown(object sender, MouseButtonEventArgs e) {
            if (!windowEventInitialized) {
                oldMousePosition = PointToScreen(e.GetPosition(this));
                lockedCollider = (sender as Image).Name;
                (sender as Image).CaptureMouse();
            }
            windowEventInitialized = true;
        }

        private void generalBorderMouseDown(object sender, MouseButtonEventArgs e) {
            if (!windowEventInitialized) {
                oldMousePosition = PointToScreen(e.GetPosition(this));
                lockedCollider = (sender as Border).Name;
                (sender as Border).CaptureMouse();
            }
            windowEventInitialized = true;
        }


        private void generalImageeMouseUp(object sender, MouseButtonEventArgs e) {
            windowEventInitialized = false;
            lockedCollider = "";
            (sender as Image).ReleaseMouseCapture();
        }

        private void generalBorderMouseUp(object sender, MouseButtonEventArgs e) {
            windowEventInitialized = false;
            lockedCollider = "";
            (sender as Border).ReleaseMouseCapture();
        }

        public void enableToggle(bool value) {
            if (value) {
                lockedCollider = "toggle";
            } else {
                lockedCollider = "";
            }

        }

        private void dragControl(object sender, MouseEventArgs e) {
            if (windowEventInitialized) {
                currentMousePosition = PointToScreen(e.GetPosition(this));
                double distanceX = currentMousePosition.X - oldMousePosition.X;
                double distanceY = currentMousePosition.Y - oldMousePosition.Y;
                oldMousePosition = PointToScreen(e.GetPosition(this));

                translateBy(distanceX, distanceY);
                persitCurrentWindow();
            }
        }

        private void resizeControl(object sender, MouseEventArgs e) {
            if (windowEventInitialized) {
                currentMousePosition = PointToScreen(e.GetPosition(this));
                double distanceX = currentMousePosition.X - oldMousePosition.X;
                double distanceY = currentMousePosition.Y - oldMousePosition.Y;
                oldMousePosition = PointToScreen(e.GetPosition(this));

                if ((this.Width + distanceX) < minSize) {
                    this.Width = minSize;
                } else {
                    this.Width = this.Width + distanceX;

                    translateForWidthResize(distanceX);
                }

                if ((this.Height + distanceY) < minSize) {
                    this.Height = minSize;
                } else {
                    this.Height = this.Height + distanceY;

                    translateForHeightResize(distanceY);
                }
                persitCurrentWindow();
            }
        }

        private void translateBy(double distanceX, double distanceY) {
            TranslateTransform transform = new TranslateTransform();
            transform.X = distanceX + anchorPoint.X;
            transform.Y = distanceY + anchorPoint.Y;
            this.RenderTransform = transform;

            anchorPoint.X = transform.X;
            anchorPoint.Y = transform.Y;
        }

        private void translateForHeightResize(double distanceY) {
            TranslateTransform transform = new TranslateTransform();
            transform.X = anchorPoint.X;
            transform.Y = distanceY / 2 + anchorPoint.Y;
            this.RenderTransform = transform;

            anchorPoint.X = transform.X;
            anchorPoint.Y = transform.Y;
        }

        private void translateForWidthResize(double distanceX) {
            TranslateTransform transform = new TranslateTransform();
            transform.X = distanceX / 2 + anchorPoint.X;
            transform.Y = anchorPoint.Y;
            this.RenderTransform = transform;

            anchorPoint.X = transform.X;
            anchorPoint.Y = transform.Y;
        }

        private void closeButton_MouseUp(object sender, MouseButtonEventArgs e) {
            this.Visibility = Visibility.Hidden;
            //parent.Children.Remove(this);
        }

        public void tryLoadCurrentWindow() {
            PersistenceContainer temporaryPersistenceContainer = PersistenceUnitThread.persistenceUnit.loadContainerByKey(this.subWindowTitle.Content.ToString() + this.GetType().Name);
            if(temporaryPersistenceContainer != null) {
                this.Width = Double.Parse(temporaryPersistenceContainer.content.Element("width").Value);
                this.Height = Double.Parse(temporaryPersistenceContainer.content.Element("height").Value);
                translateBy(Double.Parse(temporaryPersistenceContainer.content.Element("xOffset").Value), 
                            Double.Parse(temporaryPersistenceContainer.content.Element("yOffset").Value));
            }
        }

        public void persitCurrentWindow() {
            PersistenceUnitThread.persistenceUnit.addToPersistenceBuffer(PersistenceUnit.createPersistenceContainer(this.subWindowTitle.Content.ToString() + this.GetType().Name, 
                                                                                                                    this.GetType().Name, 
                                                                                                                    new String[][] {
                                                                                                                        new String[] { "height", this.Height.ToString() },
                                                                                                                        new String[] { "width", this.Width.ToString() },
                                                                                                                        new String[] { "xOffset", anchorPoint.X.ToString() },
                                                                                                                        new String[] { "yOffset", anchorPoint.Y.ToString() }
                                                                                                                    }));
            
        }
    }
}
