using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring_MartinFowler.Chapter_1.Step7
{
    /* Replacing the Conditional Logic on Price Code with Polymorphism
     * The first part of this problem is that switch statement. It is a bad idea to do a switch based on an attribute of another object.
     *  If you must use a switch statement, it should be on your own data, not on someone else's.
     *  This implies that getCharge should move onto movie:
     *  
     *  The method effectively uses two pieces of data, the length of the rental and the type of the movie.
        Why do I prefer to pass the length of rental to the movie rather than the movie type to the rental?
        It's because the proposed changes are all about adding new types. Type information generally tends to be more volatile. 
        If I change the movie type, I want the least ripple effect, so I prefer to calculate the charge within the movie.
        At last … Inheritance.We have several types of movie that have different ways of answering the same question. This
        sounds like a job for subclasses. We can have three subclasses of movie, each of which can have its own version of charge
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

        public double getCharge(int daysRented)
        {
            double result = 0;
            switch (getPriceCode())
            {
                case REGULAR:
                    result += 2;
                    if (daysRented > 2)
                        result += (daysRented - 2) * 1.5;
                    break;
                case NEW_RELEASE:
                    result += daysRented * 3;
                    break;
                case CHILDRENS:
                    result += 1.5;
                    if (daysRented > 3)
                        result += (daysRented - 3) * 1.5;
                    break;
            }
            return result;
        }

        public int getFrequentRentersPoint(int daysRented)
        {
            if (getPriceCode() == NEW_RELEASE && daysRented > 1)
                return 2;
            else return 1;
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
            return _movie.getCharge(_daysRented);
        }

        public int getFrequentRenters()
        {
            return _movie.getFrequentRentersPoint(_daysRented);
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
