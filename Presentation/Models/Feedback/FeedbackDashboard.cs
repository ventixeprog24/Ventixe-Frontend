using System;
using System.Collections.Generic;

namespace FeedbackDashboard.Models
{
    public class FeedbackDashboardViewModel
    {
        public RatingSummary RatingSummary { get; set; } = new();
        public FeedbackStatistics Statistics { get; set; } = new();
        public List<FeedbackCard> FeedbackCards { get; set; } = new();
        public FilterOptions Filters { get; set; } = new();
        public PaginationInfo Pagination { get; set; } = new();
        public DateRange DateRange { get; set; } = new();
    }

    public class RatingSummary
    {
        public double OverallRating { get; set; }
        public int TotalReviews { get; set; }
        public List<CategoryRating> CategoryRatings { get; set; } = new();
    }

    public class CategoryRating
    {
        public string Category { get; set; } = string.Empty;
        public double Rating { get; set; }
    }

    public class FeedbackStatistics
    {
        public int LowRatingsCount { get; set; } // 1-3 stars
        public int HighRatingsCount { get; set; } // 4-5 stars
        public List<MonthlyData> MonthlyData { get; set; } = new();
        public string SelectedTimeRange { get; set; } = "This Year";
    }

    public class MonthlyData
    {
        public string Month { get; set; } = string.Empty;
        public int LowRatings { get; set; }
        public int HighRatings { get; set; }
    }

    public class FeedbackCard
    {
        public string ReviewerName { get; set; } = string.Empty;
        public DateTime ReviewDate { get; set; }
        public int StarRating { get; set; }
        public string ReviewContent { get; set; } = string.Empty;
        public string EventName { get; set; } = string.Empty;
        public string EventCategory { get; set; } = string.Empty;
        public string CategoryColor { get; set; } = "#ec4899"; // Default pink
    }

    public class FilterOptions
    {
        public string SelectedRating { get; set; } = "All Rating";
        public string SelectedCategory { get; set; } = "All Category";
        public string SelectedEvent { get; set; } = "All Event";
        
        public List<string> RatingOptions { get; set; } = new() { "All Rating", "5 Stars", "4 Stars", "3 Stars", "2 Stars", "1 Star" };
        public List<string> CategoryOptions { get; set; } = new() { "All Category", "Music", "Food & Culinary", "Technology", "Fashion", "Art & Design" };
        public List<string> EventOptions { get; set; } = new() { "All Event", "Echo Beats Festival", "Culinary Delights", "Symphony Under the Stars" };
    }

    public class PaginationInfo
    {
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 95;
        public int TotalEntries { get; set; } = 568;
        public int EntriesPerPage { get; set; } = 6;
        public int StartEntry => (CurrentPage - 1) * EntriesPerPage + 1;
        public int EndEntry => Math.Min(CurrentPage * EntriesPerPage, TotalEntries);
    }

    public class DateRange
    {
        public DateTime StartDate { get; set; } = new DateTime(2029, 4, 1);
        public DateTime EndDate { get; set; } = new DateTime(2029, 5, 30);
    }
}