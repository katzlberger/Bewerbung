using System;
using System.Drawing;

namespace BandOfPearl.Logic
{
    public class Band
    {
        //fields
        private Node? _head;
        private int _count;

        /// <summary>
        /// read-only propertie - count
        /// </summary>
        public int Count
        {
            get { return _count; }
        }

        //Methods

        /// <summary>
        /// add a Pearl on the first position
        /// </summary>
        /// <param name="newPearl"></param>
        public void AddPearl(Pearl newPearl)
        {
            Node newNode = new Node();
            newNode.Pearl = newPearl;

            if(newPearl != null)
            {
                if (_head == null)
                {
                    _head = newNode;
                }
                else
                {
                    newNode.Next = _head;
                    _head = newNode;
                }
                _count++;
            }
        }

        /// <summary>
        /// gives back a Pearl at a spezific position
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public Pearl? GetPearlAtPosition(int pos)
        {
            if (pos < 0)
            {
                return null;
            }
            int actPos = 0;
            Node? actNode = _head;
            while(actNode != null && actPos < pos)
            {
                actNode = actNode.Next;
                actPos++;
            }

            if (actNode != null)
            {
                return actNode.Pearl;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// counts the number of the same colored pearls
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public int GetNumberOfColoredPearls(string color)
        {
            Node? actNode = _head;
            int countSameColor = 0;

            while (actNode != null)
            {
                if(actNode?.Pearl?.Color == color)
                {
                    countSameColor++;
                }
                actNode = actNode?.Next;
            }

            return countSameColor;
        }

        /// <summary>
        /// removes the first pearl and give the removed pearl back
        /// </summary>
        /// <returns></returns>
        public Pearl? RemovePearl()
        {
            if (_head == null)
            {
                return null;
            }
            Pearl removePearl = new Pearl(_head.Pearl.Color, _head.Pearl.Weight);
            
            _head = _head.Next;
            _count--;

            return removePearl;
        }

        /// <summary>
        /// gets the total weight of the band
        /// </summary>
        /// <returns></returns>
        public double GetTotalWeight()
        {
            Node? actNode = _head;
            double sumWeight = 0;

            while (actNode != null)
            {
                sumWeight += actNode.Pearl.Weight;

                actNode = actNode.Next;
            }
            return sumWeight;
        }  
    }
}
