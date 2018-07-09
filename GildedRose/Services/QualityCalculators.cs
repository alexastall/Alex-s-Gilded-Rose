using System;
using GildedRose.Interfaces;
using GildedRose.Models;

namespace GildedRose.Services
{
    public class UpdateQualityFactory : IUpdateQualityFactory
    {
        public IUpdateQuality Create(string name)
        {
            switch (name)
            {
                case "Aged Brie":
                    return new AgedBrieQualityCalculator();
                case "Sulfuras":
                    return new DoNothingUpdate();
                case "Backstage passes":
                    return new BackstagePassesQualityCalculator();
                case "Conjured":
                    return new StandardQualityCalculator(2);
                default:
                    return new StandardQualityCalculator();
            }
        }
    }

    public class StandardQualityCalculator : IUpdateQuality
    {
        private readonly int _factor;

        public StandardQualityCalculator(int factor = 1)
        {
            _factor = factor;
        }

        public void UpdateQuality(Item item)
        {
            item.SellIn--;
            if (item.Name == "INVALID ITEM") // Could put some clever logic here to determine if item is invalid such as checking for valid item types in DB.
            {
                throw new Exception("NO SUCH ITEM");
            }

            if (item.Quality > 0)
            {
                var amount = 1; // Standard decrement amount
                if (item.SellIn < 0) // If past sell by date, increase quality decrement amount 
                {
                    amount = 2;
                }
                item.Quality -= _factor * amount;
            }
            if (item.Quality < 0) // Quality cannot drop below zero
            {
                item.Quality = 0;
            }
        }
    }

    public class BackstagePassesQualityCalculator : IUpdateQuality
    {
        public void UpdateQuality(Item item)
        {
            item.SellIn--;
            if (item.SellIn < 0) // Drop to zero after the event has passed
            {
                item.Quality = 0;
            }
            else if (item.SellIn <= 5) // Increase quality by 3 if <= 5 days from event
            {
                item.Quality = item.Quality + 3;
            }
            else if (item.SellIn <= 10) // Increase quality by 2 if <= 10 days from event
            {
                item.Quality = item.Quality + 2;
            }
            else if (item.Quality < 50)
            {
                item.Quality++;
            }
        }
    }

    public class AgedBrieQualityCalculator : IUpdateQuality
    {
        public void UpdateQuality(Item item)
        {
            item.SellIn--;
            if (item.Quality < 50)
            {
                item.Quality++;
            }
        }
    }


    public class DoNothingUpdate : IUpdateQuality
    {
        public void UpdateQuality(Item item) { }
    }
}