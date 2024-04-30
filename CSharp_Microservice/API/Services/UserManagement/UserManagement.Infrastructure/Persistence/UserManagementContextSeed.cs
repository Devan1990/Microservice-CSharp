using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Application.Features.Role.Queries.GetRoles;
using UserManagement.Domain.Entities;

namespace UserManagement.Infrastructure.Persistence
{
    public class UserManagementContextSeed
    {
        public static async Task SeedAsync(UserManagementContext userManagementContext, ILogger<UserManagementContextSeed> logger)
        {
            if (!userManagementContext.Countries.Any())
            {
                userManagementContext.Countries.AddRange(GetPreconfiguredCountries());
                await userManagementContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(UserManagementContext).Name);
            }
            if (!userManagementContext.RoleType.Any())
            {
                userManagementContext.RoleType.AddRange(GetPreconfiguredRoleTypes());
                await userManagementContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(UserManagementContext).Name);
            }
            if (!userManagementContext.ExpectedLevels.Any())
            {
                userManagementContext.ExpectedLevels.AddRange(GetPreconfiguredExpectedLevels());
                await userManagementContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(UserManagementContext).Name);
            }
            if (!userManagementContext.Verticals.Any())
            {
                userManagementContext.Verticals.AddRange(GetPreconfiguredVerticals());
                await userManagementContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(UserManagementContext).Name);
            }

            await Task.CompletedTask;
        }

        private static IEnumerable<Country> GetPreconfiguredCountries()
        {
            return new List<Country>
            {
                new Country()
                {
                    CountryName = "Australia",
                    Areas = new List<Area>()
                    {
                        new Area()
                        {
                            AreaName = "Oceania Area",
                            Regions = new List<Region>()
                            {
                                new Region()
                                {
                                    RegionName ="Asia Pacific"
                                },
                                 new Region()
                                {
                                    RegionName ="Australi Regionf"
                                },
                                 new Region()
                                {
                                    RegionName ="Australi Regions"
                                },

                         }
                        },
                        new Area()
                        {
                            AreaName = "Northern Territory",
                            Regions = new List<Region>()
                            {
                                new Region()
                                { 
                                
                                    RegionName ="North Region "
                                },
                                 new Region()
                                {
                                    RegionName ="South Region "
                                },
                                  new Region()
                                {
                                    RegionName ="E Region "
                                }
                            }
                        },
                         new Area()
                        {
                            AreaName = "Queensland Area",
                            Regions = new List<Region>()
                            {
                                new Region()
                                {
                                    RegionName ="Queens Regions "
                                },
                                 new Region()
                                {
                                    RegionName ="Queens Regionw "
                                },
                                  new Region()
                                {
                                    RegionName ="Queens Regionf "
                                }
                            }
                        }
                    }
                },
                new Country()
                {
                    CountryName = "Belgium",
                    Areas = new List<Area>()
                    {
                        new Area()
                        {
                            AreaName = "North West Continent Area",
                            Regions = new List<Region>()
                            {
                                new Region()
                                {
                                    RegionName ="Europe"
                                },
                                 new Region()
                                {
                                    RegionName ="Europe Regiond"
                                },
                                  new Region()
                                {
                                    RegionName ="Europe Regiong "
                                }
                            }
                        },
                        new Area()
                        {
                            AreaName = "Belgium Areas",
                            Regions = new List<Region>()
                            {
                                new Region()
                                {
                                    RegionName ="Belgium regiond"
                                },
                                 new Region()
                                {
                                    RegionName ="Belgium regionr"
                                },
                                  new Region()
                                {
                                    RegionName ="Belgium regior"
                                }
                            }
                        }
                    }
                },
                new Country()
                {
                    CountryName = "Brazil",
                    Areas = new List<Area>()
                    {
                        new Area()
                        {
                            AreaName = "East Coast South America Area",
                            Regions = new List<Region>()
                            {
                                new Region()
                                {
                                    RegionName ="Latin America"
                                }
                            }
                        }
                    }
                },
                new Country()
                {
                    CountryName = "India",
                    Areas = new List<Area>()
                    {
                        new Area()
                        {
                            AreaName = "India and Bangladesh Area",
                            Regions = new List<Region>()
                            {
                                new Region()
                                {
                                    RegionName ="West Central Asia"
                                }
                            }
                        }
                    }
                },
            };
        }
        private static IEnumerable<ExpectedLevel> GetPreconfiguredExpectedLevels()
        {
            return new List<ExpectedLevel>
            {
                new ExpectedLevel()
                {
                    ExpectedLevelName = "Foundational",

                },
                 new ExpectedLevel()
                {
                    ExpectedLevelName = "Professional",

                },
                  new ExpectedLevel()
                {
                    ExpectedLevelName = "Advanced",

                }
            };
        }

            private static IEnumerable<RoleType> GetPreconfiguredRoleTypes()
            {
                return new List<RoleType>
            {
                new RoleType()
                {
                    Roletype = "Assessor",

                },
                 new RoleType()
                {
                    Roletype = "Assessee",

                }
        };
            }
        private static IEnumerable<Verticals> GetPreconfiguredVerticals()
        {
            return new List<Verticals>
            {
                new Verticals()
                {
                    Vertical = "vertical 7",

                },
                 new Verticals()
                {
                    Vertical = "vertical 6",

                },
                  new Verticals()
                {
                    Vertical = "vertical 5",

                },
                   new Verticals()
                {
                    Vertical = "vertical 4",

                },
                    new Verticals()
                {
                    Vertical = "vertical 3",

                },
                     new Verticals()
                {
                    Vertical = "vertical 2",

                },
                      new Verticals()
                {
                    Vertical = "vertical 1",

                }
        };
        }

    }
}
