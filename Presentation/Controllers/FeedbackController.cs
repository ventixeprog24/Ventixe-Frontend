using Microsoft.AspNetCore.Mvc;
using FeedbackDashboard.Models;

namespace FeedbackDashboard.Controllers
{
    public class FeedbackController : Controller
    {
        public IActionResult Dashboard()
        {
            var viewModel = GetMockData();
            return View(viewModel);
        }

        private FeedbackDashboardViewModel GetMockData()
        {
            return new FeedbackDashboardViewModel
            {
                RatingSummary = new RatingSummary
                {
                    OverallRating = 4.8,
                    TotalReviews = 15545,
                    CategoryRatings = new List<CategoryRating>
                    {
                        new() { Category = "Venue", Rating = 4.7 },
                        new() { Category = "Event Organization", Rating = 4.8 },
                        new() { Category = "Staff Support", Rating = 4.6 },
                        new() { Category = "Entertainment Quality", Rating = 4.9 },
                        new() { Category = "Food & Beverages", Rating = 4.3 },
                        new() { Category = "Value for Money", Rating = 4.5 }
                    }
                },
                Statistics = new FeedbackStatistics
                {
                    LowRatingsCount = 110,
                    HighRatingsCount = 880,
                    MonthlyData = new List<MonthlyData>
                    {
                        new() { Month = "Jan", LowRatings = 45, HighRatings = 320 },
                        new() { Month = "Feb", LowRatings = 38, HighRatings = 380 },
                        new() { Month = "Mar", LowRatings = 42, HighRatings = 390 },
                        new() { Month = "Apr", LowRatings = 35, HighRatings = 420 },
                        new() { Month = "May", LowRatings = 40, HighRatings = 450 },
                        new() { Month = "Jun", LowRatings = 48, HighRatings = 480 },
                        new() { Month = "Jul", LowRatings = 52, HighRatings = 510 },
                        new() { Month = "Aug", LowRatings = 46, HighRatings = 490 },
                        new() { Month = "Sep", LowRatings = 44, HighRatings = 470 },
                        new() { Month = "Oct", LowRatings = 50, HighRatings = 520 },
                        new() { Month = "Nov", LowRatings = 55, HighRatings = 540 },
                        new() { Month = "Dec", LowRatings = 60, HighRatings = 580 }
                    }
                },
                FeedbackCards = new List<FeedbackCard>
                {
                    new()
                    {
                        ReviewerName = "Jackson Moore",
                        ReviewDate = new DateTime(2029, 4, 22),
                        StarRating = 5,
                        ReviewContent = "An absolutely amazing festival! The lineup of artists was incredible, and the sound quality was impeccable. The energy from the crowd made it a night to remember.",
                        EventName = "Echo Beats Festival",
                        EventCategory = "Music",
                        CategoryColor = "#ec4899"
                    },
                    new()
                    {
                        ReviewerName = "Alicia Smithson",
                        ReviewDate = new DateTime(2029, 5, 2),
                        StarRating = 4,
                        ReviewContent = "Beautiful designs and a well-organized event overall. The stage lighting were captivating, but the seating arrangements could have been planned better for the audience.",
                        EventName = "Runway Revolution 2029",
                        EventCategory = "Fashion",
                        CategoryColor = "#ec4899"
                    },
                    new()
                    {
                        ReviewerName = "Patrick Cooper",
                        ReviewDate = new DateTime(2029, 4, 20),
                        StarRating = 5,
                        ReviewContent = "The music under the open sky was breathtaking. The orchestra was phenomenal, and the ambiance made it feel like a dream. Everything was organized beautifully.",
                        EventName = "Symphony Under the Stars",
                        EventCategory = "Music",
                        CategoryColor = "#ec4899"
                    },
                    new()
                    {
                        ReviewerName = "Clara Simmons",
                        ReviewDate = new DateTime(2029, 5, 25),
                        StarRating = 4,
                        ReviewContent = "The variety of cuisines and food stalls was fantastic! The flavors were outstanding, though some popular stalls ran out of food too early in the event.",
                        EventName = "Culinary Delights Festival",
                        EventCategory = "Food & Culinary",
                        CategoryColor = "#ec4899"
                    },
                    new()
                    {
                        ReviewerName = "Natalie Johnson",
                        ReviewDate = new DateTime(2029, 5, 18),
                        StarRating = 5,
                        ReviewContent = "The expo was well laid out and well-organized! The installations were awe-inspiring, and the chance to meet artists was a highlight of the event for me.",
                        EventName = "Artistry Unveiled Expo",
                        EventCategory = "Art & Design",
                        CategoryColor = "#ec4899"
                    },
                    new()
                    {
                        ReviewerName = "Henry Carter",
                        ReviewDate = new DateTime(2029, 6, 1),
                        StarRating = 4,
                        ReviewContent = "A fantastic platform for tech enthusiasts to explore the latest innovations. More hands-on workshops would have made the event even better, but it was still very informative.",
                        EventName = "Tech Future Expo",
                        EventCategory = "Technology",
                        CategoryColor = "#ec4899"
                    }
                }
            };
        }
    }
}