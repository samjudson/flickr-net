using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections.ObjectModel;

namespace FlickrNet
{
    /// <summary>
    /// The shape data supplied by <see cref="Flickr.PlacesGetInfo"/>.
    /// </summary>
    /// <remarks>
    /// See <a href="http://code.flickr.com/blog/2008/10/30/the-shape-of-alpha/">http://code.flickr.com/blog/2008/10/30/the-shape-of-alpha/</a> for more details.
    /// </remarks>
    public sealed class ShapeData : IFlickrParsable
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ShapeData()
        {
            PolyLines = new Collection<Collection<PointD>>();
            Urls = new Collection<Uri>();
        }

        /// <summary>
        /// The date the shapedata was created.
        /// </summary>
        public DateTime DateCreated { get; private set; }

        /// <summary>
        /// The alpha for the shape data.
        /// </summary>
        public double Alpha { get; private set; }

        /// <summary>
        /// The number of points in the shapefile.
        /// </summary>
        public int PointCount { get; private set; }

        /// <summary>
        /// The number of edge in the shapefile.
        /// </summary>
        public int EdgeCount { get; private set; }

        /// <summary>
        /// Does the shape have a donut hole.
        /// </summary>
        public bool HasDonutHole { get; private set; }

        /// <summary>
        /// Is the shape a donut hole.
        /// </summary>
        public bool IsDonutHole { get; private set; }

        /// <summary>
        /// A list of polylines making up the shape. Each polyline is itself a list of points.
        /// </summary>
        public Collection<Collection<PointD>> PolyLines { get; private set; }

        /// <summary>
        /// A list of urls for the shapefiles.
        /// </summary>
        public Collection<Uri> Urls { get; private set; }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "created":
                        DateCreated = UtilityMethods.UnixTimestampToDate(reader.Value);
                        break;
                    case "alpha":
                        Alpha = reader.ReadContentAsDouble();
                        break;
                    case "count_points":
                        PointCount = reader.ReadContentAsInt();
                        break;
                    case "count_edges":
                        EdgeCount = reader.ReadContentAsInt();
                        break;
                    case "has_donuthole":
                        HasDonutHole = reader.Value == "1";
                        break;
                    case "is_donuthole":
                        IsDonutHole = reader.Value == "1";
                        break;
                    default:
                        throw new ParsingException("Unknown attribute value: " + reader.LocalName + "=" + reader.Value);
                }
            }

            reader.Read();

            while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
            {
                switch (reader.LocalName)
                {
                    case "polylines":
                        reader.Read();
                        while (reader.LocalName == "polyline")
                        {
                            Collection<PointD> polyline = new Collection<PointD>();
                            string polystring = reader.ReadElementContentAsString();
                            string[] points = polystring.Split(' ');
                            foreach (string point in points)
                            {
                                string[] xy = point.Split(',');
                                if (xy.Length != 2) throw new ParsingException("Invalid polypoint found in polyline : '" + polystring + "'");
                                polyline.Add(new PointD(double.Parse(xy[0], System.Globalization.NumberFormatInfo.InvariantInfo), double.Parse(xy[1], System.Globalization.NumberFormatInfo.InvariantInfo)));
                            }
                            PolyLines.Add(polyline);
                        }
                        reader.Read();
                        break;
                    case "urls":
                        reader.Skip();
                        break;

                }
            }
        }
    }

    /// <summary>
    /// A point structure for holding double-floating points precision data.
    /// </summary>
    public struct PointD
    {
        private double _x;
        private double _y;

        /// <summary>
        /// The X position of the point.
        /// </summary>
        public double X { get { return _x; } private set { _x = value; } }

        /// <summary>
        /// The Y position of the point.
        /// </summary>
        public double Y { get { return _y; } private set { _y = value; } }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public PointD(double x, double y)
        {
            _x = x;
            _y = y;
        }
    }
}


