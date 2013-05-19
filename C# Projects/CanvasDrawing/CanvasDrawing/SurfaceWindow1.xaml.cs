using System;
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
using Microsoft.Surface;
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;
using Microsoft.Surface.Presentation.Input;
using System.Windows.Ink;
/**
 * Author: Kapil
 * This app Demonstrates the following:
 * -How to draw a complex shape using Path
 * -How to use ink canvas and do changes to the drawn strokes
 * */
namespace CanvasDrawing
{
    /// <summary>
    /// Interaction logic for SurfaceWindow1.xaml
    /// </summary>
    public partial class SurfaceWindow1 : SurfaceWindow
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public SurfaceWindow1()
        {
            InitializeComponent();
            Path myShape = new Path();
            myShape.StrokeThickness = 3.0;
            myShape.Fill = System.Windows.Media.Brushes.Wheat;
            myShape.Stroke = System.Windows.Media.Brushes.BlueViolet;
            PathGeometry myGeometry = new PathGeometry();
            PathFigure figure = new PathFigure();
            
            //Figure draws the segments upside down (this the coordinate system in negatives to draw objects)
            Double width = 200;
            Double height = 100;
            figure.SetValue(PathFigure.StartPointProperty, new Point(height, 0));
            ArcSegment arc = new ArcSegment(new Point(height, height), new Size(height / 2, height / 2), 0.0, true, SweepDirection.Counterclockwise, true);
            //Note: LineSegments take end point as the constructor, Their Start point will be the end point of previous segment(the order you added into path figure(Source API))
            LineSegment arcVertical1 = new LineSegment(new Point(height, height - 25), true);
            LineSegment horizontal1 = new LineSegment(new Point(height + 275, height - 25), true);
            LineSegment vertical = new LineSegment(new Point(height + 275, 25), true);
            LineSegment horizontal2 = new LineSegment(new Point(height, 25), true);
            LineSegment arcVertical2 = new LineSegment(new Point(height, 0), true);

            figure.Segments.Add(arc);
            figure.Segments.Add(arcVertical1);
            figure.Segments.Add(horizontal1);
            figure.Segments.Add(vertical);
            figure.Segments.Add(horizontal2);
            figure.Segments.Add(arcVertical2);
            myGeometry.Figures.Add(figure);

            myShape.Data = myGeometry;
            myCanvas.Children.Add(myShape);


            //****************************** Register for Stroke change events*************************
            // This is how we can get the currently being drawn stroke information
            inkCanvas.Strokes.StrokesChanged += new StrokeCollectionChangedEventHandler(canvasStrokesChanged);

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

        private void canvasMouseDown(object sender, MouseButtonEventArgs e)
        {
            //Console.WriteLine("Mouse Down Event");
        }

        private void canvasMouseMove(object sender, MouseEventArgs e)
        {

            //Console.WriteLine("Mouse Move Event");
           

        }

        private void canvasMouseUp(object sender, MouseButtonEventArgs e)
        {
            //Console.WriteLine("Mouse Up Event");
        }

        /**
         * This method is called as you start drawing, for each change 
         * */
        private void canvasStrokesChanged(object sender, StrokeCollectionChangedEventArgs e)
        {

            if (sender as StrokeCollection != null)
            {
                StrokeCollection strokes = (StrokeCollection)sender;
                
                for(int i=0; i<strokes.Count ; i++){
                    Console.WriteLine();
                    Stroke s = strokes[i];
                    //ensures we don't end up hooking our event handler multiple times :)
                    s.StylusPointsChanged -= new EventHandler(s_StylusPointsChanged);
                    s.StylusPointsChanged += new EventHandler(s_StylusPointsChanged);
                }
            }
        }
        TextBlock t;
        /**
         * This is called when the user finishes drawing a stroke.
         * */
        private void strokeCollected(object sender, InkCanvasStrokeCollectedEventArgs e)
        {
            //Console.WriteLine("Stroke Collected");
            Stroke s = e.Stroke;
            if(s.StylusPoints.Count < 20){
                inkCanvas.Strokes.Remove(s);
            }
            Double firstX = s.StylusPoints[0].X;
            Double firstY = s.StylusPoints[0].Y;
            Point first = new Point(firstX,firstY);
            
            Double lastX= s.StylusPoints[s.StylusPoints.Count-1].X ;
            Double lastY = s.StylusPoints[s.StylusPoints.Count-1].Y ;
            Point last = new Point(lastX,lastY);

            Polyline p = new Polyline();
            p.Stroke = System.Windows.Media.Brushes.Cornsilk;
            p.StrokeThickness = 3.5;

            PointCollection pc = new PointCollection();
            pc.Add(first);
            pc.Add(last);

            p.Points = pc;
            inkCanvas.Strokes.Remove(s);
            myCanvas.Children.Add(p);

        }
        /**
         * Called as the user makes changes to stroke (i.e. called as the user draws stroke)
         * */
        private void s_StylusPointsChanged(object sender, EventArgs e)
        {
            Stroke s = (Stroke)sender;
            if (t == null)
            {
                t = new TextBlock();
                t.FontSize = 20;
                myCanvas.Children.Add(t);
            }

            Double x= s.StylusPoints[s.StylusPoints.Count-1].X ;
            Double y = s.StylusPoints[s.StylusPoints.Count-1].Y ;

            String text = " X: " +  x.ToString() + " Y: " + y.ToString();
            t.Text = text;
        }
    }
}