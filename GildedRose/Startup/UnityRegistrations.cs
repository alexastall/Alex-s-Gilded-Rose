using GildedRose.Interfaces;
using GildedRose.Services;
using Unity;

namespace GildedRose.Startup
{
    class UnityRegistrations : UnityContainer
    {
        public UnityRegistrations()
        {
            this.RegisterType<ILogger, ConsoleLogger>();
            this.RegisterType<IUpdateQualityFactory, UpdateQualityFactory>();
        }
    }
}
