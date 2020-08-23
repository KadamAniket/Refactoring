using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring_MartinFowler.Chapter_1.Step1
{
    /* Extract Method: Extract code into its own method
       Fragment for any variables that are local in scope to the method
       Any nonmodified variable I can pass in as a parameter.
       Modified variables need more care. If there is only one, I can return it
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
            int count = 0;
            
                String result = "Rental Record for " + getName() + "\n";
            while (count < _rentals.Count)
            {
                double thisAmount = 0;
                Rental each = (Rental)_rentals[count];

                thisAmount = amountFor(each);

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
            }
            //add footer lines
            result += "Amount owed is " + Convert.ToString(totalAmount) +
           "\n";
            result += "You earned " + Convert.ToString(frequentRenterPoints)
           +
           " frequent renter points";
            return result;
        }

        private double amountFor(Rental aRental)
        {
            double result = 0;
            switch (aRental.getMovie().getPriceCode())
            {
                case Movie.REGULAR:
                    result += 2;
                    if (aRental.getDaysRented() > 2)
                        result += (aRental.getDaysRented() - 2) * 1.5;
                    break;
                case Movie.NEW_RELEASE:
                    result += aRental.getDaysRented() * 3;
                    break;
                case Movie.CHILDRENS:
                    result += 1.5;
                    if (aRental.getDaysRented() > 3)
                        result += (aRental.getDaysRented() - 3) * 1.5;
                    break;
            }
            return result;
        }
    }

}
