using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;

var heroes = new List<Hero>
{
    new("Wade", "Wilson", "Dead-pool", false),
    new(string.Empty, string.Empty, "Home-lander", true),
    new("Bruce", "Wayne", "Batman", false),
    new("Peter", "Parker", "Spider-man", true),
    new(string.Empty, string.Empty, "Storm-front", true),
};

// You can build the function using parenthesis or not
var heroesWhoCanFly = HeroesFilterWithPredefinedDelegate(heroes, hero => hero.CanFly);
var heroesWhoCanFlyString = string.Join(", ", heroesWhoCanFly);
Console.WriteLine("HEROES WHO CAN FLY");
Console.WriteLine(heroesWhoCanFlyString);

// You can build the function using parenthesis or not
var heroesUnknownLastName = HeroesFilterWithDelegate(heroes, (Hero hero) => string.IsNullOrEmpty(hero.LastName));
var heroesFilterString = string.Join(", ", heroesUnknownLastName);
Console.WriteLine();
Console.WriteLine("HEROES WHO HAS NOT LAST NAME");
Console.WriteLine(heroesFilterString);

// with the generic delegate and function we can use infinity filter, for example
var superHeroesWithFirstLetterW = GenericFilter(new[] { "Spiderman", "Batman", "HomeLander", "Deadpool", "Wondereoman" }, name => name.StartsWith("W")).ToList();
var evenNumbers = GenericFilter(new[] { 1, 2, 3, 5, 6, 7, 8, 9, 23, 45 }, number => number % 2 == 0).ToList();

// IEnumerable is just forward only and read only
// Using predefined delegate 
// Func<InputParameter, ReturnValue> genericFilter
static IEnumerable<Hero> HeroesFilterWithPredefinedDelegate(IEnumerable<Hero> heroes, Func<Hero, bool> myMethodName)
{
    // Same function than FilterHeroesWhoCanFly/HeroesWhoLastNameIsUnknown
    // but converter to LINQ. Notice this is the group form, we can call the function "genericFilter"
    // without parameter
    return heroes.Where(myMethodName).ToList();
}


// Generic Filter using Predefined Delegate
// Using predefined delegate
static IEnumerable<T> FilterWithPredefinedDelegate<T>(IEnumerable<T> items, Func<T, bool> genericFilter)
{
    // Same function than FilterHeroesWhoCanFly/HeroesWhoLastNameIsUnknown
    // but converter to LINQ. Notice this is the group form, we can call the function "genericFilter"
    // without parameter
    return items.Where(genericFilter).ToList();
}

static IEnumerable<Hero> HeroesFilterWithDelegate(IEnumerable<Hero> heroes, Filter myMethodName)
{
    return heroes.Where(hero => myMethodName(hero)).ToList();
}



static IEnumerable<T> GenericFilter<T>(IEnumerable<T> items, GenericFilter<T> genericFilter)
{
    return items.Where(item => genericFilter(item)).ToList();
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

// The same than below but using Yield,
// yield is a keyword that works in any function that return an IEnumerable
IEnumerable<Hero> FilterHeroesWhoCanFlyUsingYield(IEnumerable<Hero> heroes)
{
    foreach (var hero in heroes)
    {
        if (hero.CanFly)
        {
            yield return hero;
        }
    }
}

// Same function below but generic and GenericFilter as a parameter
IEnumerable<T> GenericFilterHeroesWhoCanFlyUsingYield<T>(IEnumerable<T> items, GenericFilter<T> filter)
{
    foreach (var item in items)
    {
        if (filter(item))
        {
            yield return item;
        }
    }
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

// Generic delegate
delegate bool GenericFilter<T>(T hero);