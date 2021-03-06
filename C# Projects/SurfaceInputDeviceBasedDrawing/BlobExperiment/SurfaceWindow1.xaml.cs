using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using Microsoft.Surface;
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;
using Microsoft.Surface.Presentation.Input;
using System.Windows.Ink;

/**
 * Author: Kapil
 * Try these with surface input simulator software :) 
 * 
 * This software will demonstrate how to use different types of touch input devices (blobs and tags) 
 * and when two touches (other than finger touch) is detected, it will draw a bounding circle.
 * Other than that this class provides distance calculator for two given input devices, displays 
 * all input devices (except finger touch) coordinates and shows how many input devices (including finger touch) it has detected.
 * */
namespace BlobExperiment
{
    /// <summary>
    /// Interaction logic for SurfaceWindow1.xaml
    /// </summary>
    public partial class SurfaceWindow1 : SurfaceWindow
    {
        TextBox detectedInputs = new TextBox();
        List<TouchDevice> tagTouchDevices = new List<TouchDevice>();
        List<TouchDevice> blobTouchDevices = new List<TouchDevice>();

        Path boundingCirlce = new Path();// { Width = 100, Height = 100, Margin = new Thickness(100,100,0,0) };
        EllipseGeometry circle = new EllipseGeometry(new Point(100, 100), 25, 25);
        /// <summary>
        /// Default constructor.
        /// </summary>
        public SurfaceWindow1()
        {
            InitializeComponent();
            detectedInputs.HorizontalAlignment = HorizontalAlignment.Left;
            detectedInputs.VerticalAlignment = VerticalAlignment.Top;
            detectedInputs.FontSize = 20;
            detectedInputs.Text = "Waiting for touch devices";

            boundingCirlce.Fill = System.Windows.Media.Brushes.Blue;
            boundingCirlce.Stroke = System.Windows.Media.Brushes.Black;
            boundingCirlce.Opacity = 0.25;
            boundingCirlce.StrokeThickness = 2.0;
            boundingCirlce.Visibility = Visibility.Hidden;
            boundingCirlce.Data = circle;
            boundingCirlce.PreviewTouchDown += inkCanvasForceCapture;
            myCanvas.Children.Add(boundingCirlce);
            myCanvas.Children.Add(detectedInputs);

            inkCanvas.DefaultDrawingAttributes.Color = Color.FromRgb(0, 0, 0);
            inkCanvas.DefaultDrawingAttributes.FitToCurve = true;
            // Add handlers for window availability events
            AddWindowAvailabilityHandlers();
        }

        /// <summary>
        /// Occurs when the window is about to close. 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // Remove handlers for window availability events
            RemoveWindowAvailabilityHandlers();
        }

        /// <summary>
        /// Adds handlers for window availability events.
        /// </summary>
        private void AddWindowAvailabilityHandlers()
        {
            // Subscribe to surface window availability events
            ApplicationServices.WindowInteractive += OnWindowInteractive;
            ApplicationServices.WindowNoninteractive += OnWindowNoninteractive;
            ApplicationServices.WindowUnavailable += OnWindowUnavailable;
        }

        /// <summary>
        /// Removes handlers for window availability events.
        /// </summary>
        private void RemoveWindowAvailabilityHandlers()
        {
            // Unsubscribe from surface window availability events
            ApplicationServices.WindowInteractive -= OnWindowInteractive;
            ApplicationServices.WindowNoninteractive -= OnWindowNoninteractive;
            ApplicationServices.WindowUnavailable -= OnWindowUnavailable;
        }

        /// <summary>
        /// This is called when the user can interact with the application's window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowInteractive(object sender, EventArgs e)
        {
            //TODO: enable audio, animations here
        }

        /// <summary>
        /// This is called when the user can see but not interact with the application's window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowNoninteractive(object sender, EventArgs e)
        {
            //TODO: Disable audio here if it is enabled

            //TODO: optionally enable animations here
        }

        /// <summary>
        /// This is called when the application's window is not visible or interactive.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowUnavailable(object sender, EventArgs e)
        {
            //TODO: disable audio, animations here
        }

  
        Int32 noOfFingerTouches = 0;
        Int32 noOfBlobs = 0;
        Int32 noOfTags = 0;
        bool blobDetected;
        bool tagDetected;
        bool fingerTouchDetected;
        private void canvas_Touch_Down(object sender, TouchEventArgs e)
        {
            if (e.TouchDevice.GetIsTagRecognized()) //Tags
            {
                Interlocked.Increment(ref noOfTags);
                detectedInputs.Text = " Fingers: " + noOfFingerTouches + "\nTags:" + noOfTags + "\nBlobs: " + noOfBlobs;
                tagTouchDevices.Add(e.TouchDevice);
                updateViews(e.TouchDevice);
            }
            else if (e.TouchDevice.GetIsFingerRecognized()) //Finger
            {
                Interlocked.Increment(ref noOfFingerTouches);
                fingerTouchDetected = true;
                detectedInputs.Text = " Fingers: " + noOfFingerTouches + "\nTags:" + noOfTags + "\nBlobs: " + noOfBlobs;
            }
            else//Blobs
            {
                Interlocked.Increment(ref noOfBlobs);
                detectedInputs.Text = " Fingers: " + noOfFingerTouches + "\nTags:" + noOfTags + "\nBlobs: " + noOfBlobs;
                blobTouchDevices.Add(e.TouchDevice);
                updateViews(e.TouchDevice);
            }
            updateInputDataDisplayer();
        }

        bool inDrawingMode = false;
        //Occurs when the touch input device is moved
        private void canvas_Touch_Move(object sender, TouchEventArgs e)
        {
            //Console.WriteLine("Sender: " + sender);
            updateInputDataDisplayer();

            //Only draw when two tags are detected and finger touch(es) are detected
            if (noOfTags == 2 && fingerTouchDetected)
            {
                inDrawingMode = true; // we don't want both canvas to call update Metrics
                updateViews(e.TouchDevice);
                e.TouchDevice.Capture(inkCanvas);
                //e.Handled = true;
            }
            else inDrawingMode = false;
            //we don't need to update metrics since normal canvas's event will update it
            updateViews(e.TouchDevice);
        }


        private void canvas_Touch_Up(object sender, TouchEventArgs e)
        {
            boundingCirlce.Visibility = Visibility.Hidden;
            if (e.TouchDevice.GetIsTagRecognized())
            {
                Interlocked.Decrement(ref noOfTags);
                detectedInputs.Text = " Fingers: " + noOfFingerTouches + "\nTags:" + noOfTags + "\nBlobs: " + noOfBlobs;
                tagTouchDevices.Remove(e.TouchDevice);
            }
            else if (e.TouchDevice.GetIsFingerRecognized())
            {
                Interlocked.Decrement(ref noOfFingerTouches);
                detectedInputs.Text = " Fingers: " + noOfFingerTouches + "\nTags:" + noOfTags + "\nBlobs: " + noOfBlobs;
                if (noOfFingerTouches == 0) fingerTouchDetected = false;
            }
            else
            {
                Interlocked.Decrement(ref noOfBlobs);
                detectedInputs.Text = " Fingers: " + noOfFingerTouches + "\nTags:" + noOfTags + "\nBlobs: " + noOfBlobs;
                blobTouchDevices.Remove(e.TouchDevice);
            }
            updateInputDataDisplayer();
        }
        /**
         * Displays the currently recongized input data (ONLY displays Blobs and Tags)
         * */
        private void updateInputDataDisplayer()
        {
            StringBuilder sb = new StringBuilder();
            Int16 touchDeviceCounter = 0;
            foreach (TouchDevice blob in blobTouchDevices)
            {
                touchDeviceCounter ++;
                Point p = blob.GetCenterPosition(this);
                sb.Append("Blob " + touchDeviceCounter + "   X: " + p.X + " Y: " + p.Y + "\n");
            }
            touchDeviceCounter = 0;
            foreach (TouchDevice tag in tagTouchDevices)
            {
                touchDeviceCounter ++;
                Point p = tag.GetCenterPosition(this);
                sb.Append("Tag " + touchDeviceCounter + "   X: " + p.X + " Y: " + p.Y + "\n");
            }
            inputDataDisplayer.Text = sb.ToString();
        }


        //Used to update the length visualisation and displys the bounding circle (using length as the radius)
        private void updateViews(TouchDevice callee)
        {
            // just want to know how WPF handles multi touches and events simulatenously(Observation: This states it is executed by Main: that means other threads just add the event to Main thread's queue :) )
            //Console.WriteLine("Calling Thread: " + Thread.CurrentThread + " Is background Thread? : " + Thread.CurrentThread.IsBackground); 
            //update the booleans if there are no blobs/tags
            if (noOfBlobs == 0) blobDetected = false;
            else blobDetected = true;
            if (noOfTags == 0) tagDetected = false;
            else tagDetected = true;

            Double distance = 0;

            //Drag the length displayer closer to the calling canvas's point
            lengthDisplayer.SetValue(Canvas.LeftProperty, (Double)callee.GetCenterPosition(this).X + 200);
            lengthDisplayer.SetValue(Canvas.TopProperty, (Double)callee.GetCenterPosition(this).Y + 10);

            //update length
            if (blobDetected)
            {
                TouchDevice blob = blobTouchDevices.FirstOrDefault();
                //Console.WriteLine( "Blob List SIze" + blobTouchDevices.Count + "  TAG List Size : " + tagTouchDevices.Count);
                if (noOfBlobs == 1 && noOfTags == 1)
                {
                    distance = getDistance(blobTouchDevices[0].GetCenterPosition(this), tagTouchDevices[0].GetCenterPosition(this));
                    lengthDisplayer.Text = "Blob to Tag Distance: " + distance;
                    if (!blob.Equals(callee)) displayBoundingEllipse(distance, blob.GetCenterPosition(this));
                }
                else if (noOfBlobs == 2)
                {
                    distance = getDistance(blobTouchDevices[0].GetCenterPosition(this), blobTouchDevices[1].GetCenterPosition(this));
                    lengthDisplayer.Text = "Blobs Distance: " + distance;
                    displayBoundingEllipse(distance, blobTouchDevices[0].GetCenterPosition(this));
                }
            }
            else if (tagDetected)
            {//no blobs detected
                if (noOfTags == 2)
                {
                    distance = getDistance(tagTouchDevices[0].GetCenterPosition(this), tagTouchDevices[1].GetCenterPosition(this));
                    lengthDisplayer.Text = "Tags Distance: " + distance;

                    TouchDevice bigTag;
                    //We are not interested in the moving part's tag. As we use the bigger tag for updating center of the bounding circle
                    if (tagTouchDevices[0] != callee) bigTag = tagTouchDevices[0];
                    else bigTag = tagTouchDevices[1];
                    displayBoundingEllipse(distance, bigTag.GetCenterPosition(this));
                }
            }
            else
            {
                lengthDisplayer.Text = "Input not supported.\n OR \nToo few/many inputs. ";
            }
        }
        /**
         * @radius : radius of the circle 
         * @center : This is the center point that will be used to draw the circle ?
         * */
        private void displayBoundingEllipse(Double radius, Point center){
            //boundingCirlce.Width = radius;
            //boundingCirlce.Height = radius;
            circle.Center = center;
            circle.RadiusX = radius;
            circle.RadiusY = radius;
            
            //we surely dont want to do any transformation if we want the radius to change while keeping the center consistent
            //boundingCirlce.RenderTransform = new TranslateTransform(center.X, center.Y);
            //boundingCirlce.Margin = new Thickness(center.X, center.Y, 0, 0);
            boundingCirlce.Visibility = Visibility.Visible;
        }

        private Double getDistance(Point p1, Point p2)
        {
            Double x_dist = p1.X - p2.X;
            Double y_dist = p1.Y - p2.Y;
            //Console.WriteLine(x_dist + " : " + y_dist);
            return Math.Sqrt(x_dist * x_dist + y_dist * y_dist);
        }

        private void inkCanvas_StrokeCollected(object sender, InkCanvasStrokeCollectedEventArgs e)
        {
            Stroke s = e.Stroke;
            Console.WriteLine("inkCanvas_StrokeCollected Called");
            //Left for future work 
            //You can implement the snapping feature here
            //Also note we have inkCanvas.DefaultDrawingAttributes.FitToCurve = true; inside constructor
            // You may not wanna use that
        }

        private void inkCanvasForceCapture(object sender, TouchEventArgs e)
        {
            Console.WriteLine(" inkCanvasForceCapture Called");
            inkCanvas.CaptureTouch(e.TouchDevice);
        }
    } 
}