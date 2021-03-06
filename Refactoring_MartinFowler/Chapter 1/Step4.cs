﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring_MartinFowler.Chapter_1.Step4
{
    /*  Extracting getFrequentRenderPoints
     *  We look at the use of locally scoped variables. Again each is used and can be passed in
        as a parameter. The other temp used is frequentRenterPoints.
        In this case frequentRenterPoints does have a value beforehand. The body of the extracted method doesn't read the value, however,
        so we don't need to pass it in as a parameter as long as we use an appending assignment.
       * */

    public class Movie
    {
        public const int CHILDRENS = 2;
        public const int REGULAR = 0;
        public const int NEW_RELEASE = 1;
        private String _title;
        private int _priceCode;
        public Movie(String title, int priceCode)
        {
            _title = title;
            _priceCode = priceCode;
        }
        public int getPriceCode()
        {
            return _priceCode;
        }
        public void setPriceCode(int arg)
        {
            _priceCode = arg;
        }
        public String getTitle()
        {
            return _title;
        }
    }

    class Rental
    {
        private Movie _movie;
        private int _daysRented;
        public Rental(Movie movie, int daysRented)
        {
            _movie = movie;
            _daysRented = daysRented;
        }
        public int getDaysRented()
        {
            return _daysRented;
        }
        public Movie getMovie()
        {
            return _movie;
        }

        public double getCharge()
        {
            double result = 0;
            switch (getMovie().getPriceCode())
            {
                case Movie.REGULAR:
                    result += 2;
                    if (getDaysRented() > 2)
                        result += (getDaysRented() - 2) * 1.5;
                    break;
                case Movie.NEW_RELEASE:
                    result += getDaysRented() * 3;
                    break;
                case Movie.CHILDRENS:
                    result += 1.5;
                    if (getDaysRented() > 3)
                        result += (getDaysRented() - 3) * 1.5;
                    break;
            }
            return result;
        }

        public int getFrequentRenters()
        {
            if ((getMovie().getPriceCode() == Movie.NEW_RELEASE) && getDaysRented() > 1)
                return 2;
            else return 1;
        }
    }

    class Customer
    {
        private String _name;
        private ArrayList _rentals = new ArrayList();
        public Customer(String name)
        {
            _name = name;
        }
        public void addRental(Rental arg)
        {
            _rentals.Add(arg);
        }
        public String getName()
        {
            return _name;
        }

        public String statement()
        {
            double totalAmount = 0;
            int frequentRenterPoints = 0;
            String result = "Rental Record for " + getName() + "\n";
            int count = 0;
            while (count < _rentals.Count)
            {
                Rental each = (Rental)_rentals[count];

                frequentRenterPoints = each.getFrequentRenters();

                //show figures for this rental
                result += "\t" + each.getMovie().getTitle() + "\t" +
               Convert.ToString(each.getCharge()) + "\n";
                totalAmount += each.getCharge();

                count++;
            }
            //add footer lines
            result += "Amount owed is " + Convert.ToString(totalAmount) +
           "\n";
            result += "You earned " + Convert.ToString(frequentRenterPoints)
           +
           " frequent renter points";
            return result;
        }

    }
}
