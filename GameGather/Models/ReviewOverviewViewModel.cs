namespace Web.Models
{
    public class ReviewOverviewViewModel
    {
        public double AverageScore
        {
            get
            {
                 // Use the reviews to calculate the average score and round it to 1 decimal
                 if (Reviews != null && Reviews.Count() > 0)
                {
                    return Math.Round(Reviews.Average(r => r.Score), 2);
                    }
                    else
                {
                        return 0;
                    }
            }
        }

        public int NumberOfHostedBoardGameNights { get; set; }

        public IEnumerable<ReviewViewModel>? Reviews { get; set; }
    }
}
