using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring_MartinFowler.Chapter_1.Step6
{
    /* Adding htmlStatments
     * 
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
            String result = "Rental Record for " + getName() + "\n";
            int count = 0;
            while (count < _rentals.Count)
            {
                Rental each = (Rental)_rentals[count];

                //show figures for this rental
                result += "\t" + each.getMovie().getTitle() + "\t" + Convert.ToString(each.getCharge()) + "\n";

                count++;
            }
            //add footer lines
            result += "Amount owed is " + Convert.ToString(getTotalCharge()) + "\n";
            result += "You earned " + Convert.ToString(getTotalRenterPoints()) + " frequent renter points";
            return result;
        }

        public string htmlStatement()
        {
            string result = "<H1>Rentals for <EM>" + getName() + "</EM></H1 >< P >\n";
            int count = 0;
            while (count < _rentals.Count)
            {
                Rental each = (Rental)_rentals[count];

                //show figures for each rental
                result += each.getMovie().getTitle() + ": " +
                Convert.ToString(each.getCharge()) + "<BR>\n";
            }
            //add footer lines
            result += "<P>You owe <EM>" + Convert.ToString(getTotalCharge()) + "</EM><P>\n";
            result += "On this rental you earned <EM>" + Convert.ToString(getTotalRenterPoints()) + "</EM> frequent renter points<P>";
            return result;
        }

        private double getTotalCharge()
        {
            double result = 0;
            int count = 0;
            while (count < _rentals.Count)
            {
                Rental each = (Rental)_rentals[count];
                result += each.getCharge();

                count++;
            }
            return result;
        }

        private double getTotalRenterPoints()
        {
            double result = 0;
            int count = 0;
            while (count < _rentals.Count)
            {
                Rental each = (Rental)_rentals[count];
                result += each.getFrequentRenters();

                count++;
            }
            return result;
        }

    }
}
