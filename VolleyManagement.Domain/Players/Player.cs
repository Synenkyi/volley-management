﻿namespace VolleyManagement.Domain.Players
{
    using System;
    using VolleyManagement.Domain.Properties;

    /// <summary>
    /// Player domain class.
    /// </summary>
    public class Player
    {
        private string _firstName;
        private string _lastName;
        private int _birthYear;
        private int _height;
        private int _weight;

        /// <summary>
        /// Gets or sets a value indicating where Id.
        /// </summary>
        /// <value>Id of player.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating where FirstName.
        /// </summary>
        /// <value>First name.</value>
        public string FirstName
        {
            get
            {
                return _firstName;
            }

            set
            {
                if (PlayerValidation.ValidateFirstName(value))
                {
                    throw new ArgumentException(Resources.ValidationPlayerFirstName);
                }

                _firstName = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating where LastName.
        /// </summary>
        /// <value>Last name.</value>
        public string LastName
        {
            get
            {
                return _lastName;
            }

            set
            {
                if (PlayerValidation.ValidateLastName(value))
                {
                    throw new ArgumentException(Resources.ValidationPlayerLastName);
                }

                _lastName = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating where BirthYear.
        /// </summary>
        /// <value>Birth year.</value>
        public int BirthYear
        {
            get
            {
                return _birthYear;
            }

            set
            {
                if (PlayerValidation.ValidateBirthYear(value))
                {
                    throw new ArgumentException(Resources.ValidationPlayerBirthYear);
                }

                _birthYear = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating where Height.
        /// </summary>
        /// <value>The height.</value>
        public int Height
        {
            get
            {
                return _height;
            }

            set
            {
                if (PlayerValidation.ValidateHeight(value))
                {
                    throw new ArgumentException(Resources.ValidationPlayerHeight);
                }

                _height = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating where Weight.
        /// </summary>
        /// <value>The weight.</value>
        public int Weight
        {
            get
            {
                return _weight;
            }

            set
            {
                if (PlayerValidation.ValidateWeight(value))
                {
                    throw new ArgumentException(Resources.ValidationPlayerWeight);
                }

                _weight = value;
            }
        }
    }
}
