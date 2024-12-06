using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MobileLibrary
{
    /// <summary>
    /// Simulation of a simple mobile phone
    /// </summary>
    public class Mobile
    {
        #region fields
        /// <summary>
        /// Fields
        /// </summary>
        /// 

        private string _name = string.Empty;
        private string _phoneNumber = string.Empty;
        private string _lastCalledNumber = string.Empty;
        private int _secondsActive;
        private int _secondsPassive;
        private int _centsToPay;
        private bool _isInCall;
        private bool _lastCallIsActive;
        private Mobile  _callPartner;
        private DateTime _lastStartTime;
        #endregion fields
        #region constructors
        /// <summary>
        /// Constructors
        /// </summary>
        public Mobile(string phoneNumber):this(phoneNumber, ""){}

        public Mobile(string phoneNumber, string name) 
        {
           PhoneNumber = phoneNumber;
            Name = name;
        }
        #endregion constuctors
        #region properies
        /// <summary>
        /// Properties
        /// </summary>

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            private set { _phoneNumber = value; }
        }

        public string LastCalledNumber
        {
            get { return _lastCalledNumber; }
            private set { _lastCalledNumber = value; }
        }

        public int SecondsActive
        {
            get { return _secondsActive; }
        }

        public int SecondsPassive
        {
            get { return _secondsPassive; }
        }

        /// <summary>
        /// calculates the cents you have to pay for your calling
        /// </summary>
        public int CentsToPay
        {
            get
            {
                int seconds = _secondsActive;
                if (!_lastCallIsActive)
                {
                    return _centsToPay;
                }
                while (seconds > 0)
                {
                    int temp = seconds / 60;
                    if (temp > 30)
                    {
                        _centsToPay += 8;
                        seconds -= 60;
                    }
                    else
                    {
                        _centsToPay += 4;
                        seconds -= 30;
                    }
                }

                return _centsToPay;

            }
        }

        /// <summary>
        /// looks if you are in a call
        /// </summary>
        public bool IsInCall
        {
            get
            {
                return _isInCall;
            }
        }

        /// <summary>
        /// With Errorhandling (see taskdescription)
        /// </summary>
        public string Name
        {
            get { return _name; }
            private set
            {
                if(value.Length < 2 || (value[0] < '9' && value[0] >'0'))
                {
                    _name = "ERROR";
                }
                else
                {
                    
                    _name = value;
                }
            }
        }
        #endregion properties
        #region methods

        /// <summary>
        /// Mobile initiates a call to a passive mobile phone. Time starts counting
        /// for both mobiles.
        /// </summary>
        /// <param name="passive">passive mobile</param>
        /// <returns>Returns true when phone call started correctly. False when active or passive phone is already busy (already talking).</returns>
        public bool StartCallTo(Mobile passive)
        {
            if(passive._isInCall || _isInCall)
            {
                return false;
            }
            _callPartner = passive;
            _lastCallIsActive = true;
            _isInCall = true;
            _lastCalledNumber = passive._phoneNumber;
            _lastStartTime = DateTime.Now;
            passive.StartCallFrom(this);
            return true;
        }

        /// <summary>
        /// Starts the call for the passive mobile
        /// </summary>
        /// <param name="other"></param>
        private bool StartCallFrom(Mobile other)
        {
            if(_isInCall || _isInCall)
            {
                return false;
            }
            _isInCall = true;
            _callPartner = other;
            _lastCallIsActive = false;
            _lastCalledNumber = other._phoneNumber;
            _lastStartTime = DateTime.Now;
            other.StartCallTo(this);
            return true;
        }

        /// <summary>
        /// End the call and also the call by the other mobile. Calculate duration and
        /// by the active caller the costs of the call.
        /// </summary>
        /// <returns>false, if there is no call pending</returns>
        public bool StopCall()
        {
            int seconds = (int)((DateTime.Now - _lastStartTime).TotalSeconds * 20);
            if(seconds <= 0 || _callPartner == null)
            { return false; }

            _isInCall = false;
            _callPartner._isInCall = false;
            if (_lastCallIsActive)
            {
                _secondsActive += seconds;
                _callPartner._secondsPassive += seconds;
            }
            _secondsPassive += seconds;
            _callPartner._secondsActive += seconds;


            return true;
        }
        #endregion methods

    }
}
