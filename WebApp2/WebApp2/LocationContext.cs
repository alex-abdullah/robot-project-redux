using System;
using WebApp2.Controller.Services;
using Microsoft.EntityFrameworkCore;
namespace WebApp2
{
	[Keyless]
	public class LocationContext : DbContext
	{
		public LocationContext(DbContextOptions<LocationContext> options) : base(options)
		{
		}

		public DbSet<RobotSightings> Locations { get; set; }
	}
}

