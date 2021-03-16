using System;
using System.Collections.Generic;
using System.Linq;

var heroes = new List<Hero>
{
    new("Wade", "Wilson", "Dead-pool", false),
    new(string.Empty, string.Empty, "Home-lander", true),
    new("Bruce", "Wayne", "Batman", false),
    new("Peter", "Parker", "Spider-man", true),
    new(string.Empty, string.Empty, "Storm-front", true),
};

var heroesWhoCanFly = HeroesFilterWithoutDelegate(heroes, (Hero hero) => hero.CanFly);
var heroesWhoCanFlyString = string.Join(", ", heroesWhoCanFly);
Console.WriteLine("HEROES WHO CAN FLY");
Console.WriteLine(heroesWhoCanFlyString);

var heroesUnknownLastName = HeroesFilterWithDelegate(heroes, (Hero hero) => string.IsNullOrEmpty(hero.LastName));
var heroesFilterString = string.Join(", ", heroesUnknownLastName);
Console.WriteLine();
Console.WriteLine("HEROES WHO HAS NOT LAST NAME");
Console.WriteLine(heroesFilterString);

static List<Hero> HeroesFilterWithoutDelegate(List<Hero> heroes, Func<Hero, bool> myMethodName)
{
    // Same function than FilterHeroesWhoCanFly/HeroesWhoLastNameIsUnknown
    // but converter to LINQ. Notice this is the group form, we can call the function "myMethodName"
    // without parameter
    return heroes.Where(myMethodName).ToList();
}

static List<Hero> HeroesFilterWithDelegate(List<Hero> heroes, Filter myMethodName)
{
    return heroes.Where(hero => myMethodName(hero)).ToList();
}


// Using delegate we can make one function that do the same of these two, as shown above
List<Hero> FilterHeroesWhoCanFly(List<Hero> heroes)
{
    List<Hero> resultList = new();
    foreach (var hero in heroes)
    {
        if (hero.CanFly)
        {
            resultList.Add(hero);
        }
    }
    return resultList;
}

List<Hero> HeroesWhoLastNameIsUnknown(List<Hero> heroes)
{
    List<Hero> resultList = new();
    foreach (var hero in heroes)
    {
        if (hero.LastName == string.Empty)
        {
            resultList.Add(hero);
        }
    }
    return resultList;
}

// The delegate is a kind of contract, is the definition type of a method/function
// Delegate is for a methods what Interface is for a class
delegate bool Filter(Hero hero);
record Hero(string FirstName, string LastName, string HeroName, bool CanFly);