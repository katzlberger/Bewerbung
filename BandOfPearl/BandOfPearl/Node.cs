using System;

namespace BandOfPearl
{
    class Node
    {
        //fields
        private Pearl? _pearl;
        private Node? _next;

        //Properties
        /// <summary>
        /// propertie next Node
        /// </summary>
        public Node? Next
        {
            get { return _next; }
            set { _next = value; }
        }

        /// <summary>
        /// propertie pearl
        /// </summary>
        public Pearl? Pearl
        {
            get { return _pearl; }
            set { _pearl = value; }
        }
    }
}
