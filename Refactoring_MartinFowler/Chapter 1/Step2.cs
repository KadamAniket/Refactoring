using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring_MartinFowler.Chapter_1
{
    /*  Move method to its expected class
        In most cases a method should be on the object whose data it uses
        In this case fitting into its new home means removing the parameter
        I leave the old method to delegate to the new method. This is useful if it is a public method and I don't want to change the interface of the other class.
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
                double thisAmount = 0;
                Rental each = (Rental)_rentals[count];

                thisAmount = each.getCharge();

                // add frequent renter points
                frequentRenterPoints++;
                // add bonus for a two day new release rental
                if ((each.getMovie().getPriceCode() == Movie.NEW_RELEASE)
               &&
               each.getDaysRented() > 1) frequentRenterPoints++;
                //show figures for this rental
                result += "\t" + each.getMovie().getTitle() + "\t" +
               Convert.ToString(thisAmount) + "\n";
                totalAmount += thisAmount;

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
