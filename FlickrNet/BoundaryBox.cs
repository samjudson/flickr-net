using System;

namespace FlickrNet
{

	/// <summary>
	/// Summary description for BoundaryBox.
	/// </summary>
	public class BoundaryBox
	{
		private GeoAccuracy _accuracy = GeoAccuracy.Street;
		private bool _isSet;

		private double _minimumLat = -90;
		private double _minimumLon = -180;
		private double _maximumLat = 90;
		private double _maximumLon = 180;

		/// <summary>
		/// Gets weither the boundary box has been set or not.
		/// </summary>
		internal bool IsSet
		{
			get { return _isSet; }
		}

		/// <summary>
		/// The search accuracy - optional. Defaults to <see cref="GeoAccuracy.Street"/>.
		/// </summary>
		public GeoAccuracy Accuracy
		{
			get { return _accuracy; }
			set { _accuracy = value; }
		}

		/// <summary>
		/// The minimum latitude of the boundary box, i.e. bottom left hand corner.
		/// </summary>
		public double MinimumLatitude
		{
			get { return _minimumLat; }
			set 
			{
				if( value < -90 || value > 90 )
                    throw new ArgumentOutOfRangeException("value", "Must be between -90 and 90");
				_isSet = true; _minimumLat = value; 
			}
		}

		/// <summary>
		/// The minimum longitude of the boundary box, i.e. bottom left hand corner. Range of -180 to 180 allowed.
		/// </summary>
		public double MinimumLongitude
		{
			get { return _minimumLon; }
			set { 
				if( value < -180 || value > 180 )
                    throw new ArgumentOutOfRangeException("value", "Must be between -180 and 180");
				_isSet = true; _minimumLon = value; 
			}
		}

		/// <summary>
		/// The maximum latitude of the boundary box, i.e. top right hand corner. Range of -90 to 90 allowed.
		/// </summary>
		public double MaximumLatitude
		{
			get { return _maximumLat; }
			set 
			{
				if( value < -90 || value > 90 )
                    throw new ArgumentOutOfRangeException("value", "Must be between -90 and 90");
				_isSet = true; _maximumLat = value; 
			}
		}

		/// <summary>
		/// The maximum longitude of the boundary box, i.e. top right hand corner. Range of -180 to 180 allowed.
		/// </summary>
		public double MaximumLongitude
		{
			get { return _maximumLon; }
			set 
			{
				if( value < -180 || value > 180 )
                    throw new ArgumentOutOfRangeException("value", "Must be between -180 and 180");
				_isSet = true; _maximumLon = value; 
			}
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public BoundaryBox()
		{
		}

		/// <summary>
		/// Default constructor, specifying only the accuracy level.
		/// </summary>
		/// <param name="accuracy">The <see cref="GeoAccuracy"/> of the search parameter.</param>
		public BoundaryBox(GeoAccuracy accuracy)
		{
			_accuracy = accuracy;
		}

		/// <summary>
		/// Constructor for the <see cref="BoundaryBox"/>
		/// </summary>
		/// <param name="points">A comma seperated list of co-ordinates defining the boundary box.</param>
		/// <param name="accuracy">The <see cref="GeoAccuracy"/> of the search parameter.</param>
		public BoundaryBox(string points, GeoAccuracy accuracy) : this(points)
		{
			_accuracy = accuracy;
		}

		/// <summary>
		/// Constructor for the <see cref="BoundaryBox"/>
		/// </summary>
		/// <param name="points">A comma seperated list of co-ordinates defining the boundary box.</param>
		public BoundaryBox(string points)
		{
			if( points == null ) throw new ArgumentNullException("points");

			string[] splits = points.Split(',');

			if( splits.Length != 4 )
				throw new ArgumentException("Parameter must contain 4 values, seperated by commas.", "points");

			try
			{
				MinimumLongitude = double.Parse(splits[0],System.Globalization.NumberFormatInfo.InvariantInfo);
				MinimumLatitude = double.Parse(splits[1],System.Globalization.NumberFormatInfo.InvariantInfo);
				MaximumLongitude = double.Parse(splits[2],System.Globalization.NumberFormatInfo.InvariantInfo);
				MaximumLatitude = double.Parse(splits[3],System.Globalization.NumberFormatInfo.InvariantInfo);
			}
			catch(FormatException)
			{
				throw new ArgumentException("Unable to parse points as integer values", "points");
			}
		}

		/// <summary>
		/// Constructor for the <see cref="BoundaryBox"/>.
		/// </summary>
		/// <param name="minimumLongitude">The minimum longitude of the boundary box. Range of -180 to 180 allowed.</param>
		/// <param name="minimumLatitude">The minimum latitude of the boundary box. Range of -90 to 90 allowed.</param>
		/// <param name="maximumLongitude">The maximum longitude of the boundary box. Range of -180 to 180 allowed.</param>
		/// <param name="maximumLatitude">The maximum latitude of the boundary box. Range of -90 to 90 allowed.</param>
		/// <param name="accuracy">The <see cref="GeoAccuracy"/> of the search parameter.</param>
		public BoundaryBox(double minimumLongitude, double minimumLatitude, double maximumLongitude, double maximumLatitude, GeoAccuracy accuracy) : this(minimumLongitude, minimumLatitude, maximumLongitude, maximumLatitude)
		{
			_accuracy = accuracy;
		}

		/// <summary>
		/// Constructor for the <see cref="BoundaryBox"/>.
		/// </summary>
		/// <param name="minimumLongitude">The minimum longitude of the boundary box. Range of -180 to 180 allowed.</param>
		/// <param name="minimumLatitude">The minimum latitude of the boundary box. Range of -90 to 90 allowed.</param>
		/// <param name="maximumLongitude">The maximum longitude of the boundary box. Range of -180 to 180 allowed.</param>
		/// <param name="maximumLatitude">The maximum latitude of the boundary box. Range of -90 to 90 allowed.</param>
		public BoundaryBox(double minimumLongitude, double minimumLatitude, double maximumLongitude, double maximumLatitude)
		{
			MinimumLatitude = minimumLatitude;
			MinimumLongitude = minimumLongitude;
			MaximumLatitude = maximumLatitude;
			MaximumLongitude = maximumLongitude;
		}

		/// <summary>
		/// Overrides the ToString method.
		/// </summary>
		/// <returns>A comma seperated list of co-ordinates defining the boundary box.</returns>
		public override string ToString()
		{
			return String.Format(System.Globalization.NumberFormatInfo.InvariantInfo, "{0},{1},{2},{3}", MinimumLongitude, MinimumLatitude, MaximumLongitude, MaximumLatitude);
		}

        /// <summary>
        /// Calculates the distance in miles from the SW to the NE corners of this boundary box.
        /// </summary>
        /// <returns></returns>
        public double DiagonalDistanceInMiles()
        {
            return DiagonalDistance() * 3963.191;
        }

        /// <summary>
        /// Calculates the distance in kilometres from the SW to the NE corners of this boundary box.
        /// </summary>
        /// <returns></returns>
        public double DiagonalDistanceInKM()
        {
            return DiagonalDistance() * 6378.137;
        }

        private double DiagonalDistance()
        {
            double latRad1 = MinimumLatitude / 180.0 * Math.PI;
            double latRad2 = MaximumLatitude / 180.0 * Math.PI;
            double lonRad1 = MinimumLongitude / 180.0 * Math.PI;
            double lonRad2 = MaximumLongitude / 180.0 * Math.PI;

            double e = Math.Acos(Math.Sin(latRad1) * Math.Sin(latRad2) + Math.Cos(latRad1) * Math.Cos(latRad2) * Math.Cos(lonRad2 - lonRad1));
            return e;
        }

		/// <summary>
		/// Example boundary box for the UK.
		/// </summary>
		public static BoundaryBox UK
		{
			get { return new BoundaryBox(-11.118164, 49.809632, 1.625977, 62.613562); }
		}

		/// <summary>
		/// Example boundary box for Newcastle upon Tyne, England.
		/// </summary>
		public static BoundaryBox UKNewcastle
		{
			get { return new BoundaryBox(-1.71936, 54.935821, -1.389771, 55.145919); }
		}

		/// <summary>
		/// Example boundary box for the USA (excludes Hawaii and Alaska).
		/// </summary>
		public static BoundaryBox Usa
		{
			get { return new BoundaryBox(-130.429687, 22.43134, -58.535156, 49.382373); }
		}

		/// <summary>
		/// Example boundary box for Canada.
		/// </summary>
		public static BoundaryBox Canada
		{
			get { return new BoundaryBox(-143.085937, 41.640078, -58.535156, 73.578167); }
		}

		/// <summary>
		/// Example boundary box for the whole world.
		/// </summary>
		public static BoundaryBox World
		{
			get { return new BoundaryBox(-180, -90, 180, 90); }
		}

	}
}
