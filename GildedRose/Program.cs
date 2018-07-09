using System;
using System.Collections.Generic;
using GildedRose.Interfaces;
using GildedRose.Models;
using GildedRose.Startup;
using Unity;

namespace GildedRose
{
    class Program
    {
        private readonly ILogger _logger;
        private readonly IUpdateQualityFactory _qualityFactory;
        private IList<Item> _items;

        public Program()
        {
            var container = new UnityRegistrations();
            _logger = container.Resolve<ILogger>();
            _qualityFactory = container.Resolve<IUpdateQualityFactory>();
        }

        static void Main(string[] args)
        {
            var app = new Program
            {
                _items = new List<Item>
                {
                    new Item {Name = "Aged Brie", SellIn = 1, Quality = 1},
                    new Item {Name = "Backstage passes", SellIn = -1, Quality = 2},
                    new Item {Name = "Backstage passes", SellIn = 9, Quality = 2},
                    new Item {Name = "Sulfuras", SellIn = 2, Quality = 2},
                    new Item {Name = "Normal Item", SellIn = -1, Quality = 55},
                    new Item {Name = "Normal Item", SellIn = 2, Quality = 2},
                    new Item {Name = "INVALID ITEM", SellIn = 2, Quality = 2},
                    new Item {Name = "Conjured", SellIn = 2, Quality = 2},
                    new Item {Name = "Conjured", SellIn = -1, Quality = 5}
                }
            };

            while (true)
            {
                app.UpdateQuality();
                Console.ReadKey();
            }
        }

        public void UpdateQuality()
        {
            foreach (Item item in _items)
            {
                var updateStrategy = _qualityFactory.Create(item.Name);
                _logger.Log($"BEFORE - Item: {item.Name}, sell in {item.SellIn}, quality: {item.Quality}.");
                try
                {
                    updateStrategy.UpdateQuality(item);
                    _logger.Log($"AFTER  - Item: {item.Name}, sell in {item.SellIn}, quality: {item.Quality}.");
                }
                catch (Exception e)
                {
                    _logger.Log(e.Message);
                }
                finally
                {
                    _logger.Log("");
                }
            }
            _logger.Separator();
        }
    }
}