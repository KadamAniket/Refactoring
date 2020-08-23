using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring_MartinFowler.Chapter_1.Step5
{

    /* Removing temps
     * I like to use Replace Temp with Query to replace totalAmount and frequentRentalPoints with query methods.
       Queries are accessible to any method in the class and thus encourage a cleaner design without long, complex methods
       I began by replacing totalAmount with a charge method on customer
       Most refactorings reduce the amount of code, but this one increases it.
       The other concern with this refactoring lies in performance. The old code executed the "while" loop once, the new code executes it three times.
       A while loop that takes a long time might impair performance. Many programmers would not do this refactoring simply for this reason. But note
        the words if and might. Until I profile I cannot tell how much time is needed for the loop to calculate or whether the loop is called often enough for it to affect the overall performance of the
        system. Don't worry about this while refactoring. When you optimize you will have to worry about it, but you will then be in a much better position to do something about it, and you will have more
        options to optimize effectively.
        These queries are now available for any code written in the customer class. They can easily be added to the interface of the class should other parts of the system need this information.
        Without queries like these, other methods have to deal with knowing about the rentals and building the loops. In a complex system, that will lead to much more code to write and maintain
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
