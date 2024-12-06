using System;

namespace BandOfPearl
{
    public class Pearl
    {
        //fields
        private string? _color;
        private double _weight;

		//Construktor
		public Pearl(string color, double weight)
		{
			Color = color;
			Weight = weight;
		}

		//Properties

		/// <summary>
		/// sets the color if it is Red, Blue or Green
		/// </summary>
        public string? Color
		{
			get { return _color; }
			private set
			{
				if(value == "Red" || value == "Green" || value == "Blue")
				{
					_color = value;
				}
				else
				{
					_color = "Unknown";
				}
            }
        }

		/// <summary>
		/// sets the weight, if it is negative it is set to 0
		/// </summary>
		public double Weight
		{
			get { return _weight; }
			private set 
			{
				if(value < 0)
				{
					_weight = 0;
				}
				else
				{
					_weight = value;
				}
			}
		}

	}
}
