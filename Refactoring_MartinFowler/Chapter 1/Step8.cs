using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring_MartinFowler.Chapter_1.Step8
{
    /* Replacing the Conditional Logic on Price Code with Polymorphism
     * At last … Inheritance.We have several types of movie that have different ways of answering the same question. This
        sounds like a job for subclasses. We can have three subclasses of movie, each of which can have its own version of charge.
        A movie can change its classification during its lifetime. An object cannot change its class during its lifetime.
        We can do the subclassing from the price code object and change the price whenever we need to.
      * */

    public class Movie
    {
        public const int CHILDRENS = 2;
        public const int REGULAR = 0;
        public const int NEW_RELEASE = 1;
        private String _title;
        private Price _price;
        public Movie(String title, int priceCode)
        {
            _title = title;
            setPriceCode(priceCode);
        }
        public int getPriceCode()
        {
            return _price.getPriceCode();
        }
        public void setPriceCode(int arg)
        {
            switch (arg)
            {
                case REGULAR:
                    _price = new RegularPrice();
                    break;
                case CHILDRENS:
                    _price = new ChildrensPrice();
                    break;
                case NEW_RELEASE:
                    _price = new NewReleasePrice();
                    break;
                default:
                    throw new Exception("Incorrect Price Code");
            }
        }
        public String getTitle()
        {
            return _title;
        }

        public double getCharge(int daysRented)
        {
            double result = 0;
            result = _price.getCharge(daysRented);
            return result;
        }

        public int getFrequentRentersPoint(int daysRented)
        {
            return _price.getFrequentRentersPoint(daysRented);
        }
    }
    public abstract class Price
    {
        public abstract int getPriceCode();

        public abstract double getCharge(int daysRented);

        public virtual int getFrequentRentersPoint(int daysRented)
        {
            return 1;
        }

    }
    public class ChildrensPrice : Price
    {
        public override int getPriceCode()
        {
            return Movie.CHILDRENS;
        }

        public override double getCharge(int daysRented)
        {
            double result = 1.5;
            if (daysRented > 3)
                result += (daysRented - 3) * 1.5;
            return result;
        }

    }
    class NewReleasePrice : Price
    {
        public override int getPriceCode()
        {
            return Movie.NEW_RELEASE;
        }

        public override double getCharge(int daysRented)
        {
            return daysRented * 3;
        }

        public override int getFrequentRentersPoint(int daysRented)
        {
            if (daysRented > 1)
                return 2;
            else return 1;
        }
    }
    class RegularPrice : Price
    {
        public override int getPriceCode()
        {
            return Movie.REGULAR;
        }

        public override double getCharge(int daysRented)
        {
            double result = 2;
            if (daysRented > 2)
                result += (daysRented - 2) * 1.5;
            return result;
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
